[![Version: 1.0 Release](https://img.shields.io/badge/Version-1.0%20Release-green.svg)](https://github.com/sunriax) [![License: GPL v3](https://img.shields.io/badge/License-GPL%20v3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)

# Screen Rotating Tool

## Description:

Windows Tool that is polling within a configured time a REST API on a raspberry. The REST API delivers the status of a reed contact (true/false), that is connected to the raspberry PI. Within the tool specified display in the software gets rotated.

[![ScreenRotator](https://github.com/sunriax/screen/raw/develop/screenrotator.png)](https://github.com/sunriax/screen/tree/master/ScreenRotator)

## Required Hardware:

1. Windows PC ([running .NET Core 3 runtime](https://dotnet.microsoft.com/download/))
1. [Raspberry PI 3](https://at.rs-online.com/web/p/entwicklungskits-prozessor-mikrocontroller/1812043/)
1. [Reed contact](https://at.rs-online.com/web/p/magnetische-naherungsschalter/5245472/)

---

## Installation

### Windows PC

To install ScreenRotator on a local PC it is necessary to download the installer or extract the portable version to the host (e.g. C:\Tools\ScreenRotator).

* [Portable Version](https://github.com/sunriax/screen/releases/latest/download/ScreenRotator.zip)
* [Installer](https://github.com/sunriax/screen/releases/latest/download/ScreenRotator.msi)

#### Configuration

To setup the endpoints (raspberries) where the host should connect to, configure necessary parameters in appsettings.json

*Global Parameter*

| Parameter | Description                                                |
|:----------|:-----------------------------------------------------------|
| Interval  | Time in seconds the raspberry will be queried              |
| Retry     | If an error occurrs how often a reconnection will be tryed |
| Timeout   | Time in seconds a query to the raspberry can take          |

*Room Parameter*

| Parameter | Description                             |
|:----------|:----------------------------------------|
| Name      | Name of the room                        |
| Address   | IP-Address of the endpoint (raspberry)  |
| Proxy     | Use system proxy to query the endpoints |
| Selected  | Room selected by default                |

**`appsettings.json`**

``` json
{
  "Version": "1.0",
  "Interval": 10,
  "Retry": 10,
  "Timeout": 5,
  "Rooms": [
    {
      "Name": "Room ???",
      "Address": "IP-ADDRESS",
      "Port": 8443,
      "Link": "screen",
      "Screen": 1,
      "Orientation": 1,
      "Selected": true,
      "Proxy": false
    },
    {
      "Name": "Raum ???",
      "Address": "IP-ADDRESS",
      "Port": 8443,
      "Link": "Screen",
      "Screen": 1,
      "Orientation": 1,
      "Selected": false,
      "Proxy": false
    }
  ]
}
```

If the standard endpoint service is used the Port and Link will be 8443 and Screen. This can be changed in the ScreenService appsettings.json that runs on raspberry.

### Raspberry PI

To install ScreenService on the endpoint it is necessary to [download](https://github.com/sunriax/screen/releases/latest/download/ScreenService.tar.gz) and extract the service to the host.

#### Hardware Setup

```
,--------------------------------.
| oooooooooooooooooooo J8     +====
| 1oooXXoooooooooooooo        | USB
|                             +====
|      Pi Model 3B  V1.2         |
|      +----+                 +====
| |D|  |SoC |                 | USB
| |S|  |    |                 +====
| |I|  +----+                    |
|                   |C|     +======
|                   |S|     |   Net
| pwr        |HDMI| |I||A|  +======
`-| |--------|    |----|V|-------'

J8:
   3V3  (1) (2)  5V
 GPIO2  (3) (4)  5V
 GPIO3  (5) (6)  GND
 GPIO4  (7) (8)  GPIO14
   GND  (9) (10) GPIO15
GPIO17 (11) (12) GPIO18
GPIO27 (13) (14) GND
GPIO22 (15) (16) GPIO23
   3V3 (17) (18) GPIO24
GPIO10 (19) (20) GND
 GPIO9 (21) (22) GPIO25
GPIO11 (23) (24) GPIO8
   GND (25) (26) GPIO7
 GPIO0 (27) (28) GPIO1
 GPIO5 (29) (30) GND
 GPIO6 (31) (32) GPIO12
GPIO13 (33) (34) GND
GPIO19 (35) (36) GPIO16
GPIO26 (37) (38) GPIO20
   GND (39) (40) GPIO21
```

The reed contact is connected to Pin 9 (Ground) and Pin 11 (GPIO 17). To get low/high level the circuit is driven with the internal pullup resistor of the raspberry.

#### Installation

``` bash
wget https://github.com/sunriax/screen/releases/latest/download/ScreenService.tar.gz
tar -xvzf ScreenService.tar.gz
chown -R root:root ./screen
mv ./screen /opt
chmod 0777 /opt/screen/service

# Try to run the service
/opt/screen/service
```

#### Configuration

*Room Parameter*

| Parameter   | Description                     |
|:------------|:--------------------------------|
| HTTP_PORT   | HTTP listening port             |
| HTTPS_PORT  | HTTPS listening port            |
| Link        | API Path /screen                |
| Certificate | Certificate that should be used |

**`appsettings.json`**

``` json
{
  "Version": "1.0",
  "HTTP_PORT": 8080,
  "HTTPS_PORT": 8443,
  "Link": "screen",
  "Port": 17,
  "Certificate": "DEMO.crt"
}
```

#### Systemd Service

To start the ScreenService automatically it is possible to create a systemd service.

**`/lib/systemd/system/screen.service`**
```
[Unit]
Description=Screen Service .NET Core API

[Service]
WorkingDirectory=/opt/screen
ExecStart=/opt/screen/ScreenService
Restart=always
# RestartSec=10
SyslogIdentifier=netcore-screen
User=pi
# Environment=ASPNETCORE_ENVIRONMENT=Production
[Install]
WantedBy=multi-user.target
```

``` bash
# Link the service to systemd
sudo ln -s /lib/systemd/system/screen.service /etc/systemd/system/

sudo systemctl daemon-reload
sudo systemctl start screen.service

sudo systemctl status screen.service
# screen.service should be active
● screen.service - Screen Service .NET Core API
   Loaded: loaded (/lib/systemd/system/screen.service; enabled; vendor preset: enabled)
   Active: active (running) since Thu 2020-07-30 13:32:49 CEST; 45min ago
 Main PID: 343 (ScreenService)
    Tasks: 18 (limit: 2068)
   CGroup: /system.slice/screen.service
           └─343 /opt/screen/ScreenService

Jul 30 13:32:49 ... systemd[1]: Started Screen Service .NET Core API.
...

# Enable ScreenService permanent
sudo systemctl enable screen
```

## Error

If the failures are not listed feel free to start an issue!

### Windows

* .NET Core runtime not installed [Download here](https://dotnet.microsoft.com/download)

### Linux

* .NET Core runtime not installed [Installation Instruction](#Install-.NET-Core-runtime-on-raspberry)

#### Installation Instruction

Get the latest [download link here](https://dotnet.microsoft.com/download/dotnet-core/3.1)

``` bash
## Download the latest version
wget https://download.visualstudio.microsoft.com/download/pr/92e90ed5-dba3-427b-a876-8b46fe5e16b6/69cae4fe4a0ec6bc7585de6fa889fd66/aspnetcore-runtime-3.1.6-linux-arm.tar.gz

mkdir dotnet
cd dotnet

tar -xvzf ../aspnetcore-runtime-3.1.6-linux-arm.tar.gz

cd ..

sudo chown -R root:root ./dotnet/
sudo mv ./dotnet/ /opt/
cd /opt/dotnet/
```

To export dotnet path create a profile file

**`/etc/profile.d/dotnet.sh`**
``` bash
export DOTNET_ROOT=/opt/dotnet
export PATH=$PATH:/opt/dotnet
```

and link the dotnet runtime to standard path

``` bash
ln -s /opt/dotnet/ /usr/share/dotnet
```

---