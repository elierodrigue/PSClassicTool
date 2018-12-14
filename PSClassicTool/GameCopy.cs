using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
namespace PSClassicTool
{
    public partial class GameCopy : Form
    {
        private string _Source;
        private string _Destination;
        BackgroundWorker wrk = new BackgroundWorker();
        WebClient client = new WebClient();
        private bool downloadInProgress = false;
        public string currentFileName = "";
        public decimal currentFileSize = 0;
        public decimal currentFileProgress = 0;
        public GameCopy( string source,string destination)
        {
            InitializeComponent();
            _Source = source;
            _Destination = destination;
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
 
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            wrk.DoWork += Wrk_DoWork;
            wrk.WorkerReportsProgress = true;
  
            wrk.ProgressChanged += Wrk_ProgressChanged;
            wrk.RunWorkerCompleted += Wrk_RunWorkerCompleted;
            wrk.RunWorkerAsync();
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {

            downloadInProgress = false;
        }

        private void Wrk_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ProgressChangedEventHandler(Wrk_ProgressChanged),new object[] { null, null });
            }
            else
            {
                label1.Text = currentFileName + " (" + (int)(currentFileProgress / 1024 / 1024) + " mB / " + (int)(currentFileSize / 1024 / 1024)+ " mB)";
                progressBar1.Maximum = (int)currentFileSize / 1024;
                progressBar1.Value = (int)currentFileProgress / 1024;
            }
        }

        

        private void Wrk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            currentFileProgress = e.BytesReceived;
            currentFileSize = e.TotalBytesToReceive;
            //throw new NotImplementedException();
            Console.Write("");
        }

       
        private void CopyFolder(string sourcePath, string destPath)
        {
            if (!System.IO.Directory.Exists(destPath))
            {
                System.IO.Directory.CreateDirectory(destPath);
            }

            foreach (string file in System.IO.Directory.GetFiles(sourcePath))
            {
                downloadInProgress = true;
                currentFileName = System.IO.Path.GetFileName(file);
                client.DownloadFileAsync(new Uri(file), System.IO.Path.Combine(destPath, System.IO.Path.GetFileName(file)));
                while(downloadInProgress)
                {
                    System.Threading.Thread.Sleep(200);
                    wrk.ReportProgress(0, null);
                }
            
            }
            foreach (string directory in System.IO.Directory.GetDirectories(sourcePath))
            {
                string dirName = System.IO.Path.GetDirectoryName(directory);
                CopyFolder(directory, System.IO.Path.Combine(destPath, dirName));
            }

        }
        private void Wrk_DoWork(object sender, DoWorkEventArgs e)
        {
            CopyFolder(_Source, _Destination);
            
        }
    }
}
