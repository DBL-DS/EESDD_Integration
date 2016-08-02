using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Module.UDP
{
    static class BytesConverter
    {
        public static float[] ToFloatArray(byte[] bytes)
        {
            int size = bytes.Length / 4;
            float[] array = new float[size];

            for (int i = 0; i < size; i++)
                array[i] = BitConverter.ToSingle(bytes, i * 4);

            return array;
        }

        public static string ToString(byte[] bytes)
        {
            return BitConverter.ToString(bytes);
        }

        public static T ConvertWith<T>(byte[] bytes,
            Func<byte[], T> convertFunc)
        {
            return convertFunc(bytes);
        }
    }
}
