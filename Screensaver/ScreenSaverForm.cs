using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screensaver {
    public partial class ScreenSaverForm : Form {

        SlowFiller bgrdDrawer;
        Task AnimationTask;

        public ScreenSaverForm() {
            InitializeComponent();
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Bounds = Screen.PrimaryScreen.Bounds;
            Maze m = new Maze(Screen.PrimaryScreen.Bounds.Width / 10 - 2, Screen.PrimaryScreen.Bounds.Height / 10 - 2);
            m.CreatePassages(50);
            bgrdDrawer = new SlowFiller(m.Draw(), new Point(1000, 500), Color.DarkBlue);
            BackgroundImage = bgrdDrawer.Image.Bitmap;

            //ticker = new Timer();
            //ticker.Interval = 20;
            //ticker.Tick += (o, e) => { bgrdDrawer.AdvanceFill(2); Invalidate(); };
            //ticker.Enabled = true;
            AnimationTask = Animate();
        }

        async Task Animate() {
            while (!bgrdDrawer.IsDone) {
                bgrdDrawer.AdvanceFill(3);
                Invalidate();
                await Task.Delay(20);
            }
        }

        
    }
}
