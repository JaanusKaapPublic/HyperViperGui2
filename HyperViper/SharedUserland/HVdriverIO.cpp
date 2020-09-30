#include "HVdriverIO.h"
#include <stdio.h>

bool HVdriverIO::init()
{
	handle = CreateFile(L"\\\\.\\HyperViper", GENERIC_READ | GENERIC_WRITE, 0, NULL, OPEN_EXISTING, 0, NULL);
	return (handle != INVALID_HANDLE_VALUE);
}

bool HVdriverIO::devIOctrl(DWORD code, PVOID inBuffer, DWORD inBufferSize, PVOID outBuffer, DWORD* outBufferSize)
{
	bool result = false;
	DWORD recv, outBufferSizeVal = 0;
	if (outBufferSize)
		outBufferSizeVal = *outBufferSize;

	if (DeviceIoControl(handle, code, inBuffer, inBufferSize, outBuffer, outBufferSizeVal, &recv, NULL))
	{
		result = true;
		if (outBufferSize)
			*outBufferSize = recv;
	}
	return result;
}

bool HVdriverIO::hypercallsCall(PHV_X64_HYPERCALL_INPUT callInfo, PVOID inBuffer, DWORD inBufferSize, PHV_X64_HYPERCALL_OUTPUT callResult, PVOID outBuffer, DWORD outBufferSize)
{
	bool result = false;
	outBufferSize += sizeof(HV_X64_HYPERCALL_OUTPUT);
	BYTE* bufIn = new BYTE[inBufferSize + sizeof(HV_X64_HYPERCALL_INPUT)];
	BYTE* bufOut = new BYTE[outBufferSize];

	memcpy(bufIn, callInfo, sizeof(HV_X64_HYPERCALL_INPUT));
	memcpy(bufIn + sizeof(HV_X64_HYPERCALL_INPUT), inBuffer, inBufferSize);

	if (devIOctrl(IOCTL_HYPERCALLS_CALL, bufIn, inBufferSize + sizeof(HV_X64_HYPERCALL_INPUT), bufOut, &outBufferSize))
	{
		result = true;
		memcpy(callResult, bufOut, sizeof(HV_X64_HYPERCALL_OUTPUT));
		memcpy(outBuffer, bufOut + sizeof(HV_X64_HYPERCALL_OUTPUT), outBufferSize - sizeof(HV_X64_HYPERCALL_OUTPUT));
	}
	delete bufIn;
	delete bufOut;
	return result;
}

bool HVdriverIO::hypercallsHook(void)
{
	return devIOctrl(IOCTL_HYPERCALLS_HOOK, NULL, 0, NULL, NULL);
}

bool HVdriverIO::hypercallsUnhook(void)
{
	return devIOctrl(IOCTL_HYPERCALLS_UNHOOK, NULL, 0, NULL, NULL);
}

bool HVdriverIO::hypercallsStartLogging(char* filename)
{
	char* buffer = new char[strlen(filename) + 13];
	memcpy(buffer, "\\DosDevices\\", 12);
	memcpy(buffer + 12, filename, strlen(filename) + 1);
	bool result = devIOctrl(IOCTL_HYPERCALLS_START_LOG, buffer, (DWORD)strlen(buffer), NULL, NULL);
	delete buffer;
	return result;
}

bool HVdriverIO::hypercallsStopLogging()
{
	return devIOctrl(IOCTL_HYPERCALLS_STOP_LOG, NULL, 0, NULL, NULL);
}

bool HVdriverIO::hypercallsGetStats(PHV_HOOKING_HCALL_STATS output)
{
	DWORD len = sizeof(HV_HOOKING_HCALL_STATS) * (HYPERCALL_LAST_NR+1);
	return devIOctrl(IOCTL_HYPERCALLS_GET_STATS, NULL, 0, output, &len);
}

bool HVdriverIO::hypercallsSetConf(PHV_HOOKING_HCALL_CONF_SET input)
{
	return devIOctrl(IOCTL_HYPERCALLS_SET_MONITOR_CONF, input, sizeof(HV_HOOKING_HCALL_CONF_SET), NULL, NULL);
}

bool HVdriverIO::hypercallsClearStats(void)
{
	return devIOctrl(IOCTL_HYPERCALLS_CLEAR_STATS, NULL, 0, NULL, NULL);
}

bool HVdriverIO::hypercallsFuzz(PHV_X64_HYPERCALL_INPUT callInfo, PVOID buffer, DWORD len, PHV_MUTATION_CONF confIn)
{
	char* data = new char[sizeof(HV_FUZZING_HCALL) + len];
	PHV_FUZZING_HCALL conf = (PHV_FUZZING_HCALL)data;
	memcpy(&(conf->code), callInfo, sizeof(HV_X64_HYPERCALL_INPUT));
	memcpy(&(conf->conf), confIn, sizeof(HV_MUTATION_CONF));
	memcpy(data + sizeof(HV_FUZZING_HCALL), buffer, len);
	return devIOctrl(IOCTL_HYPERCALLS_FUZZ, data, len + sizeof(HV_FUZZING_HCALL), NULL, NULL);
}
