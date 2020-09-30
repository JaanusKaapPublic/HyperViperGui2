#include "HypercallHook.h"
#include "OsPointers.h"

HypercallHook* HypercallHook::currentHookHandler;
void HypercallHook::preHypercallStatic(HV_X64_HYPERCALL_INPUT InputValue, ULONGLONG InputPa, ULONGLONG OutputPa)
{
	if (currentHookHandler)
		currentHookHandler->preHypercall(InputValue, InputPa, OutputPa);
}

void HypercallHook::preHypercall(HV_X64_HYPERCALL_INPUT InputValue, ULONGLONG InputPa, ULONGLONG OutputPa)
{
	UNREFERENCED_PARAMETER(InputValue);
	UNREFERENCED_PARAMETER(InputPa);
	UNREFERENCED_PARAMETER(OutputPa);
}

NTSTATUS HypercallHook::hook()
{
#ifdef _DEBUG
	DbgPrint("[DEBUG][HypercallHook::hook] starting hook\n", OsPointers::getPtr_HvcallCodeVa());
#endif
	if (hypercallOriginalLocation)
		return STATUS_INVALID_DEVICE_REQUEST;

	void* hvCallCodeVa = OsPointers::getPtr_HvcallCodeVa();
#ifdef _DEBUG
	DbgPrint("[DEBUG][HypercallHook::hook] HvcallCodeVa = 0x%llx\n", hvCallCodeVa);
#endif
	currentHookHandler = this;
	hypercallOriginalLocation = *(void**)hvCallCodeVa;
	hypercallPreCallHandler = (void*)(&preHypercallStatic);
	*(void**)hvCallCodeVa = (void*)&hypercallHook;

#ifdef _DEBUG
	DbgPrint("[DEBUG][HypercallHook::hook] hooking over\n", OsPointers::getPtr_HvcallCodeVa());
#endif
	return STATUS_SUCCESS;
}

NTSTATUS HypercallHook::unhook()
{
	if (!hypercallOriginalLocation)
		return STATUS_INVALID_DEVICE_REQUEST;

	*(void**)OsPointers::getPtr_HvcallCodeVa() = hypercallOriginalLocation;
	currentHookHandler = NULL;
	hypercallPreCallHandler = NULL;
	hypercallOriginalLocation = NULL;
	return STATUS_SUCCESS;
}

bool HypercallHook::isHooked()
{
	return (currentHookHandler != NULL);
}