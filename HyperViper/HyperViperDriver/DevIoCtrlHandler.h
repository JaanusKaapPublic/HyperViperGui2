#pragma once
#include"HypercallMonitor.h"

class DevIoCtrlHandler
{
private:
	HypercallMonitor* hypercalls;

public:
	DevIoCtrlHandler();
	~DevIoCtrlHandler();

	NTSTATUS hypercallCall(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen);
	NTSTATUS hypercallHook(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen);
	NTSTATUS hypercallUnhook(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen);
	NTSTATUS hypercallGetStats(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen);
	NTSTATUS hypercallSetMonitorConf(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen);
	NTSTATUS hypercallStartLogging(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen);
	NTSTATUS hypercallStopLogging(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen);
	NTSTATUS hypercallClearStats(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen);
	NTSTATUS hypercallIsHooked(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen);
	NTSTATUS hypercallFuzz(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG dataLen);
};