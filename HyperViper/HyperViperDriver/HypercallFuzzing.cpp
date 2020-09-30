#include "HypercallFuzzing.h"
#include "Mutation.h"


HypercallFuzzing::HypercallFuzzing(void)
{
#ifdef _DEBUG
	DbgPrint("[DEBUG][HypercallFuzzing::HypercallFuzzing] Constructor\n");
#endif
	memset(&mutations, 0, sizeof(mutations));
}

bool HypercallFuzzing::fuzzHypercall(HV_X64_HYPERCALL_INPUT hvInput, void* bufferIn, UINT32 bufferInLen, PHV_MUTATION_CONF conf)
{
	HV_X64_HYPERCALL_OUTPUT out;
	void* tmp = ExAllocatePoolWithTag(NonPagedPoolNx, 4096, 0x76687668);
	void* ptr = ExAllocatePoolWithTag(NonPagedPoolNx, bufferInLen, 0x76687668);
	DbgPrint("Fuzzing hypercall 0x%X\n", hvInput.CallCode);
	for (UINT32 x = 0; x < conf->count; x++)
	{
		memcpy(ptr, bufferIn, bufferInLen);
		Mutation::mutate(ptr, bufferInLen, conf);
		this->hypercall(hvInput, ptr, bufferInLen, tmp, 4096, &out);
	}
	DbgPrint(">>ended\n");
	ExFreePool(ptr);
	ExFreePool(tmp);
	return true;
}

void HypercallFuzzing::preHypercall(HV_X64_HYPERCALL_INPUT InputValue, ULONGLONG InputPa, ULONGLONG OutputPa)
{
	ULONGLONG inPA;
	PMDL inMdl = NULL;
	UINT8* inVM = NULL;
	_MM_COPY_ADDRESS addr;
	SIZE_T tmp;

	if (mutations[InputValue.CallCode].type == NONE || mutations[InputValue.CallCode].target == 0)
		return;

	mutations[InputValue.CallCode].target--;
	addr.PhysicalAddress.QuadPart = InputPa;
	if (generateMDL(mutations[InputValue.CallCode].maxLength, &inPA, &inMdl, (PVOID*)&inVM) != STATUS_SUCCESS)
		return;

	for (UINT32 x = 0; x < mutations[InputValue.CallCode].count; x++)
	{
		MmCopyMemory(inVM, addr, mutations[InputValue.CallCode].maxLength, MM_COPY_MEMORY_PHYSICAL, &tmp);
		Mutation::mutate(inVM, mutations[InputValue.CallCode].maxLength, &mutations[InputValue.CallCode]);
		__try
		{
			HvlInvokeHypercall(InputValue, inPA, OutputPa);
		}
		__except (EXCEPTION_EXECUTE_HANDLER)
		{
		}
	}

	ExFreePool(inVM);
	if (inMdl)
	{
		MmFreePagesFromMdlEx(inMdl, 0);
		ExFreePool(inMdl);
	}
}

bool HypercallFuzzing::setMutationConf(UINT32 hypercall, PHV_MUTATION_CONF conf)
{
	if (hypercall > HYPERCALL_LAST_NR)
		return false;
	if (hypercall == 0)
	{
		for (int x = 1; x <= HYPERCALL_LAST_NR; x++)
			memcpy(&mutations[x], conf, sizeof(HV_MUTATION_CONF));
	}
	else
	{
		memcpy(&mutations[hypercall], conf, sizeof(HV_MUTATION_CONF));
	}
	return true;
}
