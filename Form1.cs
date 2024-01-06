using System;
using System.IO;
using YoutubeDLSharp;
using YoutubeDLSharp.Metadata;
using YoutubeDLSharp.Options;

namespace Youtube_Download
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            TryDownload();
        }
        async void TryDownload()
        {
            pictureBox1.Show();
            int downloadable = 1;
            var ytdl = new YoutubeDL();
            if (File.Exists("yt-dlp.exe") && File.Exists("ffmpeg.exe"))
            {
                ytdl.YoutubeDLPath = $"";
                ytdl.FFmpegPath = $"";
            }
            else
            {
                await YoutubeDLSharp.Utils.DownloadYtDlp();
                await YoutubeDLSharp.Utils.DownloadFFmpeg();
            }
            if (textBox2.Enabled)
            {
                if (Directory.Exists(textBox2.Text))
                {
                    downloadable = 1;
                }
                else
                {
                    downloadable = 0;
                }
            }
            if (downloadable == 1)
            {
                Download();
            }
            if (downloadable == 0)
            {
                MessageBox.Show("Неверный путь к файлу или неверная ссылка");
            }
        }
        int checker;
        int checker2;
        int checker3;
        async void Download()
        {
            var options = new OptionSet()
            {
                Format = "best",
            };

            if (checker3 % 2 == 0)
            {
                options.Format = "best";
                options.AudioQuality = 0;
            }
            else
            {
                options.Format = "worst";
                options.AudioQuality = 7;
            }
            
            var ytdl = new YoutubeDL();
            ytdl.OutputFolder = $"{textBox2.Text}";
            if (checker2 % 2 == 0)
            {
                options.RecodeVideo = VideoRecodeFormat.Mp4;
                var res = await ytdl.RunVideoDownload(textBox1.Text, overrideOptions: options);
            }
            else
            {
                options.AudioFormat = AudioConversionFormat.Mp3;
                var res = await ytdl.RunAudioDownload(textBox1.Text, overrideOptions: options);
            }
            
            pictureBox1.Hide();
            if (checker % 2 == 0)
            {
                MessageBox.Show("Загрузка завершена, файл находится там где приложение.");
            }
            else
            {
                MessageBox.Show($"Загрузка завершена в папку {textBox2.Text}");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checker % 2 == 0)
            {
                textBox2.Enabled = true;
                textBox2.Text = "C:\\";
            }
            else
            { 
            textBox2.Enabled = false;
            textBox2.Text = "";
            }
            checker++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = Clipboard.GetText();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checker2++;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checker3++;
        }
    }
}