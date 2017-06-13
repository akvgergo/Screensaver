using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Collections;

namespace Screensaver {
    public class Labyrinth {

        WallSegment[,] Tiles;

        public WallSegment this[int x, int y] { get { return Tiles[x, y]; } }
        public Size Size { get; set; }

        public Labyrinth(Size size) : this(size.Width, size.Height) { }

        public Labyrinth(int width, int height, int seed = -1) {
            Size = new Size(width, height);
            Random rnd = new Random(seed < 0 ? Environment.TickCount : seed);
            Tiles = new WallSegment[width, height];

            BitArray filled = new BitArray(width * height);

            for (int y = 0; y < width; y++) {
                Tiles[y, 0] |= WallSegment.Up_Down;
                Tiles[y, height - 1] |= WallSegment.Up_Down;
            }

            for (int x = 0; x < height; x++) {
                Tiles[0, x] |= WallSegment.Right_Left;
                Tiles[width - 1, x] |= WallSegment.Right_Left;
            }


        }

        public void PlaceWall(int x, int y, WallSegment direction) {

        }

        public void DebugDraw() {
            Bitmap bmp = new Bitmap(Size.Width * 20, Size.Height * 20);
            Graphics g = Graphics.FromImage(bmp);

            Pen pen = new Pen(Color.Black, 2);

            for (int y = 0; y < Size.Width; y++) {
                for (int x = 0; x < Size.Height; x++) {
                    var tile = Tiles[y, x];

                    if ((tile & WallSegment.Up) > 0) g.DrawLine(pen, x * 20 + 10, y * 20 + 10, x * 20 + 10, y * 20);
                    if ((tile & WallSegment.Down) > 0) g.DrawLine(pen, x * 20 + 10, y * 20 + 10, x * 20 + 10, y * 20 + 20);
                    if ((tile & WallSegment.Left) > 0) g.DrawLine(pen, x * 20 + 10, y * 20 + 10, x * 20, y * 20 + 10);
                    if ((tile & WallSegment.Right) > 0) g.DrawLine(pen, x * 20 + 10, y * 20 + 10, x * 20 + 20, y * 20 + 10);
                }
            }
            g.Dispose();
            bmp.Save(@"D:\Documents\képek\Output\Labyrinth.bmp");
        }

        public enum WallSegment : byte {
            None = 0,
            Up = 1,
            Right = 2,
            Up_Right = 3,
            Down = 4,
            Up_Down = 5,
            Right_Down = 6,
            Up_Right_Down = 7,
            Left = 8,
            Up_Left = 9,
            Right_Left = 10,
            Up_Right_Left = 11,
            Down_Left = 12,
            Up_Down_Left = 13,
            Right_Down_Left = 14,
            All = 15
        }
    }

}
