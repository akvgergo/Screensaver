using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Screensaver {
    public class BitMatrix {
        BitArray fields;

        public int Width { get; }
        public int Height { get; }

        public bool this[int x, int y] {
            get { return fields[Width * y + x]; }
            set { fields[Width * y + x] = value; }
        }

        public BitMatrix(int width, int height) {
            Width = width;
            Height = height;
            fields = new BitArray(width * height);

        }
    }
}
