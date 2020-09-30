#include "Mutation.h"
#include "Random.h"

UINT8 Mutation::bits[] = { 1, 2, 4, 8, 16, 32, 64, 128 };
MUTATION_SPECIAL_VALUES Mutation::specials[] =
{
	{1, 0x00},
	{1, 0x01},
	{1, 0x7E},
	{1, 0x7F},
	{1, 0x80},
	{1, 0x81},
	{1, 0xF0},
	{1, 0xFE},
	{1, 0xFF},

	{2, 0x0000},
	{2, 0x0001},
	{2, 0x7FFE},
	{2, 0x7FFF},
	{2, 0x8000},
	{2, 0x8001},
	{2, 0xFFFE},
	{2, 0xFFFF},

	{4, 0x00000000},
	{4, 0x00000001},
	{4, 0x7FFFFFFE},
	{4, 0x7FFFFFFF},
	{4, 0x80000000},
	{4, 0x80000001},
	{4, 0xFFFFFFFE},
	{4, 0xFFFFFFFF},

	{8, 0x0000000000000000},
	{8, 0x0000000000000001},
	{8, 0x7FFFFFFFFFFFFFFE},
	{8, 0x7FFFFFFFFFFFFFFF},
	{8, 0x8000000000000000},
	{8, 0x8000000000000001},
	{8, 0xFFFFFFFFFFFFFFFE},
	{8, 0xFFFFFFFFFFFFFFFF},
};

UINT32 Mutation::specialsCount = sizeof(specials) / sizeof(MUTATION_SPECIAL_VALUES);

UINT32 Mutation::mutate(PVOID buffer, UINT32 len, PHV_MUTATION_CONF conf)
{
	if (len > conf->maxLength)
		len = conf->maxLength;
	if (conf->type == INCREMENTING_SINGLE)
		return mutateIncrmentSingle(buffer, len, conf);
	if (conf->type == BIT_FLIPPING_SINGLE)
		return mutateFlipBitsSingle(buffer, len, conf);
	if (conf->type == SPECIAL_VALUE_SINGLE)
		return mutateSpecialsSingle(buffer, len, conf);
	if (conf->type == INCREMENTING_MULTIPLE)
		return mutateIncrmentMultiple(buffer, len, conf);
	if (conf->type == BIT_FLIPPING_MULTIPLE)
		return mutateFlipBitsMultiple(buffer, len, conf);
	return 0xFFFFFFFF;
}


UINT32 Mutation::mutateIncrmentSingle(PVOID buffer, UINT32 len, PHV_MUTATION_CONF conf)
{
	UINT32 cur = conf->seed++;
	UINT32 pos = (cur / 0xFF) % len;
	if(conf->dbgMsg)
		DbgPrint("%d>[0x%X/0x%X] 0x%02X->", conf->seed, pos, len, ((PUINT8)buffer)[pos]);
	((PUINT8)buffer)[pos] += (cur % 255) + 1;
	if (conf->dbgMsg)
		DbgPrint("0x%02X\n", ((PUINT8)buffer)[pos]);
	return 1;
}


UINT32 Mutation::mutateFlipBitsSingle(PVOID buffer, UINT32 len, PHV_MUTATION_CONF conf)
{
	UINT32 cur = conf->seed++;
	UINT32 pos = (cur / 8) % len;
	if (conf->dbgMsg)
		DbgPrint("%d>[0x%X/0x%X] 0x%02X->", conf->seed, pos, len, ((PUINT8)buffer)[pos]);
	((PUINT8)buffer)[pos] ^= bits[cur % 8];
	if (conf->dbgMsg)
		DbgPrint("0x%02X\n", ((PUINT8)buffer)[pos]);
	return 1;
}

UINT32 Mutation::mutateSpecialsSingle(PVOID buffer, UINT32 len, PHV_MUTATION_CONF conf)
{
	UINT32 cur = conf->seed++;
	UINT32 pos = (cur / specialsCount) % len;
	UINT32 val = cur % specialsCount;
	
	if (len - pos < specials[val].size)
	{
		if (conf->dbgMsg)
			DbgPrint("%d>[0x%X/0x%X] UNCHANGED->UNCHANGED\n", conf->seed, pos, len);
		return 1;
	}

	PVOID ptr = &((PUINT8)buffer)[pos];
	if (specials[val].size == 1)
	{
		DbgPrint("%d>[0x%X/0x%X] 0x%02X->", conf->seed, pos, len, ((PUINT8)buffer)[pos]);
		((PUINT8)buffer)[pos] = (UINT8)specials[val].value;
		DbgPrint("0x%02X\n", ((PUINT8)buffer)[pos]);
	}
	else if (specials[val].size == 2)
	{
		if (conf->dbgMsg)
			DbgPrint("%d>[0x%X/0x%X] 0x%04X->", conf->seed, pos, len, *(PUINT16)ptr);
		*(PUINT16)ptr = (UINT16)specials[val].value;
		if (conf->dbgMsg)
			DbgPrint("0x%04X\n", *(PUINT16)ptr);
	}
	else if (specials[val].size == 4)
	{
		if (conf->dbgMsg)
			DbgPrint("%d>[0x%X/0x%X] 0x%08X->", conf->seed, pos, len, *(PUINT32)ptr);
		*(PUINT32)ptr = (UINT32)specials[val].value;
		if (conf->dbgMsg)
			DbgPrint("0x%08X\n", *(PUINT32)ptr);
	}
	else if (specials[val].size == 8)
	{
		if (conf->dbgMsg)
			DbgPrint("%d>[0x%X/0x%X] 0x%llX->", conf->seed, pos, len, *(PUINT64)ptr);
		*(PUINT64)ptr = specials[val].value;
		if (conf->dbgMsg)
			DbgPrint("0x%llX\n", *(PUINT64)ptr);
	}
	return 1;
}

UINT32 Mutation::mutateIncrmentMultiple(PVOID buffer, UINT32 len, PHV_MUTATION_CONF conf)
{
	Random::setCurrentSeed(conf->seed++);
	UINT32 count = (Random::rand() % (conf->maxChanges - conf->minChanges + 1)) + conf->minChanges;
	if (conf->dbgMsg)
		DbgPrint("-----%d changes-----\n", count);
	for (UINT32 x = 0; x < count; x++)
	{
		UINT32 pos = Random::rand() % len;
		if (conf->dbgMsg)
			DbgPrint("  [0x%X/0x%X] 0x%02X->", pos, len, ((PUINT8)buffer)[pos]);
		((PUINT8)buffer)[pos] += (Random::rand() % 0xFF) + 1;
		if (conf->dbgMsg)
			DbgPrint("0x%02X\n", ((PUINT8)buffer)[pos]);
	}
	return count;
}

UINT32 Mutation::mutateFlipBitsMultiple(PVOID buffer, UINT32 len, PHV_MUTATION_CONF conf)
{
	Random::setCurrentSeed(conf->seed++);
	UINT32 count = (Random::rand() % (conf->maxChanges - conf->minChanges + 1)) + conf->minChanges;
	if (conf->dbgMsg)
		DbgPrint("-----%d changes-----\n", count);
		
	for (UINT32 x = 0; x < count; x++)
	{
		UINT32 pos = Random::rand() % len;
		if (conf->dbgMsg)
			DbgPrint("  [0x%X/0x%X] 0x%02X->", pos, len, ((PUINT8)buffer)[pos]);
		((PUINT8)buffer)[pos] ^= bits[Random::rand() % 8];
		if (conf->dbgMsg)
			DbgPrint("0x%02X\n", ((PUINT8)buffer)[pos]);
	}
	return 1;
}