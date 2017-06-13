using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Screensaver {
    public class SlowFiller {

        public DirectBitmap Image { get; }
        HashSet<Point> pixelsToFill = new HashSet<Point>();
        int argbFillcolor { get; set; }
        int argbStartColor { get; }
        public bool IsDone { get { return pixelsToFill.Count == 0; } }

        public SlowFiller(DirectBitmap image, Point start, Color fillColor) {
            Image = image;
            pixelsToFill.Add(start);
            argbFillcolor = fillColor.ToArgb();
            argbStartColor = image[start.X, start.Y];
        }
        
        public void AdvanceFill(int pixelCount) {
            for (int i = 0; i < pixelCount; i++) {
                HashSet<Point> nextRound = new HashSet<Point>();
                foreach (var p in pixelsToFill) {
                    Image[p.X, p.Y] = argbFillcolor;
                    if (p.Y - 1 >= 0 && Image[p.X, p.Y - 1] == argbStartColor) nextRound.Add(new Point(p.X, p.Y - 1));
                    if (p.Y + 1 < Image.Height && Image[p.X, p.Y + 1] == argbStartColor) nextRound.Add(new Point(p.X, p.Y + 1));
                    if (p.X - 1 >= 0 && Image[p.X - 1, p.Y] == argbStartColor) nextRound.Add(new Point(p.X - 1, p.Y));
                    if (p.X + 1 < Image.Width && Image[p.X + 1, p.Y] == argbStartColor) nextRound.Add(new Point(p.X + 1, p.Y));
                }
                pixelsToFill = nextRound;
            }
        }
    }
}
