#pragma once
#include "HypercallFuzzing.h"

typedef struct _HV_WORKER_LOG_DATA
{
	PWORK_QUEUE_ITEM WorkItem;
	PVOID bufferIn;
	UINT64 param1;
	UINT64 param2;
	_HV_X64_HYPERCALL_INPUT input;
} HV_WORKER_LOG_DATA, * PHV_WORKER_LOG_DATA;

class HypercallMonitor : public HypercallFuzzing
{
private:
	static HypercallMonitor* activeLogObject;
	static void	LogRoutineStatic(PVOID Parameter);

protected:
	HV_HOOKING_HCALL_STATS stats[HYPERCALL_LAST_NR + 1];
	HV_HOOKING_HCALL_CONF monitorConf[HYPERCALL_LAST_NR + 1];
	HANDLE logHandle;
	FAST_MUTEX logHandleLock;

	void LogRoutine(PVOID Parameter);
	virtual void preHypercall(HV_X64_HYPERCALL_INPUT InputValue, ULONGLONG InputPa, ULONGLONG OutputPa);

public:
	HypercallMonitor();
	~HypercallMonitor();

	bool startLogging(PUNICODE_STRING filename);
	void stopLogging();

	void clearStats();
	UINT32 getStats(PHV_HOOKING_HCALL_STATS);
	bool setMonitorConf(PHV_HOOKING_HCALL_CONF, UINT32 syscall);
};