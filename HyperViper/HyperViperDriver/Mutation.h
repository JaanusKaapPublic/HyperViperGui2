#pragma once
#include<ntddk.h>
#include "HVstructures.h"

typedef struct _MUTATION_SPECIAL_VALUES
{
	UINT8 size;
	UINT64 value;
}MUTATION_SPECIAL_VALUES, *PMUTATION_SPECIAL_VALUES;


class Mutation
{
private:
	static UINT8 bits[];
	static MUTATION_SPECIAL_VALUES specials[];
	static UINT32 specialsCount;

public:
	static UINT32 mutate(PVOID buffer, UINT32 len, PHV_MUTATION_CONF conf);
	static UINT32 mutateIncrmentSingle(PVOID buffer, UINT32 len, PHV_MUTATION_CONF conf);
	static UINT32 mutateFlipBitsSingle(PVOID buffer, UINT32 len, PHV_MUTATION_CONF conf);
	static UINT32 mutateSpecialsSingle(PVOID buffer, UINT32 len, PHV_MUTATION_CONF conf);
	static UINT32 mutateIncrmentMultiple(PVOID buffer, UINT32 len, PHV_MUTATION_CONF conf);
	static UINT32 mutateFlipBitsMultiple(PVOID buffer, UINT32 len, PHV_MUTATION_CONF conf);
};