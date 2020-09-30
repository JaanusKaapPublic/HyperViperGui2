using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace HyperViperGuiDriver
{
    [StructLayout(LayoutKind.Explicit)]
    unsafe public struct HV_HOOKING_HCALL_STATS
    {
        [FieldOffset(0)] public uint count;
        [FieldOffset(4)] public ushort lastElementCount;
        [FieldOffset(8)] public ulong lastProcessID;
        [FieldOffset(16)] public byte fast;
        [FieldOffset(17)] public byte slow;
        [FieldOffset(18)] public fixed byte tmp[6];
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct HV_HOOKING_HCALL_CONF_SET
    {
        [FieldOffset(0)] public uint hypercall;
        [FieldOffset(4)] public byte breakpoint;
        [FieldOffset(5)] public byte dbgPrint;
        [FieldOffset(8)] public uint log;
        [FieldOffset(12)] public uint bufferSize;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct HV_MUTATION_CONF
    {
        [FieldOffset(0)] public byte target;
        [FieldOffset(1)] public byte dbgMsg;
        [FieldOffset(4)] public uint type;
        [FieldOffset(8)] public uint seed;
        [FieldOffset(12)] public uint minChanges;
        [FieldOffset(16)] public uint maxChanges;
        [FieldOffset(20)] public uint maxLength;
        [FieldOffset(24)] public uint count;
    }

    [StructLayout(LayoutKind.Explicit)]
    unsafe public struct GUID
    {
        [FieldOffset(0)] public fixed byte binary[16];

        public byte[] getBytes()
        {
            byte[] res = new byte[16];
            for(int x=0; x< 16; x++)
                res[x] = binary[x];
            return res;
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    unsafe public struct VMBUS_CHANNEL
    {
        [FieldOffset(0)] public ulong addr;
        [FieldOffset(0x8)] public GUID InterfaceType;
        [FieldOffset(0x18)] public GUID InterfaceInstance;
        [FieldOffset(0x28)] public uint maxNrOfPackets;
        [FieldOffset(0x2C)] public uint maxPacketSize;
        [FieldOffset(0x30)] public uint maxExternalDataSize;
        [FieldOffset(0x34)] public uint maxNrOfMDLs;
        [FieldOffset(0x38)] public uint clientContextSize;
        [FieldOffset(0x3C)] public byte isPipe;
        [FieldOffset(0x3E)] public ushort vmID;
        [FieldOffset(0x40)] public long vmBusHandle;
        [FieldOffset(0x48)] public uint nrOfPagesToAllocateInIncomingRingBuffer;
        [FieldOffset(0x4C)] public uint nrOfPagesToAllocateInOutgoingRingBuffer;
        [FieldOffset(0x50)] public byte vtlLevel;
        [FieldOffset(0x51)] public fixed byte name[128];
        [FieldOffset(0xD1)] public fixed byte tmp[7];
        public byte[] getNameBytes()
        {
            int length;
            for (length = 0; length < 120; length++)
                if (name[length] == 0x00 && name[length+1] == 0x00)
                    break;
            byte[] res = new byte[length];
            for (int x = 0; x < length; x++)
                res[x] = name[x];
            return res;
        }
    }

    class DriverIO
    {

        [DllImport("HyperViperDll.dll", EntryPoint = "init", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        static extern bool init();
        [DllImport("HyperViperDll.dll", EntryPoint = "hypercallsCall", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        static unsafe extern bool hypercallsCall(long* callInfo, byte* inBuffer, uint inBufferSize, long* callResult, byte* outBuffer, uint outBufferSize);
        [DllImport("HyperViperDll.dll", EntryPoint = "hypercallsHook", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        static extern bool hypercallsHook();
        [DllImport("HyperViperDll.dll", EntryPoint = "hypercallsUnhook", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        static extern bool hypercallsUnhook();
        [DllImport("HyperViperDll.dll", EntryPoint = "hypercallsStartLogging", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        static extern bool hypercallsStartLogging(byte[] fname);
        [DllImport("HyperViperDll.dll", EntryPoint = "hypercallsStopLogging", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        static extern bool hypercallsStopLogging();
        [DllImport("HyperViperDll.dll", EntryPoint = "hypercallsGetStats", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        static unsafe extern bool hypercallsGetStats(HV_HOOKING_HCALL_STATS* stats);
        [DllImport("HyperViperDll.dll", EntryPoint = "hypercallsSetConf", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        static unsafe extern bool hypercallsSetConf(HV_HOOKING_HCALL_CONF_SET* stats);
        [DllImport("HyperViperDll.dll", EntryPoint = "hypercallsClearStats", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        static extern bool hypercallsClearStats();
        [DllImport("HyperViperDll.dll", EntryPoint = "hypercallsFuzz", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        static unsafe extern bool hypercallFuzzDll(long* callInfo, byte* buffer, uint len, HV_MUTATION_CONF* conf);


        static public bool initialize()
        {
            return init();
        }

        static public bool hypercall(long callInfo, byte[] inBuffer, uint inBufferSize, out long callResult, out byte[] outBuffer, uint outBufferSize)
        {
            HV_HOOKING_HCALL_STATS[] val = new HV_HOOKING_HCALL_STATS[0xE8 + 1];
            bool success;
            unsafe
            {
                fixed (byte* tmpIn = inBuffer)
                {   
                    fixed (byte* tmpOut = outBuffer)
                    {
                        long result;
                        long tmpCallCode = callInfo;
                        success = hypercallsCall(&tmpCallCode, tmpIn, inBufferSize, &result, tmpOut, outBufferSize);
                        callResult = result;
                    }
                }
            }
            return success;
        }

        static public bool hook()
        {
            return hypercallsHook();
        }

        static public bool unhook()
        {
            return hypercallsUnhook();
        }

        static public bool startLogging(String filename)
        {
            Encoding enc = Encoding.GetEncoding(437); // 437 is the original IBM PC code page
            byte[] bytes = enc.GetBytes(filename);
            return hypercallsStartLogging(bytes);
        }

        static public bool stopLogging()
        {
            return hypercallsStopLogging();
        }

        static public HV_HOOKING_HCALL_STATS[] getStats()
        {
            HV_HOOKING_HCALL_STATS[] val = new HV_HOOKING_HCALL_STATS[0xE8+1];
            bool success;
            unsafe
            {
                fixed (HV_HOOKING_HCALL_STATS* tmp = val)
                {
                    success = hypercallsGetStats(tmp);
                }
            }
            if (success)
                return val;
            return null;
        }

        static public bool setConf(HV_HOOKING_HCALL_CONF_SET conf)
        {
            bool success;
            unsafe
            {
                success = hypercallsSetConf(&conf);
            }
            return success;
        }

        static public bool clearStats()
        {
            return hypercallsClearStats();
        }

        static public bool hypercallFuzz(long callInfo, byte[] buffer, uint len, HV_MUTATION_CONF conf)
        {
            bool success;
            unsafe
            {
                fixed (byte* bufferTmp = buffer)
                {
                    long hypercall = callInfo;
                    HV_MUTATION_CONF confTmp = conf;
                    success = hypercallFuzzDll(&hypercall, bufferTmp, len, &confTmp );
                }
            }
            return success;
        }
    }
}
