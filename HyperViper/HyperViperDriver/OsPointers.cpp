#include "OsPointers.h"
#include "HypercallCore.h"
#include "CodeStolenFromOthers.h"

void* OsPointers::cached_HvcallCodeVa = NULL;
void* OsPointers::cached_kmclChannelListLocation = NULL;
PFN_VMB_PACKET_ALLOCATE OsPointers::cached_VmbPacketAllocate = NULL;
PFN_VMB_PACKET_SEND OsPointers::cached_VmbPacketSend = NULL;

void* OsPointers::getPtr_HvcallCodeVa(void)
{
	if (cached_HvcallCodeVa)
		return cached_HvcallCodeVa;
	UINT8* ptr = (UINT8*)&HvlInvokeHypercall;
#ifdef _DEBUG
	DbgPrint("[DEBUG][OsPointers::getPtr_HvcallCodeVa] HvlInvokeHypercall is at 0x%llx \n", ptr);
#endif
	//ptr += 7;
	/*UINT32 offset = *((UINT32*)ptr);*/

	//Look for "call rax"
	while (*((UINT16*)(++ptr)) != 0xD0FF);

#ifdef _DEBUG
	DbgPrint("[DEBUG][OsPointers::getPtr_HvcallCodeVa] 'call rax' is at 0x%llx \n", ptr);
#endif

	//look back until "mov rax, ???"
	while (*(UINT16*)(--ptr) != 0x8B48 || *(ptr+2) != 0x05);
#ifdef _DEBUG
	DbgPrint("[DEBUG][OsPointers::getPtr_HvcallCodeVa] 'mov rax, ???' is at 0x%llx \n", ptr);
#endif
	ptr += 3;
	UINT32 offset = *((UINT32*)ptr);
#ifdef _DEBUG
	DbgPrint("[DEBUG][OsPointers::getPtr_HvcallCodeVa] offset is 0x%llx \n", offset);
#endif
	ptr += offset + 4;	
#ifdef _DEBUG
	DbgPrint("[DEBUG][OsPointers::getPtr_HvcallCodeVa] HvcallCodeVa is 0x%llx \n", ptr);
#endif
	cached_HvcallCodeVa = ptr;
	return ptr;
}


