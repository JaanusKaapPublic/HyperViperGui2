#pragma once
#include "HVenums.h"

typedef unsigned short HV_STATUS;

/////////////////////////////////////////////////
// GENERIC STRUCTURES
/////////////////////////////////////////////////
typedef struct _HV_MUTATION_CONF
{
	UINT8 target;
	UINT8 dbgMsg;
	HV_MUTATION_TYPE type;
	UINT32 seed;
	UINT32 minChanges;
	UINT32 maxChanges;
	UINT32 maxLength;
	UINT32 count;
} HV_MUTATION_CONF, * PHV_MUTATION_CONF;

/////////////////////////////////////////////////
// HYPERCALL RELATED STRUCTURES
/////////////////////////////////////////////////
typedef struct _HV_X64_HYPERCALL_INPUT
{
	UINT32 CallCode : 16;
	UINT32 IsFast : 1;
	UINT32 dontCare1 : 15;
	UINT32 CountOfElements : 12;
	UINT32 dontCare2 : 4;
	UINT32 RepStartIndex : 12;
	UINT32 dontCare3 : 4;
} HV_X64_HYPERCALL_INPUT, * PHV_X64_HYPERCALL_INPUT;

typedef struct _HV_X64_HYPERCALL_OUTPUT
{
	HV_STATUS CallStatus;
	UINT16 dontCare1;
	UINT32 ElementsProcessed : 12;
	UINT32 dontCare2 : 20;
} HV_X64_HYPERCALL_OUTPUT, * PHV_X64_HYPERCALL_OUTPUT;

typedef struct _HV_HOOKING_HCALL_CONF
{
	UINT8 breakpoint;
	UINT8 dbgPrint;
	UINT32 log;
	UINT32 bufferSize;
}HV_HOOKING_HCALL_CONF, * PHV_HOOKING_HCALL_CONF;

typedef struct _HV_HOOKING_HCALL_CONF_SET
{
	UINT32 hypercall;
	HV_HOOKING_HCALL_CONF conf;
}HV_HOOKING_HCALL_CONF_SET, * PHV_HOOKING_HCALL_CONF_SET;

typedef struct _HV_HOOKING_HCALL_STATS
{
	UINT32 count;
	UINT16 lastElementCount;
	HANDLE lastProcessID;
	UINT8 fast;
	UINT8 slow;
}HV_HOOKING_HCALL_STATS, * PHV_HOOKING_HCALL_STATS;

typedef struct _HV_FUZZING_HCALL
{
	HV_X64_HYPERCALL_INPUT code;
	HV_MUTATION_CONF conf;
}HV_FUZZING_HCALL, * PHV_FUZZING_HCALL;
