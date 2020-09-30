#pragma once
#include<Windows.h>
#include"HVstructures.h"
#include"HVdef.h"
#include"HVenums.h"

class HVdriverIO
{
private:
	HANDLE handle;

public:
	bool init();
	bool devIOctrl(DWORD code, PVOID inBuffer, DWORD inBufferSize, PVOID outBuffer, DWORD* outBufferSize);

	//Hypercalls
	bool hypercallsCall(PHV_X64_HYPERCALL_INPUT callInfo, PVOID inBuffer, DWORD inBufferSize, PHV_X64_HYPERCALL_OUTPUT callResult, PVOID outBuffer, DWORD outBufferSize);
	bool hypercallsHook(void);
	bool hypercallsUnhook(void);
	bool hypercallsStartLogging(char* fname);
	bool hypercallsStopLogging();
	bool hypercallsGetStats(PHV_HOOKING_HCALL_STATS output);
	bool hypercallsSetConf(PHV_HOOKING_HCALL_CONF_SET output);
	bool hypercallsClearStats(void);
	bool hypercallsFuzz(PHV_X64_HYPERCALL_INPUT callInfo, PVOID buffer, DWORD len, PHV_MUTATION_CONF conf);
};