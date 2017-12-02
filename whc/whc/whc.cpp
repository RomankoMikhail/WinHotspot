// whc.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"

#define EXIT_OK				0
#define EXIT_NOTHING		-1
#define EXIT_FAIL_WLAN		-2
#define EXIT_FAIL_PASSWORD	-3
#define EXIT_FAIL_SSID		-4
#define EXIT_FAIL_START		-5
#define EXIT_FAIL_STOP		-6
#define EXIT_FAIL_LIST		-7
#define EXIT_FAIL_INFO		-8

#define WINDOWS_VISTA_CLIENT 2


BOOL setSSID(PDOT11_SSID ssid, UCHAR * ssid_new)
{
	if (strlen((char*)ssid_new) > DOT11_SSID_MAX_LENGTH)
		return FALSE;

	strcpy((char*)ssid->ucSSID, (char*)ssid_new);
	ssid->uSSIDLength = strlen((char*)ssid_new);
	return TRUE;
}


BOOL setWLANHostedNetworkConnectionSettings(PWLAN_HOSTED_NETWORK_CONNECTION_SETTINGS settings, UCHAR * ssid, DWORD maxClients)
{
	if (setSSID(&settings->hostedNetworkSSID, ssid) == FALSE)
		return FALSE;

	settings->dwMaxNumberOfPeers = maxClients;
	return TRUE;
}


BOOL setWLANPassword(HANDLE hClientHandle, PUCHAR pass)
{
	if (WlanHostedNetworkSetSecondaryKey(hClientHandle, strlen((char*)pass) + 1, pass, TRUE, TRUE, NULL, NULL) == ERROR_SUCCESS)
		return true;
	return false;
}


typedef enum _actions {
	ACTION_NOTHING,
	ACTION_START,
	ACTION_STOP,
	ACTION_LIST,
	ACTION_INFO
} actions;

typedef struct _applicationSettings{
	char *ssid, *password;
	int verbose, maxClients, running;
	actions action;
} applicationSettings;


applicationSettings settings;


enum printType {
	PRINT_ALL,
	PRINT_VERBOSE,
	PRINT_MACHINE
};


// Print output with format according to flags and verbose option
void printOut(enum printType type, char * format, ...)
{
	if (type == PRINT_ALL || (type == PRINT_VERBOSE) && (settings.verbose == 1) || (type == PRINT_MACHINE) && (settings.verbose == 0))
	{
		va_list args;
		va_start(args, format);
		vprintf(format, args);
		va_end(args);
	}
}


// Print output with format according to flags and verbose option
void wprintOut(enum printType type, wchar_t * format, ...)
{
	if (type == PRINT_ALL || (type == PRINT_VERBOSE) && (settings.verbose == 1) || (type == PRINT_MACHINE) && (settings.verbose == 0))
	{
		va_list args;
		va_start(args, format);
		vwprintf(format, args);
		va_end(args);
	}
}


// Print output in MAC format according to flags and verbose option
void printHex(enum printType type, DOT11_MAC_ADDRESS mac)
{
	if (type == PRINT_ALL || (type == PRINT_VERBOSE) && (settings.verbose == 1) || (type == PRINT_MACHINE) && (settings.verbose == 0))
	{
		int i;
		for (i = 0; i < 5; i++)
		{
			printf("%x:", mac[i]);
		}
		printf("%x", mac[i]);
	}
}


void printLocalError(enum printType type, DWORD code)
{
	LPVOID lpMsgBuf;

	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS, NULL,
		code, MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), (LPTSTR)&lpMsgBuf, 0, NULL);
	
	wprintOut(type, L"%ls", lpMsgBuf);

	LocalFree(lpMsgBuf);
}


void printHelp(char * firstArgv)
{
	printf("\n\nConsole utility for controling wifi hotspot\nUsage: %s [OPTIONS]\n\nOptions:\n", firstArgv);
	puts("  /START SSID PASSWORD [CLIENTS = 8] - Start wifi hotspot with settings");
	puts("  /STOP                              - Stop wifi hotspot");
	puts("  /LIST                              - List wifi clients IP and MAC combination");
	puts("  /INFO                              - Information about current status of wifi hotspot");
	puts("  /NOVERBOSE                         - Disable verbose output (usefull for scripts)");
	puts("  /?                                 - Display this help");
}


int strcmp_i(char const *first, char const *second)
{
	for (;; first++, second++) {
		int d = tolower(*first) - tolower(*second);
		if (d != 0 || !*first || !*second)
			return d;
	}
}


int isInteger(char const *str)
{
	while (*str)
	{
		if (isdigit(*str) == 0)
			return 0;
		str++;
	}
	return 1;
}


int main(int argc, char ** argv)
{
	// Initialize default application settings
	settings.action = ACTION_NOTHING;
	settings.maxClients = 8;
	settings.verbose = 1;
	settings.running = 0;
	settings.ssid = NULL;
	settings.password = NULL;


	// Parsing arguments and flags
	if (argc == 1)
	{
		printHelp(argv[0]);
		exit(EXIT_NOTHING);
	}

	int i;
	for (i = 1; i < argc; i++)
	{
		if (!strcmp_i(argv[i], "/?"))
		{
			printHelp(argv[0]);
			exit(EXIT_NOTHING);
		}
		if (!strcmp_i(argv[i], "/NOVERBOSE"))
		{
			settings.verbose = 0;
		}
		if (!strcmp_i(argv[i], "/INFO"))
		{
			settings.action = ACTION_INFO;
		}
		if (!strcmp_i(argv[i], "/LIST"))
		{
			settings.action = ACTION_LIST;
		}
		if (!strcmp_i(argv[i], "/STOP"))
		{
			settings.action = ACTION_STOP;
		}
		if (!strcmp_i(argv[i], "/START"))
		{
			settings.action = ACTION_START;
			if (i + 2 >= argc) {
				printHelp(argv[0]);
				exit(EXIT_NOTHING);
			}
			else 
			{
				settings.ssid = argv[++i];
				settings.password = argv[++i];
				if (i+1 < argc && isInteger(argv[i+1]))
				{
					settings.maxClients = atoi(argv[++i]);
				}
			}
		}
	}


	// Open Wlan handle
	DWORD dwVersion, ret;
	HANDLE hClientHandle;

	if ((ret = WlanOpenHandle(WINDOWS_VISTA_CLIENT, NULL, &dwVersion, &hClientHandle)) != ERROR_SUCCESS)
	{
		wprintOut(PRINT_VERBOSE, L"An error occurred while trying to open the Wlan device: ");
		printLocalError(PRINT_ALL, ret);
		return EXIT_FAIL_WLAN;
	}


	// Get Wlan configuration
	BOOL *tmp = NULL;
	DWORD dwReturnSize;
	WLAN_OPCODE_VALUE_TYPE value_type;


	// Trying to recive configuration from operation system
	if ((ret = WlanHostedNetworkQueryProperty(hClientHandle, wlan_hosted_network_opcode_enable, &dwReturnSize, (PVOID*)&tmp, &value_type, NULL)) == ERROR_BAD_CONFIGURATION)
	{
		wprintOut(PRINT_VERBOSE, L"Wlan hosted network configuration not found. Generating new one...\n");

		// Failed, then try to create new one
		if (WlanHostedNetworkInitSettings(hClientHandle, NULL, NULL) != ERROR_SUCCESS)
		{
			// Failed again
			wprintOut(PRINT_VERBOSE, L"Cannot configure the Wlan device: ");
			printLocalError(PRINT_ALL, ret);
			WlanCloseHandle(hClientHandle, NULL);
			exit(EXIT_FAIL_WLAN);
		}
	}
	else if (ret != ERROR_SUCCESS)
	{
		// Query failed.
		wprintOut(PRINT_VERBOSE, L"Cannot load configuration for Wlan device!: ");
		printLocalError(PRINT_ALL, ret);
		WlanCloseHandle(hClientHandle, NULL);
		exit(EXIT_FAIL_WLAN);
	}


	// Retriving additional information about Wlan device configuration
	PWLAN_HOSTED_NETWORK_STATUS pWlanStatus = NULL;
	if ((ret = WlanHostedNetworkQueryStatus(hClientHandle, &pWlanStatus, NULL)) != ERROR_SUCCESS)
	{
		// Failed while trying to retrive additional information
		wprintOut(PRINT_VERBOSE, L"Cannot load additional information about Wlan device: ");
		printLocalError(PRINT_ALL, ret);
		WlanCloseHandle(hClientHandle, NULL);
		exit(EXIT_FAIL_WLAN);
	}


	// Retriving connection information about Wlan device
	PWLAN_HOSTED_NETWORK_CONNECTION_SETTINGS pWlanSettings = NULL;
	if ((ret = WlanHostedNetworkQueryProperty(hClientHandle, wlan_hosted_network_opcode_connection_settings, &dwReturnSize, (PVOID*)&pWlanSettings, &value_type, NULL)) != ERROR_SUCCESS)
	{
		// Failed while trying to retrive connection information
		wprintOut(PRINT_VERBOSE, L"Cannot load connection information about Wlan device: ");
		printLocalError(PRINT_ALL, ret);
		WlanCloseHandle(hClientHandle, NULL);
		exit(EXIT_FAIL_WLAN);
	}
	
	if (settings.action != ACTION_START)
	{
		settings.ssid = (char*) calloc(pWlanSettings->hostedNetworkSSID.uSSIDLength + 1, 1);
		strcpy(settings.ssid, (char*) pWlanSettings->hostedNetworkSSID.ucSSID);
		settings.maxClients = pWlanSettings->dwMaxNumberOfPeers;
	}

	// Perfoming actions
	switch (settings.action)
	{
	// On info - just printing out everything, that we know about Wlan
	case ACTION_INFO:
		printOut(PRINT_VERBOSE, "SSID:\t\t%s\nActive:\t\t%d\nMax clients:\t%d\nMAC:\t\t", settings.ssid, (pWlanStatus->HostedNetworkState == wlan_hosted_network_active) ? 1 : 0, settings.maxClients);
		printOut(PRINT_MACHINE, "%s;%d;%d;", settings.ssid, (pWlanStatus->HostedNetworkState == wlan_hosted_network_active) ? 1 : 0, settings.maxClients);
		printHex(PRINT_ALL, pWlanStatus->wlanHostedNetworkBSSID);
		printOut(PRINT_ALL, "\n");
		break;

	case ACTION_LIST:
		// TODO: Make listing
		wprintOut(PRINT_VERBOSE, L"\nSorry, List curently is unavailable!\n");
		break;
	
	case ACTION_START:
		// Preparing Wlan SSID and max client number
		if (setWLANHostedNetworkConnectionSettings(pWlanSettings, (UCHAR*)settings.ssid, settings.maxClients) == FALSE)
		{
			wprintOut(PRINT_VERBOSE, L"Cannot set SSID or max client number!\n");
			WlanCloseHandle(hClientHandle, NULL);
			exit(EXIT_FAIL_SSID);
		}

		// Setting up Wlan SSID and max client number
		if ((ret = WlanHostedNetworkSetProperty(hClientHandle, wlan_hosted_network_opcode_connection_settings, (DWORD)sizeof(WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS), pWlanSettings, NULL, NULL)) != ERROR_SUCCESS)
		{
			wprintOut(PRINT_VERBOSE, L"Cannot set SSID or max client number!\n");
			printLocalError(PRINT_ALL, ret);
			WlanCloseHandle(hClientHandle, NULL);
			return EXIT_FAIL_SSID;
		}

		// Setting up password
		if (setWLANPassword(hClientHandle, (UCHAR*)settings.password) == FALSE)
		{
			wprintOut(PRINT_VERBOSE, L"Cannot set password!\n");
			WlanCloseHandle(hClientHandle, NULL);
			return EXIT_FAIL_PASSWORD;
		}

		// Starting up!
		if ((ret = WlanHostedNetworkForceStart(hClientHandle, NULL, NULL)) != ERROR_SUCCESS)
		{
			wprintOut(PRINT_VERBOSE, L"Cannot set SSID or max client number!\n");
			printLocalError(PRINT_ALL, ret);
			WlanCloseHandle(hClientHandle, NULL);
			return EXIT_FAIL_START;
		}
		wprintOut(PRINT_VERBOSE, L"\nHotspot started\n");
		break;

	// On stop - stop hosting wifi with force (very effective, and *almost* only one reliable solution)
	case ACTION_STOP:
		if (WlanHostedNetworkForceStop(hClientHandle, NULL, NULL) != ERROR_SUCCESS)
		{
			// Failed to stop - probably Wlan card is in unclear state and PC must be rebooted
			wprintOut(PRINT_VERBOSE, L"Cannot stop hotspot: ");
			printLocalError(PRINT_ALL, ret);
			WlanCloseHandle(hClientHandle, NULL);
			exit(EXIT_FAIL_STOP);
		}
		wprintOut(PRINT_VERBOSE, L"\nHotspot stoped\n");
		break;

	case ACTION_NOTHING:
	default:
		break;
	}


	// Clearing out resources
	WlanCloseHandle(hClientHandle, NULL);
	WlanFreeMemory(pWlanStatus);
	WlanFreeMemory(pWlanSettings);
	exit(EXIT_OK);
}
