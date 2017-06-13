using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Screensaver {

    public class Maze {

        BitMatrix passage;
        public int Width { get { return passage.Width; } }
        public int Height { get { return passage.Height; } }
        public int SegmentCount { get; }

        public bool this[int x, int y] {
            get { return passage[x, y]; }
        }

        public Maze(int width, int height, int seed = -1) {
            Random rnd = new Random(seed < 0 ? Environment.TickCount : seed);
            passage = new BitMatrix(width, height);
            SegmentCount = width * height;

            Stack<Point> maze = new Stack<Point>(Width * height / 4);
            maze.Push(new Point(0, 0));
            passage[0, 0] = true;

            //generation
            while (maze.Count > 0) {
                Point tile = maze.Peek();

                //Get all the walls around the current tile
                List<Point> nextTiles = new List<Point>(4);
                if (tile.Y - 1 >= 0 && !passage[tile.X, tile.Y - 1]) nextTiles.Add(new Point(tile.X, tile.Y - 1));
                if (tile.Y + 1 < Height && !passage[tile.X, tile.Y + 1]) nextTiles.Add(new Point(tile.X, tile.Y + 1));
                if (tile.X - 1 >= 0 && !passage[tile.X - 1, tile.Y]) nextTiles.Add(new Point(tile.X - 1, tile.Y));
                if (tile.X + 1 < Width && !passage[tile.X + 1, tile.Y]) nextTiles.Add(new Point(tile.X + 1, tile.Y));

                //for each wall we check if the wall has more than two side with a passage, if it does we remove it
                for (int i = nextTiles.Count - 1; i >= 0; i--) {
                    Point p = nextTiles[i];
                    int count = 0;
                    int diagonal = 0;
                    if (p.Y - 1 >= 0 && passage[p.X, p.Y - 1]) count++;
                    if (p.Y + 1 < Height && passage[p.X, p.Y + 1]) count++;
                    if (p.X - 1 >= 0 && passage[p.X - 1, p.Y]) count++;
                    if (p.X + 1 < Width && passage[p.X + 1, p.Y]) count++;

                    if (p.Y - 1 >= 0 && p.X + 1 < width && passage[p.X + 1, p.Y - 1]) diagonal++;
                    if (p.Y + 1 < Height && p.X + 1 < Width && passage[p.X + 1, p.Y + 1]) diagonal++;
                    if (p.Y - 1 >= 0 && p.X - 1 >= 0 && passage[p.X - 1, p.Y - 1]) diagonal++;
                    if (p.Y + 1 < Height && p.X - 1 >= 0 && passage[p.X - 1, p.Y + 1]) diagonal++;

                    if (count > 1 || diagonal > 1) nextTiles.RemoveAt(i);
                }

                if (nextTiles.Count == 0) {
                    maze.Pop();
                    continue;
                }

                var nextTile = nextTiles[rnd.Next(nextTiles.Count)];
                maze.Push(nextTile);
                passage[nextTile.X, nextTile.Y] = true;
            }

        }

        public void CreatePassages(int count) {
            Random rnd = new Random();
            while (count > 0) {
                var p = new Point(rnd.Next(Width), rnd.Next(Height));
                if (!passage[p.X, p.Y]) {
                    passage[p.X, p.Y] = true;
                    count--;
                }
            }
        }

        public DirectBitmap Draw() {
            DirectBitmap bmp = new DirectBitmap(Width * 10 + 20, Height * 10 + 20);
            Graphics g = Graphics.FromImage(bmp.Bitmap);
            g.Clear(Color.Black);
            var b = new SolidBrush(Color.FromArgb(0, 0, 1));
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    if (passage[x, y]) g.FillRectangle(b, new Rectangle(x * 10 + 10, y * 10 + 10, 10, 10));
                }
            }

            g.Dispose();
            return bmp;
        }

        List<Point> GetNeighbourWalls(Point p) {
            List<Point> neighbours = new List<Point>(4);
            if (p.Y - 1 >= 0 && !passage[p.X, p.Y - 1]) neighbours.Add(new Point(p.X, p.Y - 1));
            if (p.Y + 1 < Height && !passage[p.X, p.Y + 1]) neighbours.Add(new Point(p.X, p.Y + 1));
            if (p.X - 1 >= 0 && !passage[p.X - 1, p.Y]) neighbours.Add(new Point(p.X - 1, p.Y));
            if (p.X + 1 < Width && !passage[p.X + 1, p.Y]) neighbours.Add(new Point(p.X + 1, p.Y));
            return neighbours;
        }
    }


}
