using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopPopcat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread t;
        bool sound = true;

        public MainWindow()
        {
            InitializeComponent();
            this.Topmost = true;

            t = new Thread(new ThreadStart(Play));
            t.Start();

            MouseDown += MainWindow_MouseDown;

            var menu = new System.Windows.Forms.ContextMenu();

            var noti = new NotifyIcon {
                Icon = new Icon("./icon.ico"),
                Visible = true,
                Text = "Desktop Popcat",
                ContextMenu = menu,
            };

            var bgm = new System.Windows.Forms.MenuItem
            {
                Index = 0,
                Text = "Play Sound",
            };

            bgm.Checked = true;

            bgm.Click += (object o, EventArgs e) =>
            {
                bgm.Checked = !bgm.Checked;
                if (bgm.Checked == true)
                    sound = true;
                else sound = false;
            };

            var alwaystop = new System.Windows.Forms.MenuItem
            {
                Index = 1,
                Text = "Always on Top"
            };

            alwaystop.Checked = true;

            alwaystop.Click += (object o, EventArgs e) =>
            {
                alwaystop.Checked = !alwaystop.Checked;
                if (alwaystop.Checked == true)
                    this.Topmost = true;
                else this.Topmost = false;
            };

            var about = new System.Windows.Forms.MenuItem
            {
                Index = 2,
                Text = "About"
            };

            about.Click += (object o, EventArgs e) =>
            {
                AboutWindow aboutForm = new AboutWindow();
                aboutForm.Show();
            };

            var shutdown = new System.Windows.Forms.MenuItem
            {
                Index = 3,
                Text = "Exit",
            };

            shutdown.Click += (object o, EventArgs e) =>
            {
                System.Windows.Application.Current.Shutdown();
            };

            menu.MenuItems.Add(bgm);
            menu.MenuItems.Add(alwaystop);
            menu.MenuItems.Add(about);
            menu.MenuItems.Add(shutdown);
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void Play()
        {
            while (true)
            {
                if (sound == true)
                {
                    MediaPlayer mediaPlayer = new MediaPlayer();
                    mediaPlayer.Open(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "sound.wav")));
                    mediaPlayer.Play();
                    Thread.Sleep(500);
                }
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            t.Abort();
        }
    }
}
