using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperViperGuiHypercall
{
    public class HVCL
    {
        public struct HypercallStruct
        {
            public ulong code;
            public byte[] input;
        }

        static public List<HypercallStruct> open(String fname)
        {
            List<HypercallStruct> result = new List<HypercallStruct>();
            using (BinaryReader reader = new BinaryReader(File.Open(fname, FileMode.Open)))
            {
                if (reader.ReadInt32() != 0x4c435648)
                    return null;

                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    HypercallStruct res = new HypercallStruct();
                    res.code = (ulong)reader.ReadInt64();
                    if (HypercallConversions.isFast(res.code))
                    {
                        res.input = reader.ReadBytes(16);
                    }
                    else
                    {
                        int tmp = reader.ReadInt32();
                        if (tmp == 0)
                            res.input = new byte[0];
                        else
                            res.input = reader.ReadBytes(tmp);
                    }
                    result.Add(res);
                }
            }
            return result;
        }

        static public void save(String fname, ulong hypercallCodeInput, byte[] input)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(fname, FileMode.Create)))
            {
                writer.Write(0x4c435648);
                writer.Write(hypercallCodeInput);
                if(!HypercallConversions.isFast(hypercallCodeInput))
                    writer.Write(input.Length);
                writer.Write(input);
                writer.Close();
            }
        }
    }
}
