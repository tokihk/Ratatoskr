![image](doc/_images/app_icon_128x128.png) ![image](doc/_images/app_logo_600x120.png)

https://github.com/tokihk/Ratatoskr

Ratatoskr is a development support tool that can communicate with various devices that can be connected to a personal computer.
It provides a general-purpose communication debugging environment for various communication ports such as serial port and TCP / UDP.

You can intuitively operate input / output to / from ports by easy-to-view packet view and interface that emphasizes operability.

Ratatoskr has the following features.

* Supports various communication such as serial port, TCP / IP, UDP
  * Serial Port
  * TCP/IP Server or Client
  * UDP
  * USB Capture
* Supports packet view according to usage, such as packet unit and data unit
* Simultaneous connection to multiple ports
* Simultaneous display of multiple types of packet views
* Saving / loading / saving packet logs automatically
* Real-time packet control such as filtering, packet combining
* Redirect transfer from port to port

# Getting Started

## Prerequisites

* .NET Framework 4.7

## Installing

### Install

After installing .NET Framwork, decompress it to an arbitrary folder and execute it.<br>
Other software is required when using some functions.

| Features | Required Software |
| :--- | :--- |
| USB Monitor | USBPcap ([http://desowin.org/usbpcap/](http://desowin.org/usbpcap/ "USBPcap")) |
| Ehernet | WinPcap ([https://www.winpcap.org/](https://www.winpcap.org/)) |

### Uninstall

Ratatoskr does not use the registry.<br>
Delete the file decompressed by installation and delete the configuration file.
The configuration file exists in the following location.<br>
`%APPDATA%\Ratatoskr`

# Screenshots

![](./doc/_images/basic.png)

# Authors

* **Toki-H-K** (https://github.com/tokihk)

# License

GNU GENERAL PUBLIC LICENSE v3

# [Document](./doc/index)
