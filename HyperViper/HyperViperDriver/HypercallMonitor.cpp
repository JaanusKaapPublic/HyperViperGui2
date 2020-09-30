#include"HypercallMonitor.h"

HypercallMonitor* HypercallMonitor::activeLogObject = NULL;

void HypercallMonitor::LogRoutineStatic(PVOID Parameter)
{
	HypercallMonitor* obj = activeLogObject;
	if (obj)
		obj->LogRoutine(Parameter);
}

HypercallMonitor::HypercallMonitor() : HypercallFuzzing()
{
#ifdef _DEBUG
	DbgPrint("[DEBUG][HypercallMonitor::HypercallMonitor] Constructor\n");
#endif
	memset(stats, 0, sizeof(stats));
	memset(monitorConf, 0, sizeof(monitorConf));
	logHandle = NULL;
	ExInitializeFastMutex(&logHandleLock);
}

HypercallMonitor::~HypercallMonitor()
{
#ifdef _DEBUG
	DbgPrint("[DEBUG][HypercallMonitor::~HypercallMonitor] Constructor\n");
#endif
	stopLogging();
	unhook();
}

void HypercallMonitor::preHypercall(HV_X64_HYPERCALL_INPUT InputValue, ULONGLONG InputPa, ULONGLONG OutputPa)
{
	//Status storing part
	stats[InputValue.CallCode].count++;
	if(InputValue.IsFast)
		stats[InputValue.CallCode].fast = 1;
	else
		stats[InputValue.CallCode].slow = 1;
	stats[InputValue.CallCode].lastElementCount = InputValue.CountOfElements;
	stats[InputValue.CallCode].lastProcessID = PsGetProcessId(PsGetCurrentProcess());

	if (monitorConf[InputValue.CallCode].dbgPrint)
		DbgPrint("Detected hypercall 0x%X (%s) count-elements: 0x%X   inputValue:0x%llX   param-1: 0x%llX    param-2: 0x%llX\n",
			InputValue.CallCode,
			InputValue.IsFast ? "FAST" : "SLOW",
			InputValue.CountOfElements,
			InputValue,
			InputPa,
			OutputPa);
	
	if (monitorConf[InputValue.CallCode].breakpoint)
	{
		monitorConf[InputValue.CallCode].breakpoint = 0;
		DbgBreakPoint();
	}

	if (monitorConf[InputValue.CallCode].log > 0)
	{
#ifdef _DEBUG_HEAVY
		DbgPrint("[DEBUG][HypercallMonitor::preHypercall] storing hypercall 0x%X (buffer sized 0x%X and 0x%X left)\n", InputValue.CallCode, monitorConf[InputValue.CallCode].bufferSize, monitorConf[InputValue.CallCode].log);
#endif
		PHV_WORKER_LOG_DATA data = (PHV_WORKER_LOG_DATA)ExAllocatePoolWithTag(NonPagedPoolNx, sizeof(HV_WORKER_LOG_DATA), 0x76687668);
		data->WorkItem = (PWORK_QUEUE_ITEM)ExAllocatePoolWithTag(NonPagedPoolNx, sizeof(WORK_QUEUE_ITEM), 0x76687668);
		data->bufferIn = NULL;
		data->param1 = InputPa;
		data->param2 = OutputPa;
		data->input = InputValue;
		if (!InputValue.IsFast && InputPa)
		{
			data->bufferIn = ExAllocatePoolWithTag(NonPagedPoolNx, monitorConf[InputValue.CallCode].bufferSize, 0x76687668);
			SIZE_T size;
			_MM_COPY_ADDRESS addr;
			addr.PhysicalAddress.QuadPart = InputPa;
			MmCopyMemory(data->bufferIn, addr, monitorConf[InputValue.CallCode].bufferSize, MM_COPY_MEMORY_PHYSICAL, &size);
		}
		ExInitializeWorkItem(data->WorkItem, LogRoutineStatic, data);
		ExQueueWorkItem(data->WorkItem, DelayedWorkQueue);
	}

	HypercallFuzzing::preHypercall(InputValue, InputPa, OutputPa);
}

void HypercallMonitor::LogRoutine(PVOID Parameter)
{
#ifdef _DEBUG
	DbgPrint("[DEBUG][HypercallMonitor::LogRoutine] starts\n");
#endif
	PHV_WORKER_LOG_DATA data = (PHV_WORKER_LOG_DATA)Parameter;
#ifdef _DEBUG
	DbgPrint("[DEBUG][HypercallMonitor::LogRoutine] starts with hypercall 0x%X (buffer sized 0x%X and 0x%X left)\n", data->input.CallCode, monitorConf[data->input.CallCode].bufferSize, monitorConf[data->input.CallCode].log);
#endif

	ExAcquireFastMutex(&logHandleLock);	
	if (!logHandle || !monitorConf[data->input.CallCode].log)
	{
		ExReleaseFastMutex(&logHandleLock);
		ExFreePool(data->WorkItem);
		if (data->bufferIn)
			ExFreePool(data->bufferIn);
		ExFreePool(data);
		return;
	}

	if (monitorConf[data->input.CallCode].log > 0 && monitorConf[data->input.CallCode].log != 0xFFFFFFFF)
		monitorConf[data->input.CallCode].log--;

#ifdef _DEBUG
	DbgPrint("[DEBUG][HypercallMonitor::LogRoutine] logging hypercall 0x%X (%s) - 0x%X bytes from 0x%llX\n", data->input.CallCode, (data->input.IsFast ? "FAST" : "SLOW"), monitorConf[data->input.CallCode].bufferSize, data->bufferIn);
#endif

	IO_STATUS_BLOCK ioStatusBlock;
	ZwWriteFile(logHandle, NULL, NULL, NULL, &ioStatusBlock, &(data->input), sizeof(_HV_X64_HYPERCALL_INPUT), NULL, NULL);
	if (data->input.IsFast)
	{
		ZwWriteFile(logHandle, NULL, NULL, NULL, &ioStatusBlock, &data->param1, sizeof(UINT64), NULL, NULL);
		ZwWriteFile(logHandle, NULL, NULL, NULL, &ioStatusBlock, &data->param2, sizeof(UINT64), NULL, NULL);
	}
	else
	{
		if (data->bufferIn)
		{
			ZwWriteFile(logHandle, NULL, NULL, NULL, &ioStatusBlock, &(monitorConf[data->input.CallCode].bufferSize), sizeof(UINT32), NULL, NULL);
			ZwWriteFile(logHandle, NULL, NULL, NULL, &ioStatusBlock, data->bufferIn, monitorConf[data->input.CallCode].bufferSize, NULL, NULL);
		}
		else
		{
			ZwWriteFile(logHandle, NULL, NULL, NULL, &ioStatusBlock, "\x00\x00\x00\x00", sizeof(UINT32), NULL, NULL);
		}
	}
	ExReleaseFastMutex(&logHandleLock);
	ExFreePool(data->WorkItem);
	if (data->bufferIn)
		ExFreePool(data->bufferIn);
	ExFreePool(data);
}

bool HypercallMonitor::startLogging(PUNICODE_STRING filename)
{
#ifdef _DEBUG
	DbgPrint("[DEBUG][HypercallMonitor::startLogging] starts\n");
#endif
	ExAcquireFastMutex(&logHandleLock);
	if (activeLogObject || logHandle)
	{
		ExAcquireFastMutex(&logHandleLock);
		return false;
	}

	OBJECT_ATTRIBUTES  objAttr;
	InitializeObjectAttributes(&objAttr, filename, OBJ_CASE_INSENSITIVE | OBJ_KERNEL_HANDLE, NULL, NULL);
	IO_STATUS_BLOCK    ioStatusBlock;
	NTSTATUS ntstatus = ZwCreateFile(&logHandle, GENERIC_WRITE, &objAttr, &ioStatusBlock, NULL, FILE_ATTRIBUTE_NORMAL, 0, FILE_OVERWRITE_IF, FILE_SYNCHRONOUS_IO_NONALERT, NULL, 0);
	if (NT_SUCCESS(ntstatus))
	{
		ZwWriteFile(logHandle, NULL, NULL, NULL, &ioStatusBlock, "HVCL", 4, NULL, NULL);
		activeLogObject = this;
		ExReleaseFastMutex(&logHandleLock);
		return true;
	}
	logHandle = NULL;
	ExReleaseFastMutex(&logHandleLock);
	return false;
}

void HypercallMonitor::stopLogging()
{
#ifdef _DEBUG
	DbgPrint("[DEBUG][HypercallMonitor::stopLogging] starts\n");
#endif
	ExAcquireFastMutex(&logHandleLock);
	activeLogObject = NULL;
	if(logHandle)
		ZwClose(logHandle);
	logHandle = NULL;
	ExReleaseFastMutex(&logHandleLock);
}


void HypercallMonitor::clearStats()
{
#ifdef _DEBUG
	DbgPrint("[DEBUG][HypercallMonitor::clearStats] starts\n");
#endif
	ExAcquireFastMutex(&logHandleLock);
	memset(stats, 0, sizeof(stats));
	ExReleaseFastMutex(&logHandleLock);
}

UINT32 HypercallMonitor::getStats(PHV_HOOKING_HCALL_STATS statsOut)
{
#ifdef _DEBUG
	DbgPrint("[DEBUG][HypercallMonitor::getStats] starts\n");
#endif
	memcpy(statsOut, stats, sizeof(stats));
	return sizeof(stats);
}

bool HypercallMonitor::setMonitorConf(PHV_HOOKING_HCALL_CONF confIn, UINT32 syscall)
{
#ifdef _DEBUG
	DbgPrint("[DEBUG][HypercallMonitor::setMonitorConf] starts\n");
	DbgPrint("[DEBUG][HypercallMonitor::setMonitorConf] syscall = 0x%X\n", syscall);
	DbgPrint("[DEBUG][HypercallMonitor::setMonitorConf] breakpoint = 0x%X\n", confIn->breakpoint);
	DbgPrint("[DEBUG][HypercallMonitor::setMonitorConf] bufferSize = 0x%X\n", confIn->bufferSize);
	DbgPrint("[DEBUG][HypercallMonitor::setMonitorConf] dbgPrint = 0x%X\n", confIn->dbgPrint);
	DbgPrint("[DEBUG][HypercallMonitor::setMonitorConf] log = 0x%X\n", confIn->log);
#endif
	if (syscall > HYPERCALL_LAST_NR)
		return false;

	if (syscall > 0)
	{
		memcpy(monitorConf + syscall, confIn, sizeof(HV_HOOKING_HCALL_CONF));
	}
	else 
	{
		for(int x = 0; x < HYPERCALL_LAST_NR + 1; x++)
			memcpy(monitorConf + x, confIn, sizeof(HV_HOOKING_HCALL_CONF));
	}
	return true;
}
