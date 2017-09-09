﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace Ratatoskr.Native
{
    internal static class NativeMethods
    {
        public static readonly IntPtr Null = IntPtr.Zero;

        public const int MAX_PATH = 260;

        public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        public static UInt32 CTL_CODE(uint DeviceType, uint Function, uint Method, uint Access)
        {
            return (((DeviceType) << 16) | ((Access) << 14) | ((Function) << 2) | (Method));
        }

        public const UInt32 ERROR_IO_PENDING = 997;

        public static readonly IntPtr HWND_TOP       = new IntPtr(0);
        public static readonly IntPtr HWND_BOTTOM    = new IntPtr(1);
        public static readonly IntPtr HWND_TOPMOST   = new IntPtr(-1);
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

        public const UInt32 SWP_NOSIZE           = 0x0001;
        public const UInt32 SWP_NOMOVE           = 0x0002;
        public const UInt32 SWP_NOZORDER         = 0x0004;
        public const UInt32 SWP_NOREDRAW         = 0x0008;
        public const UInt32 SWP_NOACTIVATE       = 0x0010;
        public const UInt32 SWP_FRAMECHANGED     = 0x0020;  /* The frame changed: send WM_NCCALCSIZE */
        public const UInt32 SWP_SHOWWINDOW       = 0x0040;
        public const UInt32 SWP_HIDEWINDOW       = 0x0080;
        public const UInt32 SWP_NOCOPYBITS       = 0x0100;
        public const UInt32 SWP_NOOWNERZORDER    = 0x0200;  /* Don't do owner Z ordering */
        public const UInt32 SWP_NOSENDCHANGING   = 0x0400;  /* Don't send WM_WINDOWPOSCHANGING */

        public const UInt32 SWP_DRAWFRAME       = SWP_FRAMECHANGED;
        public const UInt32 SWP_NOREPOSITION    = SWP_NOOWNERZORDER;

        public const UInt32 WM_SETREDRAW        = 0x000B;
        public const UInt32 WM_DEVMODECHANGE    = 0x001B;
        public const UInt32 WM_ACTIVATEAPP      = 0x001C;
        public const UInt32 WM_FONTCHANGE       = 0x001D;
        public const UInt32 WM_TIMECHANGE       = 0x001E;
        public const UInt32 WM_CANCELMODE       = 0x001F;
        public const UInt32 WM_SETCURSOR        = 0x0020;
        public const UInt32 WM_MOUSEACTIVATE    = 0x0021;
        public const UInt32 WM_CHILDACTIVATE    = 0x0022;
        public const UInt32 WM_QUEUESYNC        = 0x0023;

        public const UInt32 WM_GETMINMAXINFO    = 0x0024;

        public const Int32 WS_EX_DLGMODALFRAME  = 0x00000001;
        public const Int32 WS_EX_NOPARENTNOTIFY = 0x00000004;
        public const Int32 WS_EX_TOPMOST        = 0x00000008;
        public const Int32 WS_EX_ACCEPTFILES    = 0x00000010;
        public const Int32 WS_EX_TRANSPARENT    = 0x00000020;

        public const Int32 WS_EX_NOACTIVATE     = 0x08000000;

        public const UInt32 MA_ACTIVATE         = 1;
        public const UInt32 MA_ACTIVATEANDEAT   = 2;
        public const UInt32 MA_NOACTIVATE       = 3;
        public const UInt32 MA_NOACTIVATEANDEAT = 4;

        public const UInt32 SC_MANAGER_ALL_ACCESS         = 0xF003FU;
        public const UInt32 SC_MANAGER_CONNECT            = 0x00001U;
        public const UInt32 SC_MANAGER_CREATE_SERVICE     = 0x00002U;
        public const UInt32 SC_MANAGER_ENUMERATE_SERVICE  = 0x00004U;
        public const UInt32 SC_MANAGER_LOCK               = 0x00008U;
        public const UInt32 SC_MANAGER_QUERY_LOCK_STATUS  = 0x00010U;
        public const UInt32 SC_MANAGER_MODIFY_BOOT_CONFIG = 0x00020U;

        public const UInt32 SERVICE_ALL_ACCESS           = 0x000F01FFU;
        public const UInt32 SERVICE_CHANGE_CONFIG        = 0x00000002U;
        public const UInt32 SERVICE_ENUMERATE_DEPENDENTS = 0x00000008U;
        public const UInt32 SERVICE_INTERROGATE          = 0x00000080U;
        public const UInt32 SERVICE_PAUSE_CONTINUE       = 0x00000040U;
        public const UInt32 SERVICE_QUERY_CONFIG         = 0x00000001U;
        public const UInt32 SERVICE_QUERY_STATUS         = 0x00000004U;
        public const UInt32 SERVICE_START                = 0x00000010U;
        public const UInt32 SERVICE_STOP                 = 0x00000020U;
        public const UInt32 SERVICE_USER_DEFINED_CONTROL = 0x00000100U;

        public const UInt32 SERVICE_KERNEL_DRIVER       = 0x00000001U;
        public const UInt32 SERVICE_FILE_SYSTEM_DRIVER  = 0x00000002U;
        public const UInt32 SERVICE_ADAPTER             = 0x00000004U;
        public const UInt32 SERVICE_RECOGNIZER_DRIVER   = 0x00000008U;
        public const UInt32 SERVICE_WIN32_OWN_PROCESS   = 0x00000010U;
        public const UInt32 SERVICE_WIN32_SHARE_PROCESS = 0x00000020U;
        public const UInt32 SERVICE_INTERACTIVE_PROCESS = 0x00000100U;

        public const UInt32 SERVICE_BOOT_START   = 0x00000000U;
        public const UInt32 SERVICE_SYSTEM_START = 0x00000001U;
        public const UInt32 SERVICE_AUTO_START   = 0x00000002U;
        public const UInt32 SERVICE_DEMAND_START = 0x00000003U;
        public const UInt32 SERVICE_DISABLED     = 0x00000004U;

        public const UInt32 SERVICE_ERROR_IGNORE   = 0x00000000U;
        public const UInt32 SERVICE_ERROR_NORMAL   = 0x00000001U;
        public const UInt32 SERVICE_ERROR_SEVERE   = 0x00000002U;
        public const UInt32 SERVICE_ERROR_CRITICAL = 0x00000003U;

        public const UInt32 SERVICE_CONTROL_CONTINUE       = 0x00000003U;
        public const UInt32 SERVICE_CONTROL_INTERROGATE    = 0x00000004U;
        public const UInt32 SERVICE_CONTROL_NETBINDADD     = 0x00000007U;
        public const UInt32 SERVICE_CONTROL_NETBINDDISABLE = 0x0000000AU;
        public const UInt32 SERVICE_CONTROL_NETBINDENABLE  = 0x00000009U;
        public const UInt32 SERVICE_CONTROL_NETBINDREMOVE  = 0x00000008U;
        public const UInt32 SERVICE_CONTROL_PARAMCHANGE    = 0x00000006U;
        public const UInt32 SERVICE_CONTROL_PAUSE          = 0x00000002U;
        public const UInt32 SERVICE_CONTROL_STOP           = 0x00000001U;

        public const UInt32 INFINITE       = 0xFFFFFFFF;
        public const UInt32 WAIT_ABANDONED = 0x00000080;
        public const UInt32 WAIT_OBJECT_0  = 0x00000000;
        public const UInt32 WAIT_TIMEOUT   = 0x00000102;

        public const UInt32 GENERIC_READ    = 0x80000000U;
        public const UInt32 GENERIC_WRITE   = 0x40000000U;
        public const UInt32 GENERIC_EXECUTE = 0x20000000U;
        public const UInt32 GENERIC_ALL     = 0x10000000U;

        public const UInt32 FILE_SHARE_READ               = 0x00000001;
        public const UInt32 FILE_SHARE_WRITE              = 0x00000002;
        public const UInt32 FILE_SHARE_DELETE             = 0x00000004;

        public const UInt32 FILE_FLAG_WRITE_THROUGH       = 0x80000000U;
        public const UInt32 FILE_FLAG_OVERLAPPED          = 0x40000000U;
        public const UInt32 FILE_FLAG_NO_BUFFERING        = 0x20000000U;
        public const UInt32 FILE_FLAG_RANDOM_ACCESS       = 0x10000000U;
        public const UInt32 FILE_FLAG_SEQUENTIAL_SCAN     = 0x08000000U;
        public const UInt32 FILE_FLAG_DELETE_ON_CLOSE     = 0x04000000U;
        public const UInt32 FILE_FLAG_BACKUP_SEMANTICS    = 0x02000000U;
        public const UInt32 FILE_FLAG_POSIX_SEMANTICS     = 0x01000000U;
        public const UInt32 FILE_FLAG_OPEN_REPARSE_POINT  = 0x00200000U;
        public const UInt32 FILE_FLAG_OPEN_NO_RECALL      = 0x00100000U;
        public const UInt32 FILE_FLAG_FIRST_PIPE_INSTANCE = 0x00080000U;

        public const UInt32 FILE_ATTRIBUTE_READONLY  = 0x00000001;
        public const UInt32 FILE_ATTRIBUTE_HIDDEN    = 0x00000002;
        public const UInt32 FILE_ATTRIBUTE_SYSTEM    = 0x00000004;
        public const UInt32 FILE_ATTRIBUTE_ARCHIVE   = 0x00000020;
        public const UInt32 FILE_ATTRIBUTE_NORMAL    = 0x00000080;
        public const UInt32 FILE_ATTRIBUTE_TEMPORARY = 0x00000100;
        public const UInt32 FILE_ATTRIBUTE_OFFLINE   = 0x00001000;
        public const UInt32 FILE_ATTRIBUTE_ENCRYPTED = 0x00004000;

        public const UInt32 CREATE_NEW        = 1;
        public const UInt32 CREATE_ALWAYS     = 2;
        public const UInt32 OPEN_EXISTING     = 3;
        public const UInt32 OPEN_ALWAYS       = 4;
        public const UInt32 TRUNCATE_EXISTING = 5;

        public const byte NOPARITY    = 0;
        public const byte ODDPARITY   = 1;
        public const byte EVENPARITY  = 2;
        public const byte MASKPARITY  = 3;
        public const byte SPACEPARITY = 4;

        public const byte ONESTOPBIT   = 0;
        public const byte ONE5STOPBITS = 1;
        public const byte TWOSTOPBIT   = 2;

        public const UInt32 DTR_CONTROL_DISABLE   = 0x00;
        public const UInt32 DTR_CONTROL_ENABLE    = 0x01;
        public const UInt32 DTR_CONTROL_HANDSHAKE = 0x02;

        public const UInt32 RTS_CONTROL_DISABLE   = 0x00;
        public const UInt32 RTS_CONTROL_ENABLE    = 0x01;
        public const UInt32 RTS_CONTROL_HANDSHAKE = 0x02;
        public const UInt32 RTS_CONTROL_TOGGLE    = 0x03;

        public const UInt32 EV_RXCHAR   = 0x0001;
        public const UInt32 EV_RXFLAG   = 0x0002;
        public const UInt32 EV_TXEMPTY  = 0x0004;
        public const UInt32 EV_CTS      = 0x0008;
        public const UInt32 EV_DSR      = 0x0010;
        public const UInt32 EV_RLSD     = 0x0020;
        public const UInt32 EV_BREAK    = 0x0040;
        public const UInt32 EV_ERR      = 0x0080;
        public const UInt32 EV_RING     = 0x0100;
        public const UInt32 EV_PERR     = 0x0200;  // Printer error occured
        public const UInt32 EV_RX80FULL = 0x0400;  // Receive buffer is 80 percent full
        public const UInt32 EV_EVENT1   = 0x0800;  // Provider specific event 1
        public const UInt32 EV_EVENT2   = 0x1000;  // Provider specific event 2

        public const UInt32 CE_RXOVER   = 0x0001;  // Receive Queue overflow
        public const UInt32 CE_OVERRUN  = 0x0002;  // Receive Overrun Error
        public const UInt32 CE_RXPARITY = 0x0004;  // Receive Parity Error
        public const UInt32 CE_FRAME    = 0x0008;  // Receive Framing error
        public const UInt32 CE_BREAK    = 0x0010;  // Break Detected
        public const UInt32 CE_TXFULL   = 0x0100;  // TX Queue is full
        public const UInt32 CE_PTO      = 0x0200;  // LPTx Timeout
        public const UInt32 CE_IOE      = 0x0400;  // LPTx I/O Error
        public const UInt32 CE_DNS      = 0x0800;  // LPTx Device not selected
        public const UInt32 CE_OOP      = 0x1000;  // LPTx Out-Of-Paper
        public const UInt32 CE_MODE     = 0x8000;  // Requested mode unsupported

        public const UInt32 DIGCF_DEFAULT         = 0x00000001;  // only valid with DIGCF_DEVICEINTERFACE
        public const UInt32 DIGCF_PRESENT         = 0x00000002;
        public const UInt32 DIGCF_ALLCLASSES      = 0x00000004;
        public const UInt32 DIGCF_PROFILE         = 0x00000008;
        public const UInt32 DIGCF_DEVICEINTERFACE = 0x00000010;

        public const UInt32 SPDRP_DEVICEDESC                  = 0x00000000;  // DeviceDesc = R/W;
        public const UInt32 SPDRP_HARDWAREID                  = 0x00000001;  // HardwareID = R/W;
        public const UInt32 SPDRP_COMPATIBLEIDS               = 0x00000002;  // CompatibleIDs = R/W;
        public const UInt32 SPDRP_UNUSED0                     = 0x00000003;  // unused
        public const UInt32 SPDRP_SERVICE                     = 0x00000004;  // Service = R/W;
        public const UInt32 SPDRP_UNUSED1                     = 0x00000005;  // unused
        public const UInt32 SPDRP_UNUSED2                     = 0x00000006;  // unused
        public const UInt32 SPDRP_CLASS                       = 0x00000007;  // Class = R--tied to ClassGUID;
        public const UInt32 SPDRP_CLASSGUID                   = 0x00000008;  // ClassGUID = R/W;
        public const UInt32 SPDRP_DRIVER                      = 0x00000009;  // Driver = R/W;
        public const UInt32 SPDRP_CONFIGFLAGS                 = 0x0000000A;  // ConfigFlags = R/W;
        public const UInt32 SPDRP_MFG                         = 0x0000000B;  // Mfg = R/W;
        public const UInt32 SPDRP_FRIENDLYNAME                = 0x0000000C;  // FriendlyName = R/W;
        public const UInt32 SPDRP_LOCATION_INFORMATION        = 0x0000000D;  // LocationInformation = R/W;
        public const UInt32 SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = 0x0000000E;  // PhysicalDeviceObjectName = R;
        public const UInt32 SPDRP_CAPABILITIES                = 0x0000000F;  // Capabilities = R;
        public const UInt32 SPDRP_UI_NUMBER                   = 0x00000010;  // UiNumber = R;
        public const UInt32 SPDRP_UPPERFILTERS                = 0x00000011;  // UpperFilters = R/W;
        public const UInt32 SPDRP_LOWERFILTERS                = 0x00000012;  // LowerFilters = R/W;
        public const UInt32 SPDRP_BUSTYPEGUID                 = 0x00000013;  // BusTypeGUID = R;
        public const UInt32 SPDRP_LEGACYBUSTYPE               = 0x00000014;  // LegacyBusType = R;
        public const UInt32 SPDRP_BUSNUMBER                   = 0x00000015;  // BusNumber = R;
        public const UInt32 SPDRP_ENUMERATOR_NAME             = 0x00000016;  // Enumerator Name = R;
        public const UInt32 SPDRP_SECURITY                    = 0x00000017;  // Security = R/W, binary form;
        public const UInt32 SPDRP_SECURITY_SDS                = 0x00000018;  // Security = W, SDS form;
        public const UInt32 SPDRP_DEVTYPE                     = 0x00000019;  // Device Type = R/W;
        public const UInt32 SPDRP_EXCLUSIVE                   = 0x0000001A;  // Device is exclusive-access = R/W;
        public const UInt32 SPDRP_CHARACTERISTICS             = 0x0000001B;  // Device Characteristics = R/W;
        public const UInt32 SPDRP_ADDRESS                     = 0x0000001C;  // Device Address = R;
        public const UInt32 SPDRP_UI_NUMBER_DESC_FORMAT       = 0X0000001D;  // UiNumberDescFormat = R/W;
        public const UInt32 SPDRP_DEVICE_POWER_DATA           = 0x0000001E;  // Device Power Data = R;
        public const UInt32 SPDRP_REMOVAL_POLICY              = 0x0000001F;  // Removal Policy = R;
        public const UInt32 SPDRP_REMOVAL_POLICY_HW_DEFAULT   = 0x00000020;  // Hardware Removal Policy = R;
        public const UInt32 SPDRP_REMOVAL_POLICY_OVERRIDE     = 0x00000021;  // Removal Policy Override = RW;
        public const UInt32 SPDRP_INSTALL_STATE               = 0x00000022;  // Device Install State = R;
        public const UInt32 SPDRP_LOCATION_PATHS              = 0x00000023;  // Device Location Paths = R;
        public const UInt32 SPDRP_BASE_CONTAINERID            = 0x00000024;  // Base ContainerID = R;

        public const UInt32 SPDRP_MAXIMUM_PROPERTY            = 0x00000025;  // Upper bound on ordinals

        public const UInt32 DICS_FLAG_GLOBAL         = 0x00000001;  // make change in all hardware profiles
        public const UInt32 DICS_FLAG_CONFIGSPECIFIC = 0x00000002;  // make change in specified profile only
        public const UInt32 DICS_FLAG_CONFIGGENERAL  = 0x00000004;  // 1 or more hardware profile-specific

        public const UInt32 DIREG_DEV  = 0x00000001;          // Open/Create/Delete device key
        public const UInt32 DIREG_DRV  = 0x00000002;          // Open/Create/Delete driver key
        public const UInt32 DIREG_BOTH = 0x00000004;          // Delete both driver and Device key

        public const Int32 WSAEADDRNOTAVAIL  = 10049;

        public const UInt32 DIRECTORY_QUERY               = 0x0001;
        public const UInt32 DIRECTORY_TRAVERSE            = 0x0002;
        public const UInt32 DIRECTORY_CREATE_OBJECT       = 0x0004;
        public const UInt32 DIRECTORY_CREATE_SUBDIRECTORY = 0x0008;

        public const UInt32 STATUS_SUCCESS                 = 0x00000000;
        public const UInt32 STATUS_INVALID_INFO_CLASS      = 0xC0000003;
        public const UInt32 STATUS_NO_SUCH_USER            = 0xC0000064;
        public const UInt32 STATUS_WRONG_PASSWORD          = 0xC000006A;
        public const UInt32 STATUS_PASSWORD_RESTRICTION    = 0xC000006C;
        public const UInt32 STATUS_LOGON_FAILURE           = 0xC000006D;
        public const UInt32 STATUS_ACCOUNT_RESTRICTION     = 0xC000006E;
        public const UInt32 STATUS_INVALID_LOGON_HOURS     = 0xC000006F;
        public const UInt32 STATUS_INVALID_WORKSTATION     = 0xC0000070;
        public const UInt32 STATUS_PASSWORD_EXPIRED        = 0xC0000071;
        public const UInt32 STATUS_ACCOUNT_DISABLED        = 0xC0000072;
        public const UInt32 STATUS_INSUFFICIENT_RESOURCES  = 0xC000009A;
        public const UInt32 STATUS_ACCOUNT_EXPIRED         = 0xC0000193;
        public const UInt32 STATUS_PASSWORD_MUST_CHANGE    = 0xC0000224;
        public const UInt32 STATUS_ACCOUNT_LOCKED_OUT      = 0xC0000234;

        public const UInt32 FILE_DEVICE_BEEP                = 0x00000001;
        public const UInt32 FILE_DEVICE_CD_ROM              = 0x00000002;
        public const UInt32 FILE_DEVICE_CD_ROM_FILE_SYSTEM  = 0x00000003;
        public const UInt32 FILE_DEVICE_CONTROLLER          = 0x00000004;
        public const UInt32 FILE_DEVICE_DATALINK            = 0x00000005;
        public const UInt32 FILE_DEVICE_DFS                 = 0x00000006;
        public const UInt32 FILE_DEVICE_DISK                = 0x00000007;
        public const UInt32 FILE_DEVICE_DISK_FILE_SYSTEM    = 0x00000008;
        public const UInt32 FILE_DEVICE_FILE_SYSTEM         = 0x00000009;
        public const UInt32 FILE_DEVICE_INPORT_PORT         = 0x0000000a;
        public const UInt32 FILE_DEVICE_KEYBOARD            = 0x0000000b;
        public const UInt32 FILE_DEVICE_MAILSLOT            = 0x0000000c;
        public const UInt32 FILE_DEVICE_MIDI_IN             = 0x0000000d;
        public const UInt32 FILE_DEVICE_MIDI_OUT            = 0x0000000e;
        public const UInt32 FILE_DEVICE_MOUSE               = 0x0000000f;
        public const UInt32 FILE_DEVICE_MULTI_UNC_PROVIDER  = 0x00000010;
        public const UInt32 FILE_DEVICE_NAMED_PIPE          = 0x00000011;
        public const UInt32 FILE_DEVICE_NETWORK             = 0x00000012;
        public const UInt32 FILE_DEVICE_NETWORK_BROWSER     = 0x00000013;
        public const UInt32 FILE_DEVICE_NETWORK_FILE_SYSTEM = 0x00000014;
        public const UInt32 FILE_DEVICE_NULL                = 0x00000015;
        public const UInt32 FILE_DEVICE_PARALLEL_PORT       = 0x00000016;
        public const UInt32 FILE_DEVICE_PHYSICAL_NETCARD    = 0x00000017;
        public const UInt32 FILE_DEVICE_PRINTER             = 0x00000018;
        public const UInt32 FILE_DEVICE_SCANNER             = 0x00000019;
        public const UInt32 FILE_DEVICE_SERIAL_MOUSE_PORT   = 0x0000001a;
        public const UInt32 FILE_DEVICE_SERIAL_PORT         = 0x0000001b;
        public const UInt32 FILE_DEVICE_SCREEN              = 0x0000001c;
        public const UInt32 FILE_DEVICE_SOUND               = 0x0000001d;
        public const UInt32 FILE_DEVICE_STREAMS             = 0x0000001e;
        public const UInt32 FILE_DEVICE_TAPE                = 0x0000001f;
        public const UInt32 FILE_DEVICE_TAPE_FILE_SYSTEM    = 0x00000020;
        public const UInt32 FILE_DEVICE_TRANSPORT           = 0x00000021;
        public const UInt32 FILE_DEVICE_UNKNOWN             = 0x00000022;
        public const UInt32 FILE_DEVICE_VIDEO               = 0x00000023;
        public const UInt32 FILE_DEVICE_VIRTUAL_DISK        = 0x00000024;
        public const UInt32 FILE_DEVICE_WAVE_IN             = 0x00000025;
        public const UInt32 FILE_DEVICE_WAVE_OUT            = 0x00000026;
        public const UInt32 FILE_DEVICE_8042_PORT           = 0x00000027;
        public const UInt32 FILE_DEVICE_NETWORK_REDIRECTOR  = 0x00000028;
        public const UInt32 FILE_DEVICE_BATTERY             = 0x00000029;
        public const UInt32 FILE_DEVICE_BUS_EXTENDER        = 0x0000002a;
        public const UInt32 FILE_DEVICE_MODEM               = 0x0000002b;
        public const UInt32 FILE_DEVICE_VDM                 = 0x0000002c;
        public const UInt32 FILE_DEVICE_MASS_STORAGE        = 0x0000002d;
        public const UInt32 FILE_DEVICE_SMB                 = 0x0000002e;
        public const UInt32 FILE_DEVICE_KS                  = 0x0000002f;
        public const UInt32 FILE_DEVICE_CHANGER             = 0x00000030;
        public const UInt32 FILE_DEVICE_SMARTCARD           = 0x00000031;
        public const UInt32 FILE_DEVICE_ACPI                = 0x00000032;
        public const UInt32 FILE_DEVICE_DVD                 = 0x00000033;
        public const UInt32 FILE_DEVICE_FULLSCREEN_VIDEO    = 0x00000034;
        public const UInt32 FILE_DEVICE_DFS_FILE_SYSTEM     = 0x00000035;
        public const UInt32 FILE_DEVICE_DFS_VOLUME          = 0x00000036;
        public const UInt32 FILE_DEVICE_SERENUM             = 0x00000037;
        public const UInt32 FILE_DEVICE_TERMSRV             = 0x00000038;
        public const UInt32 FILE_DEVICE_KSEC                = 0x00000039;
        public const UInt32 FILE_DEVICE_FIPS                = 0x0000003A;
        public const UInt32 FILE_DEVICE_INFINIBAND          = 0x0000003B;
        public const UInt32 FILE_DEVICE_VMBUS               = 0x0000003E;
        public const UInt32 FILE_DEVICE_CRYPT_PROVIDER      = 0x0000003F;
        public const UInt32 FILE_DEVICE_WPD                 = 0x00000040;
        public const UInt32 FILE_DEVICE_BLUETOOTH           = 0x00000041;
        public const UInt32 FILE_DEVICE_MT_COMPOSITE        = 0x00000042;
        public const UInt32 FILE_DEVICE_MT_TRANSPORT        = 0x00000043;
        public const UInt32 FILE_DEVICE_BIOMETRIC           = 0x00000044;
        public const UInt32 FILE_DEVICE_PMI                 = 0x00000045;
        public const UInt32 FILE_DEVICE_EHSTOR              = 0x00000046;
        public const UInt32 FILE_DEVICE_DEVAPI              = 0x00000047;
        public const UInt32 FILE_DEVICE_GPIO                = 0x00000048;
        public const UInt32 FILE_DEVICE_USBEX               = 0x00000049;
        public const UInt32 FILE_DEVICE_CONSOLE             = 0x00000050;
        public const UInt32 FILE_DEVICE_NFP                 = 0x00000051;
        public const UInt32 FILE_DEVICE_SYSENV              = 0x00000052;
        public const UInt32 FILE_DEVICE_VIRTUAL_BLOCK       = 0x00000053;
        public const UInt32 FILE_DEVICE_POINT_OF_SERVICE    = 0x00000054;
        public const UInt32 FILE_DEVICE_STORAGE_REPLICATION = 0x00000055;
        public const UInt32 FILE_DEVICE_TRUST_ENV           = 0x00000056;
        public const UInt32 FILE_DEVICE_UCM                 = 0x00000057;
        public const UInt32 FILE_DEVICE_UCMTCPCI            = 0x00000058;

        public const UInt32 METHOD_BUFFERED   = 0;
        public const UInt32 METHOD_IN_DIRECT  = 1;
        public const UInt32 METHOD_OUT_DIRECT = 2;
        public const UInt32 METHOD_NEITHER    = 3;

        public const UInt32 FILE_ANY_ACCESS     = 0;
        public const UInt32 FILE_SPECIAL_ACCESS = FILE_ANY_ACCESS;
        public const UInt32 FILE_READ_ACCESS    = 0x0001;
        public const UInt32 FILE_WRITE_ACCESS   = 0x0002;

        [Flags]
        public enum RegOption
        {
            NonVolatile   = 0x0,
            Volatile      = 0x1,
            CreateLink    = 0x2,
            BackupRestore = 0x4,
            OpenLink      = 0x8
        }

        public const UInt32 KEY_QUERY_VALUE        = 0x0001;
        public const UInt32 KEY_SET_VALUE          = 0x0002;
        public const UInt32 KEY_CREATE_SUB_KEY     = 0x0004;
        public const UInt32 KEY_ENUMERATE_SUB_KEYS = 0x0008;
        public const UInt32 KEY_NOTIFY             = 0x0010;
        public const UInt32 KEY_CREATE_LINK        = 0x0020;
        public const UInt32 KEY_WOW64_32KEY        = 0x0200;
        public const UInt32 KEY_WOW64_64KEY        = 0x0100;
        public const UInt32 KEY_WOW64_RES          = 0x0300;
        public const UInt32 KEY_READ               = 0x00020019;
        public const UInt32 KEY_WRITE              = 0x00020006;
        public const UInt32 KEY_EXECUTE            = 0x00020019;
        public const UInt32 KEY_ALL_ACCESS         = 0x000f003f;


        public enum RegResult
        {
            CreatedNewKey     = 0x00000001,
            OpenedExistingKey = 0x00000002
        }

        public const UInt32 TIME_ONESHOT  = 0;
        public const UInt32 TIME_PERIDOIC = 1;


        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public UInt16 wYear;
            public UInt16 wMonth;
            public UInt16 wDayOfWeek;
            public UInt16 wDay;
            public UInt16 wHour;
            public UInt16 wMinute;
            public UInt16 wSecond;
            public UInt16 wMilliseconds;

            public DateTime ToDateTime()
            {
                return (new DateTime(
                    wYear,
                    wMonth,
                    wDay,
                    wHour,
                    wMinute,
                    wSecond,
                    wMilliseconds));
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public Int32 x;
            public Int32 y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct UNICODE_STRING : IDisposable
        {
            public ushort Length;
            public ushort MaximumLength;
            private IntPtr buffer;

            public UNICODE_STRING(string s)
            {
                Length = (ushort)(s.Length * 2);
                MaximumLength = (ushort)(Length + 2);
                buffer = Marshal.StringToHGlobalUni(s);
            }

            public void Dispose()
            {
                Marshal.FreeHGlobal(buffer);
                buffer = IntPtr.Zero;
            }

            public override string ToString()
            {
                return Marshal.PtrToStringUni(buffer);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OBJECT_DIRECTORY_INFORMATION
        {
           public UNICODE_STRING Name;
           public UNICODE_STRING TypeName;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SERVICE_STATUS
        {
            public UInt32 dwServiceType;
            public UInt32 dwCurrentState;
            public UInt32 dwControlsAccepted;
            public UInt32 dwWin32ExitCode;
            public UInt32 dwServiceSpecificExitCode;
            public UInt32 dwCheckPoint;
            public UInt32 dwWaitHint;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DCB
        {
            public UInt32 DCBlength;
            public UInt32 BaudRate;
            public UInt32 Flags;
            public UInt16 wReserved;
            public UInt16 XonLim;
            public UInt16 XoffLim;
            public byte ByteSize;
            public byte Parity;
            public byte StopBits;
            public sbyte XonChar;
            public sbyte XoffChar;
            public sbyte ErrorChar;
            public sbyte EofChar;
            public sbyte EvtChar;
            public UInt16 wReserved1;

            public enum FlagsParamOffset
            {
                Binary           = 31,
                Parity           = 30,
                OutxCtsFlow      = 29,
                OutxDsrFlow      = 28,
                DtrControl       = 26,
                DsrSensitivity   = 25,
                TXContinueOnXoff = 24,
                OutX             = 23,
                InX              = 22,
                ErrorChar        = 21,
                Null             = 20,
                RtsControl       = 18,
                AbortOnError     = 17,
                Dummy2           = 0,
            }

            public UInt32 fBinary
            {
                get { return ((Flags >> (int)FlagsParamOffset.Binary) & 1u); }
                set { Flags = (Flags & (~(1u << (int)FlagsParamOffset.Binary))) | ((value & 1u) << (int)FlagsParamOffset.Binary); }
            }

            public UInt32 fParity
            {
                get { return ((Flags >> (int)FlagsParamOffset.Parity) & 1u); }
                set { Flags = (Flags & (~(1u << (int)FlagsParamOffset.Parity))) | ((value & 1u) << (int)FlagsParamOffset.Parity); }
            }

            public UInt32 fOutxCtsFlow
            {
                get { return ((Flags >> (int)FlagsParamOffset.OutxCtsFlow) & 1u); }
                set { Flags = (Flags & (~(1u << (int)FlagsParamOffset.OutxCtsFlow))) | ((value & 1u) << (int)FlagsParamOffset.OutxCtsFlow); }
            }

            public UInt32 fOutxDsrFlow
            {
                get { return ((Flags >> (int)FlagsParamOffset.OutxDsrFlow) & 1u); }
                set { Flags = (Flags & (~(1u << (int)FlagsParamOffset.OutxDsrFlow))) | ((value & 1u) << (int)FlagsParamOffset.OutxDsrFlow); }
            }

            public UInt32 fRtsControl
            {
                get { return ((Flags >> (int)FlagsParamOffset.RtsControl) & 2u); }
                set { Flags = (Flags & (~(3u << (int)FlagsParamOffset.RtsControl))) | ((value & 3u) << (int)FlagsParamOffset.RtsControl); }
            }

            public UInt32 fDtrControl
            {
                get { return ((Flags >> (int)FlagsParamOffset.DtrControl) & 2u); }
                set { Flags = (Flags & (~(3u << (int)FlagsParamOffset.DtrControl))) | ((value & 3u) << (int)FlagsParamOffset.DtrControl); }
            }

            public UInt32 fDsrSensitivity
            {
                get { return ((Flags >> (int)FlagsParamOffset.DsrSensitivity) & 1u); }
                set { Flags = (Flags & (~(1u << (int)FlagsParamOffset.DsrSensitivity))) | ((value & 1u) << (int)FlagsParamOffset.DsrSensitivity); }
            }

            public UInt32 fTXContinueOnXoff
            {
                get { return ((Flags >> (int)FlagsParamOffset.TXContinueOnXoff) & 1u); }
                set { Flags = (Flags & (~(1u << (int)FlagsParamOffset.TXContinueOnXoff))) | ((value & 1u) << (int)FlagsParamOffset.TXContinueOnXoff); }
            }

            public UInt32 fOutX
            {
                get { return ((Flags >> (int)FlagsParamOffset.OutX) & 1u); }
                set { Flags = (Flags & (~(1u << (int)FlagsParamOffset.OutX))) | ((value & 1u) << (int)FlagsParamOffset.OutX); }
            }

            public UInt32 fInX
            {
                get { return ((Flags >> (int)FlagsParamOffset.InX) & 1u); }
                set { Flags = (Flags & (~(1u << (int)FlagsParamOffset.InX))) | ((value & 1u) << (int)FlagsParamOffset.InX); }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COMSTAT
        {
            public UInt32 Flags;
            public UInt32 cbInQue;
            public UInt32 cbOutQue;

            public enum FlagsParamOffset
            {
                CtsHold  = 31,
                DsrHold  = 30,
                RlsHold  = 29,
                XoffHold = 28,
                XoffSent = 27,
                Eof      = 26,
                Txim     = 25,
                Reserved = 0,
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COMMPROP
        {
            public UInt16 wPacketLength;
            public UInt16 wPacketVersion;
            public UInt32 dwServiceMask;
            public UInt32 dwReserved1;
            public UInt32 dwMaxTxQueue;
            public UInt32 dwMaxRxQueue;
            public UInt32 dwMaxBaud;
            public UInt32 dwProvSubType;
            public UInt32 dwProvCapabilities;
            public UInt32 dwSettableParams;
            public UInt32 dwSettableBaud;
            public UInt16 wSettableData;
            public UInt16 wSettableStopParity;
            public UInt32 dwCurrentTxQueue;
            public UInt32 dwCurrentRxQueue;
            public UInt32 dwProvSpec1;
            public UInt32 dwProvSpec2;
            public string wcProvChar;    
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COMMTIMEOUTS
        {
             public UInt32 ReadIntervalTimeout;
             public UInt32 ReadTotalTimeoutMultiplier;
             public UInt32 ReadTotalTimeoutConstant;
             public UInt32 WriteTotalTimeoutMultiplier;
             public UInt32 WriteTotalTimeoutConstant;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SP_DEVINFO_DATA
        {
            public UInt32  cbSize;
            public Guid    ClassGuid;
            public UInt32  DevInst;
            public UIntPtr Reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct URB_HEADER
        {
            public UInt16  Length;
            public UInt16  Function;
            public Int32   Status;
            public IntPtr  UsbdDeviceHandle;
            public UInt32  UsbdFlags;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct MMTIME
        {
            [FieldOffset(0)]
            public UInt32   wType;          // indicates the contents of the union

            [FieldOffset(4)]
            public UInt32   ms;             // milliseconds
            [FieldOffset(4)]
            public UInt32   sample;         // samples
            [FieldOffset(4)]
            public UInt32   cb;             // byte count
            [FieldOffset(4)]
            public UInt32   ticks;          // ticks in MIDI stream

            // SMPTE
            [FieldOffset(4)]
            public byte     hour;           // hours
            [FieldOffset(5)]
            public byte     min;            // minutes
            [FieldOffset(6)]
            public byte     sec;            // seconds
            [FieldOffset(7)]
            public byte     frame;          // frames
            [FieldOffset(8)]
            public byte     fps;            // frames per second
            [FieldOffset(9)]
            public byte     dummy;          // pad
            [FieldOffset(10)]
            public byte     pad0;
            [FieldOffset(11)]
            public byte     pad1;

            // MIDI
            [FieldOffset(4)]
            public UInt32   songptrpos;     // song pointer position
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TIMECAPS
        {
            public UInt32   wPeriodMin;      // minimum period supported
            public UInt32   wPeriodMax;      // maximum period supported
        }

        public delegate void TIMECALLBACK
        (
            UInt32  uTimerID,
            UInt32  uMsg,
            UIntPtr dwUser,
            UIntPtr dw1,
            UIntPtr dw2
        );

        [StructLayout(LayoutKind.Sequential)]
        public struct OBJECT_ATTRIBUTES : IDisposable
        {
            public int Length;
            public IntPtr RootDirectory;
            private IntPtr objectName;
            public uint Attributes;
            public IntPtr SecurityDescriptor;
            public IntPtr SecurityQualityOfService;

            public OBJECT_ATTRIBUTES(string name, uint attrs)
            {
                Length = 0;
                RootDirectory = IntPtr.Zero;
                objectName = IntPtr.Zero;
                Attributes = attrs;
                SecurityDescriptor = IntPtr.Zero;
                SecurityQualityOfService = IntPtr.Zero;

                Length = Marshal.SizeOf(this);
                ObjectName = new UNICODE_STRING(name);
            }

            public UNICODE_STRING ObjectName
            {
                get {
                    return (UNICODE_STRING)Marshal.PtrToStructure(objectName, typeof(UNICODE_STRING));
                }

                set {
                    bool fDeleteOld = objectName != IntPtr.Zero;

                    if (!fDeleteOld) {
                        objectName = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                    }

                    Marshal.StructureToPtr(value, objectName, fDeleteOld);
                }
            }

            public void Dispose()
            {
                if (objectName != IntPtr.Zero) {
                    Marshal.DestroyStructure(objectName, typeof(UNICODE_STRING));
                    Marshal.FreeHGlobal(objectName);
                    objectName = IntPtr.Zero;
                }
            }
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        public extern static uint GetLastError();

        [DllImport("user32.dll", SetLastError=true)]
        public extern static bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, UInt32 uFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public extern static IntPtr OpenSCManager
        (
            string lpMachineName,
            string lpDatabaseName,
            UInt32 dwDesiredAccess
        );

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public extern static bool CloseServiceHandle
        (
            IntPtr hSCObject
        );

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public extern static IntPtr CreateService
        (
            IntPtr hSCManager,
            string lpServiceName,
            string lpDisplayName,
            UInt32 dwDesiredAccess,
            UInt32 dwServiceType,
            UInt32 dwStartType,
            UInt32 dwErrorControl,
            string lpBinaryPathName,
            string lpLoadOrderGroup,
            string lpdwTagId,
            string lpDependencies,
            string lpServiceStartName,
            string lpPassword
        );

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public extern static bool DeleteService
        (
            IntPtr hService
        );

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public extern static IntPtr OpenService
        (
            IntPtr hSCManager,
            string lpServiceName,
            UInt32 dwDesiredAccess
        );

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public extern static bool StartService
        (
            IntPtr hService,
            UInt32 dwNumServiceArgs,
            IntPtr lpServiceArgVectors
        );

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public extern static bool ControlService
        (
            IntPtr hService,
            UInt32 dwControl,
            ref SERVICE_STATUS lpServiceStatus
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public extern static unsafe System.IntPtr CreateEvent
        (
            IntPtr lpEventAttributes,   // セキュリティ記述子
            bool bManualReset,          // リセットのタイプ
            bool bInitialState,         // 初期状態
            string lpName               // イベントオブジェクトの名前
        );

        [DllImport("kernel32.dll")]
        public extern static bool SetEvent
        (
            IntPtr hEvent
        );

        [DllImport("kernel32.dll")]
        public extern static bool ResetEvent
        (
            IntPtr hEvent
        );

        [DllImport("kernel32.dll", SetLastError=true)]
        public extern static UInt32 WaitForSingleObject
        (
            IntPtr hHandle,             // オブジェクトのハンドル
            UInt32 dwMilliseconds       // タイムアウト時間
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public extern static unsafe System.IntPtr CreateFile
        (
            string lpFileName,                  // ファイル名
            UInt32 dwDesiredAccess,             // アクセスモード
            UInt32 dwShareMode,                 // 共有モード
            IntPtr lpSecurityAttributes,        // セキュリティ記述子
            UInt32 dwCreationDisposition,       // 作成方法
            UInt32 dwFlagsAndAttributes,        // ファイル属性
            IntPtr hTemplateFile                // テンプレートファイルのハンドル
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr CreateFileA(
            [MarshalAs(UnmanagedType.LPStr)] string filename,
            UInt32 dwDesiredAccess,             // アクセスモード
            UInt32 dwShareMode,                 // 共有モード
            IntPtr lpSecurityAttributes,        // セキュリティ記述子
            UInt32 dwCreationDisposition,       // 作成方法
            UInt32 dwFlagsAndAttributes,        // ファイル属性
            IntPtr hTemplateFile                // テンプレートファイルのハンドル
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int WaitForMultipleObjects(
            UInt32 nCount,
            IntPtr[] lpHandles,
            Boolean fWaitAll,
            UInt32 dwMilliseconds
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public extern static unsafe bool CloseHandle
        (
            IntPtr hObject   // オブジェクトのハンドル
        );

        [DllImport("kernel32", SetLastError = true)]
        public extern static unsafe bool WriteFile
        (
            IntPtr hFile,                     // ファイルのハンドル
            byte[] lpBuffer,                  // データバッファ
            uint nNumberOfBytesToWrite,       // 書き込み対象のバイト数
            out uint lpNumberOfBytesWritten,  // 書き込んだバイト数
            IntPtr lpOverlapped               // オーバーラップ構造体のバッファ
        );

        [DllImport("kernel32", SetLastError = true)]
        public extern static unsafe bool WriteFile
        (
            IntPtr hFile,                     // ファイルのハンドル
            byte[] lpBuffer,                  // データバッファ
            uint nNumberOfBytesToWrite,       // 書き込み対象のバイト数
            out uint lpNumberOfBytesWritten,  // 書き込んだバイト数
            ref NativeOverlapped lpOverlapped // オーバーラップ構造体のバッファ
        );

        [DllImport("kernel32", SetLastError = true)]
        public extern static unsafe bool ReadFile
        (
            IntPtr hFile,                 // ファイルのハンドル
            byte *lpBuffer,               // データバッファ
            uint nNumberOfBytesToRead,    // 読み取り対象のバイト数
            out uint lpNumberOfBytesRead, // 読み取ったバイト数
            IntPtr lpOverlapped           // オーバーラップ構造体のバッファ
        );

        [DllImport("kernel32", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public extern static unsafe bool ReadFile
        (
            IntPtr hFile,                 // ファイルのハンドル
            byte *lpBuffer,               // データバッファ
            uint nNumberOfBytesToRead,    // 読み取り対象のバイト数
            out uint lpNumberOfBytesRead, // 読み取ったバイト数
            ref NativeOverlapped lpOverlapped // オーバーラップ構造体のバッファ
        );

        [DllImport("kernel32", SetLastError = true)]
        public extern static unsafe bool GetOverlappedResult
        (
            IntPtr hFile,                           // ファイル、パイプ、通信デバイスのハンドル
            ref NativeOverlapped lpOverlapped,      // オーバーラップ構造体
            out uint lpNumberOfBytesTransferred,    // 転送されたバイト数
            bool bWait                              // 待機オプション
        );

        [DllImport("kernel32", SetLastError = true)]
        public extern static unsafe bool SetupComm
        (
            IntPtr hFile,       // 通信デバイスのハンドル
            uint dwInQueue,     // 入力バッファのサイズ
            uint dwOutQueue     // 出力バッファのサイズ
        );

        [DllImport("kernel32", SetLastError = true)]
        public extern static unsafe bool PurgeComm
        (
            IntPtr hFile,   // 通信資源のハンドル
            uint dwFlags    // 実行する操作
        );

        [DllImport("kernel32", SetLastError = true)]
        public extern static unsafe bool GetCommState
        (
            IntPtr hFile,       // 通信デバイスのハンドル
            out DCB lpDCB       // DCB（ デバイス制御ブロック）構造体へのポインタ
        );

        [DllImport("kernel32", SetLastError = true)]
        public extern static unsafe bool SetCommState
        (
            IntPtr hFile,       // 通信デバイスのハンドル
            ref DCB lpDCB       // DCB（ デバイス制御ブロック）構造体へのポインタ
        );

        [DllImport("kernel32", SetLastError = true)]
        public extern static unsafe bool GetCommMask
        (
            IntPtr hFile,       // 通信デバイスのハンドル
            out uint lpEvtMask  // イベントマスクを受け取る変数へのポインタ
        );

        [DllImport("kernel32", SetLastError = true)]
        public extern static unsafe bool SetCommMask
        (
            IntPtr hFile,       // 通信デバイスのハンドル
            uint dwEvtMask      // 監視するイベントを示すマスク
        );

        [DllImport("kernel32.dll")]
        public extern static bool GetCommProperties
        (
            IntPtr hFile,               // 通信デバイスのハンドル  
            ref COMMPROP lpCommProp     // COMMPROP（ 通信プロパティ）構造体へのポインタ
        );

        [DllImport("kernel32", SetLastError = true)]
        public extern static unsafe bool WaitCommEvent
        (
            IntPtr hFile,                   // 通信デバイスのハンドル
            out uint lpEvtMask,             // イベントを受け取る変数へのポインタ
            IntPtr lpOverlapped             // OVERLAPPED 構造体へのポインタ
        );

        [DllImport("kernel32", SetLastError = true)]
        public extern static unsafe bool WaitCommEvent
        (
            IntPtr hFile,                   // 通信デバイスのハンドル
            out uint lpEvtMask,             // イベントを受け取る変数へのポインタ
            NativeOverlapped lpOverlapped   // OVERLAPPED 構造体へのポインタ
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public extern static bool SetCommTimeouts
        (
            IntPtr hFile,
            [In] ref COMMTIMEOUTS lpCommTimeouts
        );

        [DllImport("kernel32", SetLastError = true)]
        public extern static unsafe bool ClearCommError
        (
            IntPtr hFile,       // 通信デバイスのハンドル
            out uint lpErrors,  // エラーコードを受け取る変数へのポインタ
            out COMSTAT lpStat  // 通信状態バッファへのポインタ
        );

        [DllImport("kernel32", SetLastError = true)]
        public extern static unsafe bool EscapeCommFunction
        (
            IntPtr hFile,   // 通信デバイスのハンドル
            uint dwFunc     // 実行する拡張機能
        );

        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool DeviceIoControl(IntPtr hDevice, uint dwIoControlCode,
            IntPtr lpInBuffer,
            uint nInBufferSize,
            IntPtr lpOutBuffer,
            uint nOutBufferSize,
            out uint lpBytesReturned,
            IntPtr lpOverlapped
        );

        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool DeviceIoControl(IntPtr hDevice, uint dwIoControlCode,
            IntPtr lpInBuffer,
            uint nInBufferSize,
            IntPtr lpOutBuffer,
            uint nOutBufferSize,
            out uint lpBytesReturned,
            ref NativeOverlapped lpOverlapped
        );

        public static bool DeviceIoControl(IntPtr handle, uint ctrl_code, object obj_write, object obj_read, bool async)
        {
            var ctrl_ok = false;

            if (handle != NativeMethods.INVALID_HANDLE_VALUE) {
                var write_buff = IntPtr.Zero;
                var write_size = (uint)0;
                var read_buff = IntPtr.Zero;
                var read_size = (uint)0;

                if (obj_write != null) {
                    write_size = (uint)Marshal.SizeOf(obj_write);
                    write_buff = Marshal.AllocHGlobal(Marshal.SizeOf(obj_write));
                    Marshal.StructureToPtr(obj_write, write_buff, false);
                }

                if (obj_read != null) {
                    read_size = (uint)Marshal.SizeOf(obj_read);
                    read_buff = Marshal.AllocHGlobal(Marshal.SizeOf(obj_read));
                }

                if (!async) {
                    /* --- 同期実行 --- */
                    ctrl_ok = NativeMethods.DeviceIoControl(
                                handle,
                                ctrl_code,
                                write_buff,
                                write_size,
                                read_buff,
                                read_size,
                                out read_size,
                                IntPtr.Zero);
                } else {
                    /* --- 非同期実行 --- */
                    var evh = NativeMethods.CreateEvent(IntPtr.Zero, true, false, null);
                    var ovl = new NativeOverlapped();

                    ovl.EventHandle = evh;

                    ctrl_ok = NativeMethods.DeviceIoControl(
                                handle,
                                ctrl_code,
                                write_buff,
                                write_size,
                                read_buff,
                                read_size,
                                out read_size,
                                ref ovl);
                   
                    if (ctrl_ok) {
                        WaitForSingleObject(evh, INFINITE);
                    }

                    CloseHandle(evh);
                }

                if (ctrl_ok) {
                    if ((read_buff != IntPtr.Zero) && (read_size > 0)) {
                        Marshal.PtrToStructure(read_buff, obj_read);
                    }
                }

                if (write_buff != IntPtr.Zero) {
                    Marshal.FreeHGlobal(write_buff);
                }
                if (read_buff != IntPtr.Zero) {
                    Marshal.FreeHGlobal(read_buff);
                }
            }

            return (ctrl_ok);
        }

        public static bool DeviceIoControl(string devname, uint ctrl_code, object obj_write, object obj_read, bool async)
        {
            var ctrl_ok = false;
            var handle = NativeMethods.INVALID_HANDLE_VALUE;

            if (!async) {
                /* 同期実行 */
                handle = NativeMethods.CreateFile(
                                    devname,
                                    0,
                                    0,
                                    IntPtr.Zero,
                                    NativeMethods.OPEN_EXISTING,
                                    0,
                                    IntPtr.Zero);

            } else {
                /* 非同期実行 */
                handle = NativeMethods.CreateFile(
                                    devname,
                                    0,
                                    0,
                                    IntPtr.Zero,
                                    NativeMethods.OPEN_EXISTING,
                                    FILE_FLAG_OVERLAPPED,
                                    IntPtr.Zero);

            }

            if (handle != NativeMethods.INVALID_HANDLE_VALUE) {
                ctrl_ok = DeviceIoControl(handle, ctrl_code, obj_write, obj_read, async);

                NativeMethods.CloseHandle(handle);
            }

            return (ctrl_ok);
        }

        [DllImport("kernel32", SetLastError = true)]
        public extern static unsafe UInt32 QueryDosDevice
        (
            string lpDeviceName,        // MS-DOS デバイス名文字列へのポインタ
            byte[] lpTargetPath,        // 照会結果を格納するバッファへのポインタ
            UInt32 ucchMax              // バッファの最大記憶容量
        );

        [DllImport("setupapi.dll", SetLastError = true)]
        public extern static unsafe IntPtr SetupDiGetClassDevs
        (
            ref Guid ClassGuid,
            string Enumerator,
            IntPtr hwndParent,
            UInt32 Flags
        );

        [DllImport("setupapi.dll", SetLastError = true)]
        public extern static unsafe bool SetupDiClassGuidsFromName
        (
            string ClassName,
            ref Guid ClassGuidList,
            UInt32 ClassGuidListSize,
            out UInt32 RequiredSize
        );

        [DllImport("setupapi.dll", SetLastError = true)]
        public extern static unsafe bool SetupDiDestroyDeviceInfoList
        (
            IntPtr DeviceInfoSet
        );

        [DllImport("setupapi.dll", SetLastError = true)]
        public extern static unsafe bool SetupDiEnumDeviceInfo
        (
            IntPtr DeviceInfoSet,
            UInt32 MemberIndex,
            out SP_DEVINFO_DATA DeviceInfoData
        );

        [DllImport("setupapi.dll", SetLastError = true)]
        public extern static unsafe bool SetupDiGetDeviceRegistryProperty
        (
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            UInt32 Property,
            out UInt32 PropertyRegDataType,
            byte[] PropertyBuffer,
            UInt32 PropertyBufferSize,
            out UInt32 RequiredSize
        );

        [DllImport("setupapi.dll", SetLastError = true)]
        public extern static unsafe IntPtr SetupDiOpenDevRegKey
        (
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            UInt32 Scope,
            UInt32 HwProfile,
            UInt32 KeyType,
            UInt32 samDesired
        );

        [DllImport("setupapi.dll", SetLastError=true)]
        public static extern bool SetupDiCallClassInstaller
        (
             UInt32 InstallFunction,
             IntPtr DeviceInfoSet,
             ref SP_DEVINFO_DATA DeviceInfoData
        );

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueExW", SetLastError = true)]
        public extern static unsafe Int32 RegQueryValueEx
        (
            IntPtr hKey,            // キーのハンドル
            string lpValueName,     // レジストリエントリ名
            IntPtr lpReserved,      // 予約済み
            out UInt32 lpType,      // データ型が格納されるバッファ
            byte[] lpData,          // データが格納されるバッファ
            ref UInt32 lpcbData     // データバッファのサイズ
        );

        [DllImport("advapi32.dll", SetLastError = true)]
        public extern static unsafe Int32 RegCloseKey
        (
            IntPtr hKey   // 閉じるべきキーのハンドル
        );

        [DllImport("ntdll.dll", ExactSpelling=true, SetLastError=false)]
        public extern static int NtClose(IntPtr hObject);

        [DllImport("ntdll.dll")]
        public extern static int NtOpenDirectoryObject(
            out SafeFileHandle DirectoryHandle,
            UInt32 DesiredAccess,
            ref OBJECT_ATTRIBUTES ObjectAttributes);

        [DllImport("ntdll.dll")]
        public extern static int NtQueryDirectoryObject(
            SafeFileHandle DirectoryHandle,
            IntPtr Buffer,
            int Length,
            bool ReturnSingleEntry,
            bool RestartScan,
            ref uint Context,
            out uint ReturnLength);

        [DllImport("winmm.dll")]
        public static extern Int32 timeGetSystemTime
        (
            out MMTIME pmmt,
            UInt32 cbmmt
        );

        [DllImport("winmm.dll")]
        public static extern UInt32 timeGetTime();

        [DllImport("winmm.dll")]
        public static extern Int32 timeSetEvent
        (
            Int32        uDelay,
            Int32        uResolution,
            TIMECALLBACK lpTimeProc,
            IntPtr       dwUser,
            UInt32       fuEvent
        );

        [DllImport("winmm.dll")]
        public static extern Int32 timeKillEvent
        (
            Int32 uTimerID
        );

        [DllImport("winmm.dll")]
        public static extern Int32 timeGetDevCaps
        (
            out TIMECAPS ptc, Int32 cbtc
        );

        [DllImport("winmm.dll")]
        public static extern Int32 timeBeginPeriod
        (
            Int32 uPeriod
        );

        [DllImport("winmm.dll")]
        public static extern Int32 timeEndPeriod
        (
            Int32 uPeriod
        );
    }
}
