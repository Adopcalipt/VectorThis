using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorThis.Classes
{
    public class Vector4
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float R { get; set; }

        public Vector4(float x, float y, float z, float r)
        {
            X = x; Y = y; Z = z; R = r;
        }
    }
}
