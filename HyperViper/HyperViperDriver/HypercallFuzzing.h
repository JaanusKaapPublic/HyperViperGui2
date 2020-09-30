#pragma once
#include<ntddk.h>
#include "HVdef.h"
#include"HypercallHook.h"

class HypercallFuzzing : public HypercallHook
{
protected:
	HV_MUTATION_CONF mutations[HYPERCALL_LAST_NR + 1];

	virtual void preHypercall(HV_X64_HYPERCALL_INPUT InputValue, ULONGLONG InputPa, ULONGLONG OutputPa);
public:
	HypercallFuzzing(void);
	bool fuzzHypercall(HV_X64_HYPERCALL_INPUT hvInput, void* bufferIn, UINT32 bufferInLen, PHV_MUTATION_CONF conf);
	bool setMutationConf(UINT32 hypercall, PHV_MUTATION_CONF);
};