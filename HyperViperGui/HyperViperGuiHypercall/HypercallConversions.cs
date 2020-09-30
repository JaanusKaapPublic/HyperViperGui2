using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperViperGuiHypercall
{
    class HypercallConversions
    {
        static public uint getCallCode(UInt64 input)
        {
            return ((uint)input & 0xFFFF);
        }
        static public bool isFast(UInt64 input)
        {
            return ((input & 0x10000) > 0);
        }
        static public uint getCountOfElements(UInt64 input)
        {
            return ((uint)(input >> 32)) & 0xFFF;
        }
        static public uint getRepStartIndex(UInt64 input)
        {
            return ((uint)(input >> 48)) & 0xFFF;
        }

        static public long hypercallInput(uint code, bool fast, uint count, uint start)
        {
            return ((long)code & 0xFFFF) | (fast ? (long)0x10000 : 0) | (((long)count & 0xFFF) << 32) | (((long)start & 0xFFF) << 48);
        }
    }
}
