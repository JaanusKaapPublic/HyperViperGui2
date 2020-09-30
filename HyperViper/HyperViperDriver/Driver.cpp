#include<ntddk.h>
#include"Driver.h"
#include "DevIoCtrlHandler.h"

DevIoCtrlHandler* devIoCtrlHandler;


NTSTATUS DriverIoControl(PDEVICE_OBJECT DeviceObject, PIRP Irp);
NTSTATUS TracedrvDispatchOpenClose(IN PDEVICE_OBJECT pDO, IN PIRP Irp);
VOID DriverUnload(PDRIVER_OBJECT  DriverObject);


extern "C" NTSTATUS DriverEntry(PDRIVER_OBJECT pDriverObject, PUNICODE_STRING pRegistryPath)
{
	UNREFERENCED_PARAMETER(pRegistryPath);
#ifdef _DEBUG
	DbgPrint("[DEBUG][DriverEntry]Starting HyperViper\n");

	int aaa;
	int* bbb = &aaa;
	DbgPrint("[DEBUG][DriverEntry]TESTING: 0x%llX vs 0x%llX\n", bbb, bbb+1);
#endif

	NTSTATUS NtStatus = STATUS_SUCCESS;
	PDEVICE_OBJECT pDeviceObject = NULL;
	UNICODE_STRING usDriverName, usDosDeviceName;

	pDriverObject->MajorFunction[IRP_MJ_CLOSE] = TracedrvDispatchOpenClose;
	pDriverObject->MajorFunction[IRP_MJ_CREATE] = TracedrvDispatchOpenClose;
	pDriverObject->MajorFunction[IRP_MJ_DEVICE_CONTROL] = DriverIoControl;

	RtlInitUnicodeString(&usDriverName, DEVICE_NAME);
	RtlInitUnicodeString(&usDosDeviceName, SYMBOLIC_LINK_NAME);
	NtStatus = IoCreateDevice(pDriverObject, 0,	&usDriverName, 	FILE_DEVICE_UNKNOWN, FILE_DEVICE_SECURE_OPEN, FALSE, &pDeviceObject);
	if (NtStatus == STATUS_SUCCESS)
	{
		IoCreateSymbolicLink(&usDosDeviceName, &usDriverName);
		pDriverObject->DriverUnload = DriverUnload;
	}

	devIoCtrlHandler = new DevIoCtrlHandler();

	return NtStatus;
}

NTSTATUS TracedrvDispatchOpenClose(IN PDEVICE_OBJECT pDO, IN PIRP Irp)
{
	UNREFERENCED_PARAMETER(pDO);
	Irp->IoStatus.Status = STATUS_SUCCESS;
	Irp->IoStatus.Information = 0;
	PAGED_CODE();
	IoCompleteRequest(Irp, IO_NO_INCREMENT);
	return STATUS_SUCCESS;
}

VOID DriverUnload(PDRIVER_OBJECT  DriverObject)
{
#ifdef _DEBUG
	DbgPrint("[DEBUG][DriverEntry]Stopping HyperViper\n");
#endif
	delete devIoCtrlHandler;

#ifdef _DEBUG
	DbgPrint("[DEBUG][DriverEntry]Closing driver related stuff\n");
#endif
	UNICODE_STRING usDosDeviceName;    
    RtlInitUnicodeString(&usDosDeviceName, SYMBOLIC_LINK_NAME);
    IoDeleteSymbolicLink(&usDosDeviceName);
    IoDeleteDevice(DriverObject->DeviceObject);
#ifdef _DEBUG
	DbgPrint("[DEBUG][DriverEntry]HyperViper stopped\n");
#endif
}

NTSTATUS DriverIoControl(PDEVICE_OBJECT DeviceObject, PIRP Irp)
{
	UNREFERENCED_PARAMETER(DeviceObject);
	NTSTATUS NtStatus = STATUS_NOT_SUPPORTED;
	PIO_STACK_LOCATION pIoStackIrp = IoGetCurrentIrpStackLocation(Irp);
	ULONGLONG dataLen = 0;
	
	if (pIoStackIrp)
	{
#ifdef _DEBUG
		DbgPrint("DriverIoControl called with control code 0x%X\n", pIoStackIrp->Parameters.DeviceIoControl.IoControlCode);
#endif
		switch (pIoStackIrp->Parameters.DeviceIoControl.IoControlCode)
		{
		case IOCTL_HYPERCALLS_CALL:
			NtStatus = devIoCtrlHandler->hypercallCall(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_HYPERCALLS_HOOK:
			NtStatus = devIoCtrlHandler->hypercallHook(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_HYPERCALLS_UNHOOK:
			NtStatus = devIoCtrlHandler->hypercallUnhook(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_HYPERCALLS_GET_STATS:
			NtStatus = devIoCtrlHandler->hypercallGetStats(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_HYPERCALLS_SET_MONITOR_CONF:
			NtStatus = devIoCtrlHandler->hypercallSetMonitorConf(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_HYPERCALLS_START_LOG:
			NtStatus = devIoCtrlHandler->hypercallStartLogging(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_HYPERCALLS_STOP_LOG:
			NtStatus = devIoCtrlHandler->hypercallStopLogging(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_HYPERCALLS_CLEAR_STATS:
			NtStatus = devIoCtrlHandler->hypercallClearStats(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_HYPERCALLS_IS_HOOKED:
			NtStatus = devIoCtrlHandler->hypercallIsHooked(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_HYPERCALLS_FUZZ:
			NtStatus = devIoCtrlHandler->hypercallFuzz(Irp, pIoStackIrp, &dataLen);
			break;
		}
	}

	Irp->IoStatus.Status = NtStatus;
	Irp->IoStatus.Information = dataLen;
	IoCompleteRequest(Irp, IO_NO_INCREMENT);

	return NtStatus;

}