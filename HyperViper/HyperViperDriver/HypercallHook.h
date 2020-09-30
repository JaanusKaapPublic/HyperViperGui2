#pragma once
#include<ntddk.h>
#include"HypercallCore.h"

extern "C" void inline hypercallHook(void);
extern "C" void* hypercallOriginalLocation;
extern "C" void* hypercallPreCallHandler;

class HypercallHook : public HypercallCore
{
private:
	static HypercallHook* currentHookHandler;
	static void preHypercallStatic(HV_X64_HYPERCALL_INPUT InputValue, ULONGLONG InputPa, ULONGLONG OutputPa);

protected:
	virtual void preHypercall(HV_X64_HYPERCALL_INPUT InputValue, ULONGLONG InputPa, ULONGLONG OutputPa);

public:
	NTSTATUS hook();
	NTSTATUS unhook();
	bool isHooked();
};