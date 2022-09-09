using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace DPRKMandatoryProgram
{
    public partial class Form1 : Form
    {

        string path;
        int screenshotCounter = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void startRecordingButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog.SelectedPath;
                button.Text = "Слежка начата.";
                screenshotTimer.Enabled = true;
            }
        }

        private void screenshotTimer_Tick(object sender, EventArgs e)
        {
            Bitmap bmpScreenshot;
            Graphics gfxScreenshot;
            int screenCount = 1;
            foreach (Screen screen in Screen.AllScreens)
            {
                bmpScreenshot = new Bitmap(screen.Bounds.Width, screen.Bounds.Height, PixelFormat.Format32bppArgb);
                gfxScreenshot = Graphics.FromImage(bmpScreenshot);
                gfxScreenshot.CopyFromScreen(screen.Bounds.X, screen.Bounds.Y, 0, 0, screen.Bounds.Size, CopyPixelOperation.SourceCopy);
                bmpScreenshot.Save($"{path}\\{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}-{screenCount}.png", ImageFormat.Png);
                screenCount++;
                screenshotCounter++;
            }

            label1.Text = $"Количество скриншотов: {screenshotCounter}";
        }
    }
}