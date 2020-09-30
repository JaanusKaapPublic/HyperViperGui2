#pragma once
#include<ntddk.h>
#include <vmbuskernelmodeclientlibapi.h>

class OsPointers
{
private:
	static void* cached_HvcallCodeVa;
	static void* cached_kmclChannelListLocation;
	static PFN_VMB_PACKET_ALLOCATE cached_VmbPacketAllocate;
	static PFN_VMB_PACKET_SEND cached_VmbPacketSend;
public:
	//Hypercalls
	static void* getPtr_HvcallCodeVa(void);
};