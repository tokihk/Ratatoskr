<link href="../params.css" rel="stylesheet" />

# <span class="app-name" /> User's manual

![image](../_images/app_icon_128x128.png) ![image](../_images/app_logo_600x120.png)

<span class = "app-name" /> is a development support tool that can communicate with various devices that can be connected to a personal computer. <br>
It provides a general-purpose communication debugging environment for various communication ports such as serial port and TCP / UDP.

You can intuitively operate input / output to / from ports by easy-to-view packet view and interface that emphasizes operability.

<span class = "app-name" /> has the following features.

* Supports various communication such as serial port, TCP / IP, UDP
  * Serial Port
  * TCP / IP Server or Client
  * UDP
  * USB Capture
* Supports packet view according to usage, such as packet unit and data unit
* Simultaneous connection to multiple ports
* Simultaneous display of multiple types of packet views
* Saving / loading / saving packet logs automatically
* Real-time packet control such as filtering, packet combining
* Redirect transfer from port to port

![](../_images/basic.png)

## Copyright

* <span class = "author" /> - <span class = "author-mail" />

## license

GPL 3 (GNU GENERAL PUBLIC LICENSE v 3)

## Operating environment

* .NET Framework 4.7

## Installation

After installing .NET Framwork, decompress it to an arbitrary folder and execute it.

Other software is required when using some functions.

| Features | Required Software |
| :--- | :--- |
| USB Monitor | USBPcap ([http://desowin.org/usbpcap/](http://desowin.org/usbpcap/ "USBPcap")) |
| Ehernet | WinPcap ([https://www.winpcap.org/](https://www.winpcap.org/)) |

### Uninstall

<span class = "app-name" /> does not use the registry.

Delete the file decompressed by installation and delete the configuration file.
The configuration file exists in the following location.

<code>%APPDATA%\\<span class="app-name" /></code>