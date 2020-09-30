#include<ntddk.h>
#include"Memory.h"

void* __cdecl operator new(size_t Size) 
{
	PVOID pData = ExAllocatePoolWithTag(NonPagedPoolNx, Size, ((ULONG)'++CS'));
	if (pData != NULL)
		RtlZeroMemory(pData, Size);
	return pData;
}

void __cdecl operator delete(void* pData, unsigned __int64 something) 
{
	UNREFERENCED_PARAMETER(something);
	if (pData != NULL)
	{
		ExFreePoolWithTag(pData, ((ULONG)'++CS'));
	}
}