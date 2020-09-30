#include "HypercallCore.h"

NTSTATUS HypercallCore::generateMDL(ULONG size, PULONGLONG pa, PMDL* mdl, PVOID* ptr)
{
	PHYSICAL_ADDRESS low, high;
	NTSTATUS status = STATUS_SUCCESS;

	low.QuadPart = 0;
	high.QuadPart = ~0ULL;
	*mdl = NULL;

	*mdl = MmAllocatePartitionNodePagesForMdlEx(low, high, low, ROUND_TO_PAGES(size), MmCached, KeGetCurrentNodeNumber(), MM_ALLOCATE_REQUIRE_CONTIGUOUS_CHUNKS | MM_ALLOCATE_FULLY_REQUIRED | MM_DONT_ZERO_ALLOCATION, NULL);
	if (!(*mdl))
		status = STATUS_INSUFFICIENT_RESOURCES;

	if (status == STATUS_SUCCESS && ptr)
	{
		*ptr = MmGetSystemAddressForMdlSafe(*mdl, MdlMappingNoExecute);
		if (!(*ptr))
			status = STATUS_INSUFFICIENT_RESOURCES;
	}

	if (status == STATUS_SUCCESS)
		*pa = *MmGetMdlPfnArray(*mdl) << PAGE_SHIFT;

	if (status != STATUS_SUCCESS)
	{
		MmFreePagesFromMdlEx(*mdl, 0);
		ExFreePool(*mdl);
	}
	return status;
}

NTSTATUS HypercallCore::generateMDLs(PVOID inBuffer, ULONG inBufferSize, ULONG outBufferSize, PULONGLONG inPA, PMDL* inMdl, PULONGLONG outPA, PMDL* outMdl)
{
	NTSTATUS status = STATUS_SUCCESS;
	PVOID inPtr, outPtr;

	*inPA = NULL;
	*inMdl = NULL;
	*outPA = NULL;
	*outMdl = NULL;

	if (inBufferSize)
	{
		status = generateMDL(inBufferSize, inPA, inMdl, &inPtr);
		if (status != STATUS_SUCCESS)
		{
			return status;
		}
		RtlCopyMemory(inPtr, inBuffer, inBufferSize);
	}
	if (outBufferSize)
	{
		status = generateMDL(outBufferSize, outPA, outMdl, &outPtr);
		if (status != STATUS_SUCCESS)
		{
			if (*inMdl)
			{
				MmFreePagesFromMdlEx(*inMdl, 0);
				ExFreePool(*inMdl);
			}
			*inMdl = NULL;
			return status;
		}
		RtlZeroMemory(outPtr, outBufferSize);
	}
	return status;
}

NTSTATUS HypercallCore::hypercall(HV_X64_HYPERCALL_INPUT hvInput, void* bufferIn, UINT32 bufferInLen, void* bufferOut, UINT32 bufferOutLen, PHV_X64_HYPERCALL_OUTPUT hvOutput)
{
#ifdef _DEBUG
	DbgPrint("[DEBUG][HypercallCore]hypercall\n");
#endif
	NTSTATUS status = STATUS_SUCCESS;
	ULONGLONG inPA, outPA;
	PMDL inMdl = NULL, outMdl = NULL;

	if (hvInput.IsFast && bufferInLen != 16)
		return STATUS_NDIS_INVALID_LENGTH;

	if (hvInput.IsFast)
	{
		inPA = ((ULONGLONG*)bufferIn)[0];
		outPA = ((ULONGLONG*)bufferIn)[1];
	}
	else
	{
		status = generateMDLs(bufferIn, bufferInLen, bufferOutLen, &inPA, &inMdl, &outPA, &outMdl);
	}


	if (status == STATUS_SUCCESS)
	{
		__try
		{
#ifdef _DEBUG
			DbgPrint("Making hypercall[0x%llX]: code=0x%X   fast=0x%X   count=0x%X   index=0x%X  input_len = 0x%X\n", hvInput, hvInput.CallCode, hvInput.IsFast, hvInput.CountOfElements, hvInput.RepStartIndex, bufferInLen);
#endif
			*hvOutput = HvlInvokeHypercall(hvInput, inPA, outPA);
		}
		__except (EXCEPTION_EXECUTE_HANDLER)
		{
			status = STATUS_ILLEGAL_INSTRUCTION;
		}
	}

	if (status == STATUS_SUCCESS && !hvInput.IsFast)
	{
		if (hvOutput->CallStatus == 0x0)
		{
			void* ptr = MmGetSystemAddressForMdlSafe(outMdl, NormalPagePriority);
			if (ptr)
			{
				RtlCopyMemory(bufferOut, ptr, bufferOutLen);
			}
			else
			{
				status = STATUS_INSUFFICIENT_RESOURCES;
			}
		}
	}
	if (inMdl)
	{
		MmFreePagesFromMdlEx(inMdl, 0);
		ExFreePool(inMdl);
	}
	if (outMdl)
	{
		MmFreePagesFromMdlEx(outMdl, 0);
		ExFreePool(outMdl);
	}
	return status;
}