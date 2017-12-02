# WinHotspot
Windows wireless hotspot software - is little collection of tools, allowing to create hotspot on your Windows OS.

# Requirements
For just runing compiled versions you need:
 - Windows 7 or later version.
 - WiFi network card with drivers supporting hosted network
 - .Net framework 2.0

If you want to compile from sources:

 - Visual Studio 2017 with classic C++ application support

#Usage
For command line utility (whc.exe):

    Usage: whc.exe [OPTIONS]
    
    Options:
    /START SSID PASSWORD [CLIENTS = 8] - Start wifi hotspot with settings
    /STOP                              - Stop wifi hotspot
    /INFO                              - Information about current status of wifi hotspot
    /NOVERBOSE                         - Disable verbose output (usefull for scripts)
    /?                                 - Display this help
 

----------

**Creating network example:**

> whc /start "my network" "secretpassword"

This command will tries to launch hotspot with name "my network" and password "secretpassword". If error occurs text message will be displayed on screen. Also you can specify maximum clients number with:

> whc /start "my network" "secretpassword" 2

This command also will try to launch hotspot with same parameters, but only for 2 clients maximum.

----------


**Stopping the network example:**

> whc /stop

This command will tries to stop any current hotspot. If error occurs text message will be displayed on screen. 

----------

**Getting information about network:**

> whc /info

Will print information about network (without password):

    SSID:           MY-DESKTOP
    Active:         0
    Max clients:    8
    MAC:            0:0:0:0:0:0
 
 ----------


**'noverbose' mode example:**

> whc /info /noverbose

Will print information about network separated by semicolon symbol ';':

    DESKTOP-US16UID;0;8;0:0:0:0:0:0
Also application return error codes:

|Code| Description                         | Reason |
|----|-------------------------------------|--------|
| 0  |      OK                             | 
| -1 | Nothing done                        | Probably error in arguments 
| -2 |Wlan failed to be properly configured| Wlan device don't support wifi hosting or you must restart network card
| -3 |Password are not acceptable          | Password must be coded with ANSI symbols only with length from 8 to 32 symbols
| -4 |SSID are not valid                   | Choose other SSID name or restart network card
| -5 |Failed to start                      | Wlan device don't support wifi hosting or you must restart network card
| -6 |Failed to stop                       | Wlan device don't support wifi hosting or you must restart network card
| -7 |Failed to list                       |
| -8 |Failed to gather information         | Can't gather information. Card not configured properly

# Little FAQ

Q: May i use your source code in my projects?

A: Yes, you can. The only drawback - mention me in readme and/or license file

Q: I can't start wifi network!

A: Try reboot your computer or restart your wlan network card. Also your network card maybe not supported (can't create hosted network)


