using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
namespace XMusic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory + @"\Music");
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(Environment.CurrentDirectory + @"\Music"))
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\Music");
            timer1.Start();


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var file in di.GetFiles())
            {
                if (file.FullName.Contains(".mp3"))
                {
                    if (!listBox1.Items.Contains(Path.GetFileName(file.FullName)))
                    {
                        listBox1.Items.Add(Path.GetFileName(file.FullName));
                    }
                } else if (file.FullName.Contains(".wav"))
                {
                    if (!listbox1.Items.Contains(Path.GetFileName(file.FullName)))
                    {
                        listbox1.Items.Add(Path.GetFileName(file.FullName));
                    }               
                }              
            }
        }
        private WaveOutEvent outdevice;
        private AudioFileReader audiofile;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (outdevice == null)
                {
                    outdevice = new WaveOutEvent();
                    outdevice.PlaybackStopped += OnPlaybackStopped;
                }
                if (audiofile == null)
                {
                    audiofile = new AudioFileReader(Environment.CurrentDirectory + @"\Music\" + listBox1.SelectedItem);
                    outdevice.Init(audiofile);
                }
                outdevice.Play();
              
            }
            catch
            { }
        }
        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            outdevice.Dispose();
            outdevice = null;
            audiofile.Dispose();
            audiofile = null;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            outdevice?.Stop();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            try
            {
                outdevice.Volume = trackBar1.Value / 100f;
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            outdevice.Pause();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Environment.CurrentDirectory + @"\Music");
        }
    }
}

        
