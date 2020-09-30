#include "DevIoCtrlHandler.h"
#include "Memory.h"

DevIoCtrlHandler::DevIoCtrlHandler()
{
#ifdef _DEBUG
	DbgPrint("[DEBUG][DevIoCtrlHandler]Initializing stuff\n");
#endif
	hypercalls = new HypercallMonitor();
}


DevIoCtrlHandler::~DevIoCtrlHandler()
{
	hypercalls->stopLogging();
	hypercalls->unhook();
	delete hypercalls;
}

NTSTATUS DevIoCtrlHandler::hypercallCall(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen)
{
#ifdef _DEBUG
	DbgPrint("[DEBUG][DevIoCtrlHandler]syscallCall\n");
#endif
	if (pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength < sizeof(HV_X64_HYPERCALL_INPUT))
		return STATUS_NDIS_INVALID_LENGTH;
	if (pIoStackIrp->Parameters.DeviceIoControl.OutputBufferLength < sizeof(HV_X64_HYPERCALL_OUTPUT))
		return STATUS_BUFFER_OVERFLOW;
	if (((PHV_X64_HYPERCALL_INPUT)Irp->AssociatedIrp.SystemBuffer)->IsFast && pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength != sizeof(HV_X64_HYPERCALL_INPUT) + 16)
		return STATUS_NDIS_INVALID_LENGTH;

	NTSTATUS status = hypercalls->hypercall(
		*(PHV_X64_HYPERCALL_INPUT)Irp->AssociatedIrp.SystemBuffer,
		(PUINT8)Irp->AssociatedIrp.SystemBuffer + sizeof(HV_X64_HYPERCALL_INPUT),
		pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength - sizeof(HV_X64_HYPERCALL_INPUT),
		(PUINT8)Irp->AssociatedIrp.SystemBuffer + sizeof(HV_X64_HYPERCALL_OUTPUT),
		pIoStackIrp->Parameters.DeviceIoControl.OutputBufferLength - sizeof(HV_X64_HYPERCALL_OUTPUT),
		(PHV_X64_HYPERCALL_OUTPUT)Irp->AssociatedIrp.SystemBuffer
	);

	if (status == STATUS_SUCCESS)
		*dataLen = pIoStackIrp->Parameters.DeviceIoControl.OutputBufferLength;	
	return status;
}

NTSTATUS DevIoCtrlHandler::hypercallHook(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen)
{
	UNREFERENCED_PARAMETER(Irp);
	UNREFERENCED_PARAMETER(pIoStackIrp);
	UNREFERENCED_PARAMETER(dataLen);
	hypercalls->hook();
	return STATUS_SUCCESS;
}

NTSTATUS DevIoCtrlHandler::hypercallUnhook(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen)
{
	UNREFERENCED_PARAMETER(Irp);
	UNREFERENCED_PARAMETER(pIoStackIrp);
	UNREFERENCED_PARAMETER(dataLen);
	hypercalls->unhook();
	return STATUS_SUCCESS;
}

NTSTATUS DevIoCtrlHandler::hypercallGetStats(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen)
{
	if (pIoStackIrp->Parameters.DeviceIoControl.OutputBufferLength < sizeof(HV_HOOKING_HCALL_STATS)*(HYPERCALL_LAST_NR+1))
		return STATUS_BUFFER_TOO_SMALL;
	*dataLen = hypercalls->getStats((PHV_HOOKING_HCALL_STATS)Irp->AssociatedIrp.SystemBuffer);
	return STATUS_SUCCESS;
}

NTSTATUS DevIoCtrlHandler::hypercallSetMonitorConf(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen)
{
	UNREFERENCED_PARAMETER(dataLen);
#ifdef _DEBUG
	DbgPrint("[DEBUG][DevIoCtrlHandler]syscallSetMonitorConf\n");
#endif


	if (pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength < sizeof(HV_HOOKING_HCALL_CONF_SET))
		return STATUS_BUFFER_TOO_SMALL;

	PHV_HOOKING_HCALL_CONF_SET setConf = (PHV_HOOKING_HCALL_CONF_SET)Irp->AssociatedIrp.SystemBuffer;
	if (hypercalls->setMonitorConf(&(setConf->conf), setConf->hypercall))
		return STATUS_SUCCESS;
	return STATUS_INVALID_PARAMETER;
}

NTSTATUS DevIoCtrlHandler::hypercallStartLogging(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen)
{
	UNREFERENCED_PARAMETER(dataLen);

	if (pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength < 4 || pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength > 512)
		return STATUS_NDIS_INVALID_LENGTH;

	UNICODE_STRING uniName;
	ANSI_STRING AS;
	char name[512];
	memcpy(name, (char*)Irp->AssociatedIrp.SystemBuffer, pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength);
	name[pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength] = 0x00;

	RtlInitAnsiString(&AS, name);
	RtlAnsiStringToUnicodeString(&uniName, &AS, TRUE);

	if(hypercalls->startLogging(&uniName))
		return STATUS_SUCCESS;
	return STATUS_INVALID_PARAMETER;
}

NTSTATUS DevIoCtrlHandler::hypercallStopLogging(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen)
{
	UNREFERENCED_PARAMETER(Irp);
	UNREFERENCED_PARAMETER(pIoStackIrp);
	UNREFERENCED_PARAMETER(dataLen);
	hypercalls->stopLogging();
	return STATUS_SUCCESS;
}

NTSTATUS DevIoCtrlHandler::hypercallClearStats(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen)
{
	UNREFERENCED_PARAMETER(Irp);
	UNREFERENCED_PARAMETER(pIoStackIrp);
	UNREFERENCED_PARAMETER(dataLen);
	hypercalls->clearStats();
	return STATUS_SUCCESS;
}

NTSTATUS DevIoCtrlHandler::hypercallIsHooked(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen)
{

	if (pIoStackIrp->Parameters.DeviceIoControl.OutputBufferLength < 1)
		return STATUS_BUFFER_TOO_SMALL;

	if (hypercalls->isHooked())
	{
		*(char*)Irp->AssociatedIrp.SystemBuffer = 1;
	}
	else
	{
		*(char*)Irp->AssociatedIrp.SystemBuffer = 0;
	}
	*dataLen = 1;
	return STATUS_SUCCESS;
}

NTSTATUS DevIoCtrlHandler::hypercallFuzz(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen)
{
	UNREFERENCED_PARAMETER(dataLen);
#ifdef _DEBUG
	DbgPrint("[DEBUG][DevIoCtrlHandler]hypercallFuzz\n");
#endif
	if (pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength < sizeof(HV_FUZZING_HCALL) + 1)
		return STATUS_BUFFER_TOO_SMALL;

	PHV_FUZZING_HCALL conf = (PHV_FUZZING_HCALL)Irp->AssociatedIrp.SystemBuffer;
	UINT32 len = pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength - sizeof(HV_FUZZING_HCALL);
	PVOID buffer = (PVOID)(((UINT64)Irp->AssociatedIrp.SystemBuffer) + sizeof(HV_FUZZING_HCALL));
	if (hypercalls->fuzzHypercall(conf->code, buffer, len, &(conf->conf)))
		return STATUS_SUCCESS;
	return STATUS_INVALID_PARAMETER;
}
