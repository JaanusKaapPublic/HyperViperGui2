#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#include "HVstructures.h"
#include "HVdriverIO.h"

#ifdef HYPERVIPERDLL_EXPORTS
#define HYPERVIPERDLL_API extern "C" __declspec(dllexport)
#else
#define HYPERVIPERDLL_API extern "C" __declspec(dllimport)
#endif

HVdriverIO driver;

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

HYPERVIPERDLL_API bool init()
{
	return driver.init();
}

//Hypercalls
HYPERVIPERDLL_API bool hypercallsCall(PHV_X64_HYPERCALL_INPUT callInfo, PVOID inBuffer, DWORD inBufferSize, PHV_X64_HYPERCALL_OUTPUT callResult, PVOID outBuffer, DWORD outBufferSize)
{
	return driver.hypercallsCall(callInfo, inBuffer, inBufferSize, callResult, outBuffer, outBufferSize);
}

HYPERVIPERDLL_API bool hypercallsHook(void)
{
	return driver.hypercallsHook();
}

HYPERVIPERDLL_API bool hypercallsUnhook(void)
{
	return driver.hypercallsUnhook();
}

HYPERVIPERDLL_API bool hypercallsStartLogging(char* fname)
{
	return driver.hypercallsStartLogging(fname);
}

HYPERVIPERDLL_API bool hypercallsStopLogging(void)
{
	return driver.hypercallsStopLogging();
}

HYPERVIPERDLL_API bool hypercallsGetStats(PHV_HOOKING_HCALL_STATS output)
{
	return driver.hypercallsGetStats(output);
}

HYPERVIPERDLL_API bool hypercallsSetConf(PHV_HOOKING_HCALL_CONF_SET input)
{
	return driver.hypercallsSetConf(input);
}

HYPERVIPERDLL_API bool hypercallsClearStats(void)
{
	return driver.hypercallsClearStats();
}

HYPERVIPERDLL_API bool  hypercallsFuzz(PHV_X64_HYPERCALL_INPUT callInfo, PVOID buffer, DWORD len, PHV_MUTATION_CONF conf)
{
	return driver.hypercallsFuzz(callInfo, buffer, len, conf);
}