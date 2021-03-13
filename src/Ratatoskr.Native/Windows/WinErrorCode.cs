using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Native.Windows
{
    public enum WinErrorCode : UInt32
    {
        NO_ERROR					= 0,
        ERROR_INVALID_FUNCTION		= 1,
        ERROR_FILE_NOT_FOUND		= 2,
        ERROR_PATH_NOT_FOUND		= 3,
        ERROR_TOO_MANY_OPEN_FILES	= 4,
        ERROR_ACCESS_DENIED			= 5,
        ERROR_INVALID_HANDLE		= 6,
        ERROR_ARENA_TRASHED			= 7,
        ERROR_NOT_ENOUGH_MEMORY		= 8,
        ERROR_INVALID_BLOCK			= 9,
        ERROR_BAD_ENVIRONMENT		= 10,
        ERROR_BAD_FORMAT			= 11,
        ERROR_INVALID_ACCESS		= 12,
        ERROR_INVALID_DATA			= 13,
        /* **** reserved			= 14,	, ***** */
        ERROR_INVALID_DRIVE			= 15,
        ERROR_CURRENT_DIRECTORY		= 16,
        ERROR_NOT_SAME_DEVICE		= 17,
        ERROR_NO_MORE_FILES			= 18,
        /* */
        /* These are the universal int 24 mappings for the old INT 24 set of errors */
        /* */
        ERROR_WRITE_PROTECT			= 19,
        ERROR_BAD_UNIT				= 20,
        ERROR_NOT_READY				= 21,
        ERROR_BAD_COMMAND			= 22,
        ERROR_CRC					= 23,
        ERROR_BAD_LENGTH			= 24,
        ERROR_SEEK					= 25,
        ERROR_NOT_DOS_DISK			= 26,
        ERROR_SECTOR_NOT_FOUND		= 27,
        ERROR_OUT_OF_PAPER			= 28,
        ERROR_WRITE_FAULT			= 29,
        ERROR_READ_FAULT			= 30,
        ERROR_GEN_FAILURE			= 31,
        /* */
        /* These are the new 3.0 error codes reported through INT 24 */
        /* */
        ERROR_SHARING_VIOLATION			= 32,
        ERROR_LOCK_VIOLATION			= 33,
        ERROR_WRONG_DISK				= 34,
        ERROR_FCB_UNAVAILABLE			= 35,
        ERROR_SHARING_BUFFER_EXCEEDED	= 36,
        /* */
        /* New OEM network-related errors are 50-79 */
        /* */
        ERROR_NOT_SUPPORTED		= 50,
        /* */
        /* End of INT 24 reportable errors */
        /* */
        ERROR_FILE_EXISTS		= 80,
        ERROR_DUP_FCB			= 81, /* ***** */
        ERROR_CANNOT_MAKE		= 82,
        ERROR_FAIL_I24			= 83,
        /* */
        /* New 3.0 network related error codes */
        /* */
        ERROR_OUT_OF_STRUCTURES		= 84,
        ERROR_ALREADY_ASSIGNED		= 85,
        ERROR_INVALID_PASSWORD		= 86,
        ERROR_INVALID_PARAMETER		= 87,
        ERROR_NET_WRITE_FAULT		= 88,
        /* */
        /* New error codes for 4.0 */
        /* */
        ERROR_NO_PROC_SLOTS		= 89,	/* no process slots available */
        ERROR_NOT_FROZEN		= 90,
        ERR_TSTOVFL				= 91,	/* timer service table overflow */
        ERR_TSTDUP				= 92,	/* timer service table duplicate */
        ERROR_NO_ITEMS			= 93,	/* There were no items to operate upon */
        ERROR_INTERRUPT			= 95,	/* interrupted system call */

        ERROR_TOO_MANY_SEMAPHORES			= 100,
        ERROR_EXCL_SEM_ALREADY_OWNED		= 101,
        ERROR_SEM_IS_SET					= 102,
        ERROR_TOO_MANY_SEM_REQUESTS			= 103,
        ERROR_INVALID_AT_INTERRUPT_TIME		= 104,

        ERROR_SEM_OWNER_DIED	= 105, /* waitsem found owner died */
        ERROR_SEM_USER_LIMIT	= 106, /* too many procs have this sem */
        ERROR_DISK_CHANGE		= 107, /* insert disk b into drive a */
        ERROR_DRIVE_LOCKED		= 108, /* drive locked by another process */
        ERROR_BROKEN_PIPE		= 109, /* write on pipe with no reader */
        /* */
        /* New error codes for OS/2 */
        /* */
        ERROR_OPEN_FAILED				= 110, /* open/created failed due to */
						        /* explicit fail command */
        ERROR_BUFFER_OVERFLOW			= 111, /* buffer passed to system call */
						        /* is too small to hold return */
						        /* data. */
        ERROR_DISK_FULL					= 112, /* not enough space on the disk */
						        /* (DOSNEWSIZE/w_NewSize) */
        ERROR_NO_MORE_SEARCH_HANDLES	= 113, /* can't allocate another search */
						        /* structure and handle. */
						        /* (DOSFINDFIRST/w_FindFirst) */
        ERROR_INVALID_TARGET_HANDLE		= 114, /* Target handle in DOSDUPHANDLE */
						        /* is invalid */
        ERROR_PROTECTION_VIOLATION		= 115, /* Bad user virtual address */
        ERROR_VIOKBD_REQUEST			= 116,
        ERROR_INVALID_CATEGORY			= 117, /* Category for DEVIOCTL in not */
						        /* defined */
        ERROR_INVALID_VERIFY_SWITCH		= 118, /* invalid value passed for */
						        /* verify flag */
        ERROR_BAD_DRIVER_LEVEL			= 119, /* DosDevIOCTL looks for a level */
						        /* four driver.	  If the driver */
						        /* is not level four we return */
						        /* this code */
        ERROR_CALL_NOT_IMPLEMENTED		= 120, /* returned from stub api calls. */
						        /* This call will disappear when */
						        /* all the api's are implemented. */
        ERROR_SEM_TIMEOUT				= 121, /* Time out happened from the */
						        /* semaphore api functions. */
        ERROR_INSUFFICIENT_BUFFER		= 122, /* Some call require the  */
						        /* application to pass in a buffer */
						        /* filled with data.  This error is */
						        /* returned if the data buffer is too */
						        /* small.  For example: DosSetFileInfo */
						        /* requires 4 bytes of data.  If a */
						        /* two byte buffer is passed in then */
						        /* this error is returned.	 */
						        /* error_buffer_overflow is used when */
						        /* the output buffer in not big enough. */
        ERROR_INVALID_NAME				= 123, /* illegal character or malformed */
						        /* file system name */
        ERROR_INVALID_LEVEL				= 124, /* unimplemented level for info */
						        /* retrieval or setting */
        ERROR_NO_VOLUME_LABEL			= 125, /* no volume label found with */
						        /* DosQFSInfo command */
        ERROR_MOD_NOT_FOUND				= 126, /* w_getprocaddr,w_getmodhandle */
        ERROR_PROC_NOT_FOUND			= 127, /* w_getprocaddr */

        ERROR_WAIT_NO_CHILDREN			= 128, /* CWait finds to children */

        ERROR_CHILD_NOT_COMPLETE		= 129, /* CWait children not dead yet */

        /*This is a temporary fix for the 4-19-86 build this should be changed when */
        /* we get the file from MS */
        ERROR_DIRECT_ACCESS_HANDLE		= 130, /* handle operation is invalid */
						        /* for direct disk access */
						        /* handles */
        ERROR_NEGATIVE_SEEK				= 131, /* application tried to seek  */
						        /* with negative offset */
        ERROR_SEEK_ON_DEVICE			= 132, /* application tried to seek */
						        /* on device or pipe */
        /* */
        /* The following are errors generated by the join and subst workers */
        /* */
        ERROR_IS_JOIN_TARGET			= 133,
        ERROR_IS_JOINED					= 134,
        ERROR_IS_SUBSTED				= 135,
        ERROR_NOT_JOINED				= 136,
        ERROR_NOT_SUBSTED				= 137,
        ERROR_JOIN_TO_JOIN				= 138,
        ERROR_SUBST_TO_SUBST			= 139,
        ERROR_JOIN_TO_SUBST				= 140,
        ERROR_SUBST_TO_JOIN				= 141,
        ERROR_BUSY_DRIVE				= 142,
        ERROR_SAME_DRIVE				= 143,
        ERROR_DIR_NOT_ROOT				= 144,
        ERROR_DIR_NOT_EMPTY				= 145,
        ERROR_IS_SUBST_PATH				= 146,
        ERROR_IS_JOIN_PATH				= 147,
        ERROR_PATH_BUSY					= 148,
        ERROR_IS_SUBST_TARGET			= 149,
        ERROR_SYSTEM_TRACE				= 150, /* system trace error */
        ERROR_INVALID_EVENT_COUNT		= 151, /* DosMuxSemWait errors */
        ERROR_TOO_MANY_MUXWAITERS		= 152,
        ERROR_INVALID_LIST_FORMAT		= 153,
        ERROR_LABEL_TOO_LONG			= 154,
        ERROR_TOO_MANY_TCBS				= 155,
        ERROR_SIGNAL_REFUSED			= 156,
        ERROR_DISCARDED					= 157,
        ERROR_NOT_LOCKED				= 158,
        ERROR_BAD_THREADID_ADDR			= 159,
        ERROR_BAD_ARGUMENTS				= 160,
        ERROR_BAD_PATHNAME				= 161,
        ERROR_SIGNAL_PENDING			= 162,
        ERROR_UNCERTAIN_MEDIA			= 163,
        ERROR_MAX_THRDS_REACHED			= 164,
        ERROR_MONITORS_NOT_SUPPORTED	= 165,

        ERROR_INVALID_SEGMENT_NUMBER	= 180,
        ERROR_INVALID_CALLGATE			= 181,
        ERROR_INVALID_ORDINAL			= 182,
        ERROR_ALREADY_EXISTS			= 183,
        ERROR_NO_CHILD_PROCESS			= 184,
        ERROR_CHILD_ALIVE_NOWAIT		= 185,
        ERROR_INVALID_FLAG_NUMBER		= 186,
        ERROR_SEM_NOT_FOUND				= 187,

        /*	following error codes have added to make the loader error
	        messages distinct
        */

        ERROR_INVALID_STARTING_CODESEG		= 188,
        ERROR_INVALID_STACKSEG				= 189,
        ERROR_INVALID_MODULETYPE			= 190,
        ERROR_INVALID_EXE_SIGNATURE			= 191,
        ERROR_EXE_MARKED_INVALID			= 192,
        ERROR_BAD_EXE_FORMAT				= 193,
        ERROR_ITERATED_DATA_EXCEEDS_64k		= 194,
        ERROR_INVALID_MINALLOCSIZE			= 195,
        ERROR_DYNLINK_FROM_INVALID_RING		= 196,
        ERROR_IOPL_NOT_ENABLED				= 197,
        ERROR_INVALID_SEGDPL				= 198,
        ERROR_AUTODATASEG_EXCEEDS_64k		= 199,
        ERROR_RING2SEG_MUST_BE_MOVABLE		= 200,
        ERROR_RELOC_CHAIN_XEEDS_SEGLIM		= 201,
        ERROR_INFLOOP_IN_RELOC_CHAIN		= 202,

        ERROR_ENVVAR_NOT_FOUND			= 203,
        ERROR_NOT_CURRENT_CTRY			= 204,
        ERROR_NO_SIGNAL_SENT			= 205,
        ERROR_FILENAME_EXCED_RANGE		= 206, /* if filename > 8.3 */
        ERROR_RING2_STACK_IN_USE		= 207, /* for FAPI */
        ERROR_META_EXPANSION_TOO_LONG	= 208, /* if "*a" > 8.3 */

        ERROR_INVALID_SIGNAL_NUMBER		= 209,
        ERROR_THREAD_1_INACTIVE			= 210,
        ERROR_INFO_NOT_AVAIL			= 211, /*@@ PTM 5550 */
        ERROR_LOCKED					= 212,
        ERROR_BAD_DYNALINK				= 213, /*@@ PTM 5760 */
        ERROR_TOO_MANY_MODULES			= 214,
        ERROR_NESTING_NOT_ALLOWED		= 215,
    }
}
