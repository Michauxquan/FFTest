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

namespace FFmpeg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
         private string url = "D:/MichauxPro/";
       // private string url = "G:/MichauxPro/";
        private void btn1_Click(object sender, EventArgs e)
        { 
            folder1.Description = "请选择文件夹";
            if (folder1.ShowDialog() == DialogResult.OK)
            {
                this.textpath.Text = folder1.SelectedPath;
            } 
        }
        public string FFmpegPath { get; set; }
        private void btn2_Click(object sender, EventArgs e)
        {
            string para = "";
            if (string.IsNullOrEmpty(this.textpath.Text))
            {
                MessageBox.Show("请选择文件");
                return;
            }
            FFmpegPath = "C:/ffmpeg/bin/ffmpeg.exe";
            para =
                "-i " + url + "FFTest/rain.mp4 -i " + url + "/FFTest/egg.png -filter_complex \"overlay=10:10\" -b 1024k -acodec copy "+url+"FFTest/aaa.mp4";
            this.text2.Text= this.text2.Text+RunProcess(para);
        }


        //添加背景音乐 -i input_video.mp4 -i looped_audio.mp3 -shortest output_video.mp4

    
        /// <summary>
        /// 调用ffmpeg.exe 执行命令
        /// </summary>
        /// <param name="Parameters">命令参数</param>
        /// <returns>返回执行结果</returns>
        private string RunProcess(string Parameters)
        {
            //创建一个ProcessStartInfo对象 并设置相关属性
            //var oInfo = new ProcessStartInfo(FFmpegPath, Parameters);

            ProcessStartInfo oInfo = new System.Diagnostics.ProcessStartInfo(FFmpegPath);
            oInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            oInfo.Arguments = Parameters;

            oInfo.UseShellExecute = false;
            oInfo.CreateNoWindow = true;
            oInfo.RedirectStandardOutput = true;
            oInfo.RedirectStandardError = true;
            oInfo.RedirectStandardInput = true;
            //创建一个字符串和StreamReader 用来获取处理结果
            string output = null; 
            StreamReader srOutput = null;
            try
            {
                //调用ffmpeg开始处理命令
                var proc = Process.Start(oInfo);
                //proc.WaitForExit();
                //获取输出流
                srOutput = proc.StandardError;
                //转换成string
                output = srOutput.ReadToEnd();
                //关闭处理程序
               // proc.Close();
            }
            catch (Exception ex)
            {
                output = ex.ToString();
            }
            finally
            {
                //释放资源
                if (srOutput != null)
                {
                    srOutput.Close();
                    srOutput.Dispose();
                }
            }
            return output;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            FFmpegPath = "C:/ffmpeg/bin/ffmpeg.exe";
            //非MP4 MKV字幕方法
            //ffplay -vf subtitles=subtitle.ass input.mp4
            //-i D:/Michaux/FFTest/aaa.mp4 -vf subtitles=rain.srt output.mp4
           // string para = "-i D:/Michaux/FFTest/aaa.mp4 -vf subtitles=rain.srt output.mp4";


            string para = "-i " + url + "FFTest/rain.mp4 -vf \"subtitles='D\\://MichauxPro//FFTest//rain.srt'\" output.mp4";
            Console.WriteLine(para);
           // string para = "-i D:/Michaux/FFTest/aaa.mp4 -f srt -i D:/Michaux/FFTest/rain.srt -c:v copy -c:a  -c:s mov_text copy zimu.mp4";

           // string para = "-i D:/Michaux/FFTest/aaa.mp4 -i D:/Michaux/FFTest/rain.srt -map 0:v -map 0:a -map 1:s -c:v copy -c:a copy -c:s mov_text -movflags +faststart zimu.mp4";


            // MP4 zimu ="-i  D:/Michaux/FFTest/aaa.mp4 -i D:/Michaux/FFTest/rain.srt -map 0:v -map 0:a -map 1:s -c:v copy -c:a copy -c:s mov_text -movflags +faststart zimu.mp4";

            this.text2.Text = this.text2.Text + RunProcess(para); 
        }
        /// <summary>
        /// 动态文字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn4_Click(object sender, EventArgs e)
        { 
            FFmpegPath = "C:/ffmpeg/bin/ffplay.exe";
            string para = " -i " + url + "FFTest/rain.mp4 -vf drawtext=\"fontfile=arial.ttf:text='Michaux Test':x=w-t*50:fontcolor=darkorange:fontsize=30\"  dongtaizimu.mp4";
            this.text2.Text = this.text2.Text + RunProcess(para); 
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            FFmpegPath = "C:/ffmpeg/bin/ffmpeg.exe";
            string para = " -y -i " + url + "FFTest/img.mp4  -r 15 -q:v 2 " + url + "FFTest/model/model1/image%d.jpg";
            this.text2.Text = this.text2.Text + RunProcess(para); 
        }

        private void btn6_Click(object sender, EventArgs e)
        { 
            FFmpegPath = "C:/ffmpeg/bin/ffmpeg.exe";
            string para = " -y -f image2 -i " + url + "FFTest/model/model1/image%d.jpg -vcodec libx264 -r 24  "+url+"FFTest/imgp4.mp4";
            this.text2.Text = this.text2.Text + RunProcess(para); 
        }

        private void btn7_Click(object sender, EventArgs e)
        {

          //  overlay=x=main_w-overlay_w-10:y=main_h-overlay_h-10 
            //-vf transpose=1
            FFmpegPath = "C:/ffmpeg/bin/ffmpeg.exe";
            // -i " + url + "FFTest/ali.jpg -filter_complex \"overlay=122:87\" -vf transpose=1  -vf scale=198:198:gamma=1   \"smptebars=size=121:86:rate=10\" 
            string para = " -y -i " + url + "FFTest/ali.jpg -vf \"rotate=-10*PI/180,scale=176:218\" " + url + "FFTest/ali1.png";
            ///./ffmpeg -i ~/fuck.mp4 -i ~/1.mp4 -shortest -filter_complex "[1:v]chromakey=0x70de77:0.1:0.2[ckout];[0:v][ckout]overlay[out]" -map "[out]" output.mp4
            this.text2.Text = this.text2.Text + RunProcess(para); 
            para = " -y -i " + url +"FFTest/ali1.png  -vf chromakey=0x000000 " +url + "FFTest/ali1.png ";

            this.text2.Text = this.text2.Text + RunProcess(para);
            para = " -y -i " + url + "FFTest/model/model1/image1.jpg -i " + url + "FFTest/ali1.png -filter_complex \"overlay=121:86\"  " + url + "FFTest/img1.jpg";
            this.text2.Text = this.text2.Text + RunProcess(para); 
        }
        
    }
}
