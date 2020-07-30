using System;
using System.Collections.Generic;
using System.Net;

namespace DisplayConfigLib
{
    public class DisplayConfig
    {
        private string version;
        private int interval;
        private int retry;
        private int timeout;

        public string Version
        {
            get
            {
                return this.version;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new DisplayException(ErrorCode.EMPTY, $"{nameof(this.Version)} in appsettings.json");
                this.version = value;
            }
        }

        public int Interval
        {
            get
            {
                return this.interval;
            }
            set
            {
                if (value < 1)
                    throw new DisplayException(ErrorCode.VALUE, $"{nameof(this.Interval)} in appsettings.json");
                this.interval = value;
            }
        }

        public int Retry
        {
            get
            {
                return this.retry;
            }
            set
            {
                if (value < 1)
                    throw new DisplayException(ErrorCode.VALUE, $"{nameof(this.Retry)} in appsettings.json");
                this.retry = value;
            }
        }
        public int Timeout
        {
            get
            {
                return this.timeout;
            }
            set
            {
                if (value < 1)
                    throw new DisplayException(ErrorCode.VALUE, $"{nameof(this.Timeout)} in appsettings.json");
                this.timeout = value;
            }
        }

        public IEnumerable<Room> Rooms { get; set; }
    }

    public class Room
    {
        private string name;
        private string address;
        private int port;
        private string link;
        private int screen;
        private int orientation;

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new DisplayException(ErrorCode.EMPTY, $"{nameof(this.Name)} in appsettings.json");
                this.name = value;
            }
        }

        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new DisplayException(ErrorCode.EMPTY, $"{nameof(this.Address)} in appsettings.json");

                if(!IPAddress.TryParse(value, out IPAddress address))
                    throw new DisplayException(ErrorCode.EMPTY, $"{nameof(this.Address)} in appsettings.json");

                this.address = value;
            }
        }

        public int Port
        {
            get
            {
                return this.port;
            }
            set
            {
                if (value < 1 || value > UInt16.MaxValue)
                    throw new DisplayException(ErrorCode.VALUE, $"{nameof(this.Port)} in appsettings.json");
                this.port = value;
            }
        }

        public string Link
        {
            get
            {
                return this.link;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new DisplayException(ErrorCode.EMPTY, $"{nameof(this.Link)} in appsettings.json");
                this.link = value;
            }
        }

        public int Screen
        {
            get
            {
                return this.screen;
            }
            set
            {
                if (value < 1)
                    throw new DisplayException(ErrorCode.VALUE, $"{nameof(this.Screen)} in appsettings.json");
                this.screen = value;
            }
        }

        public int Orientation
        {
            get
            {
                return this.orientation;
            }
            set
            {
                if (value < 0 || value > 4)
                    throw new DisplayException(ErrorCode.VALUE, $"{nameof(this.Orientation)} in appsettings.json");
                this.orientation = value;
            }
        }

        public bool Selected { get; set; }

        public bool Proxy { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
