using DisplayConfigLib;
using DisplayLib;
using Microsoft.Extensions.Configuration;
using ScreenRotator.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenRotator
{
    public partial class FormMain : Form
    {
        private int errorCount;

        private DisplayConfig config;
        private Display display;

        private HttpClientHandler handler;
        private HttpClient client;

        public FormMain()
        {
            this.display = Display.GetInstance;

            this.InitializeComponent();

            this.notifyIconDisplay.BalloonTipTitle = DisplayResource.BallonTipTitle;
            this.notifyIconDisplay.BalloonTipText = DisplayResource.BallonTipText;

            this.LoadConfig();
            this.labelVersionNr.Text = this.config.Version;

            this.comboBoxRoom.DataSource = this.config.Rooms;
            this.comboBoxDisplay.DataSource = Screen.AllScreens.Select(s => s.DeviceName.Replace(@"\\.", string.Empty)).ToList<string>();
            this.comboBoxRotation.DataSource = Enum.GetValues(typeof(Display.Orientation)).Cast<Display.Orientation>().Select(x => $"{x.ToString().Replace("DEGREES_CW_", string.Empty)} {DisplayResource.Degree}").ToList<string>();

            this.GetCurrentRoom();
            this.GetCurrentScreen();
            this.GetCurrentOrientation();

            this.handler = new HttpClientHandler()
            {
#if DEBUG
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true,
#endif
                UseProxy = this.config.Rooms.Where(x => x.Selected == true).First().Proxy
            };
            this.client = new HttpClient(handler)
            {
                Timeout = new TimeSpan(0, 0, this.config.Timeout)
            };

            this.timerQuery.Enabled = true;
            this.timerQuery.Interval = this.config.Interval * 1000;
        }

        private void LoadConfig()
        {
            try
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(DisplayResource.ConfigFile, false, false)
                    .Build();

                //this.config = new DisplayConfig();
                this.config = configuration.Get<DisplayConfig>();
                //configuration.Bind(this.config);

                if (this.config.Rooms == null || this.config.Rooms.Count() == 0)
                    throw new DisplayException(ErrorCode.EMPTY, "No rooms found!");
            }
            catch (Exception ex)
            {
                if (ex.InnerException is DisplayException)
                    MessageBox.Show((ex.InnerException as DisplayException).ErrorMessage(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Load += (s, e) => Close();
                return;
            }

        }

        private void GetCurrentRoom()
        {
            try
            {
                this.comboBoxRoom.SelectedItem = this.config.Rooms.Where(r => r.Selected == true).First();
            }
            catch
            {
                this.comboBoxRoom.SelectedItem = this.config.Rooms.First();
                this.config.Rooms.First().Selected = true;
            }

            this.comboBoxRoom.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
        }

        private void GetCurrentScreen()
        {
            try
            {
                this.comboBoxDisplay.SelectedItem = Screen
                    .AllScreens
                    .Select(s => s.DeviceName.Replace(@"\\.", string.Empty))
                    .ToList<string>()
                    .ElementAt((this.comboBoxRoom.SelectedItem as Room).Screen - 1);
            }
            catch { }

            this.comboBoxDisplay.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
        }

        private void GetCurrentOrientation()
        {
            try
            {
                this.comboBoxRotation.SelectedItem = Enum
                    .GetValues(typeof(Display.Orientation))
                    .Cast<Display.Orientation>()
                    .Select(x => $"{x.ToString().Replace("DEGREES_CW_", string.Empty)} {DisplayResource.Degree}")
                    .ToList<string>()
                    .ElementAt((this.comboBoxRoom.SelectedItem as Room).Orientation);
            }
            catch { }

            this.comboBoxRotation.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            if (comboBox.Name == this.comboBoxRoom.Name)
            {
                this.timerQuery.Start();
                this.errorCount = 0;

                this.config.Rooms.ToList().ForEach(x =>
                {
                    if (x.Name == (this.comboBoxRoom.SelectedItem as Room).Name)
                        x.Selected = true;
                    else
                        x.Selected = false;
                });

                GetCurrentScreen();
                GetCurrentOrientation();
            }
            else if (comboBox.Name == this.comboBoxDisplay.Name)
            {
                (this.comboBoxRoom.SelectedItem as Room).Screen = Screen
                    .AllScreens
                    .Select(s => s.DeviceName.Replace(@"\\.", string.Empty))
                    .ToList<string>().IndexOf(this.comboBoxDisplay.SelectedItem as string) + 1;
            }
            else if (comboBox.Name == this.comboBoxRotation.Name)
            {
                (this.comboBoxRoom.SelectedItem as Room).Orientation = Enum
                    .GetValues(typeof(Display.Orientation))
                    .Cast<Display.Orientation>()
                    .Select(x => $"{x.ToString().Replace("DEGREES_CW_", string.Empty)} {DisplayResource.Degree}")
                    .ToList<string>().IndexOf(this.comboBoxRotation.SelectedItem as string);
            }
        }

        private async void timerQuery_Tick(object sender, EventArgs e)
        {
            if (this.errorCount >= this.config.Retry)
            {
                this.timerQuery.Stop();
                this.labelConnection.ForeColor = Color.DarkBlue;
                labelConnection.Text = DisplayResource.ConnectionStopped;

                return;
            }

            try
            {
                Room r = this.comboBoxRoom.SelectedItem as Room;

                if (bool.TryParse(await client.GetStringAsync($"https://{r.Address}:{r.Port}/{r.Link}"), out bool status))
                {
                    this.labelConnection.ForeColor = Color.DarkGreen;
                    this.labelConnection.Text = DisplayResource.ConnectionEstablished;

                    if (status)
                        this.display.Rotate((uint)r.Screen, (Display.Orientation)r.Orientation);
                    else
                        this.display.ResetAllRotations();

                    this.errorCount = 0;
                }
            }
            catch
            {
                this.labelConnection.ForeColor = Color.DarkRed;
                labelConnection.Text = DisplayResource.ConnectionError;

                this.errorCount++;
            }
        }

        private void menuItemStartQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void notifyIconDisplay_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIconDisplay.Visible = true;
                notifyIconDisplay.ShowBalloonTip(1000);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIconDisplay.Visible = false;
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            CreateConfig();

            display.ResetAllRotations();
            timerQuery.Dispose();
            client.Dispose();
            notifyIconDisplay.Dispose();
        }

        private void CreateConfig()
        {
            JsonSerializerOptions serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            File.WriteAllText(DisplayResource.ConfigFile, JsonSerializer.Serialize(this.config, serializeOptions));
        }
    }
}
