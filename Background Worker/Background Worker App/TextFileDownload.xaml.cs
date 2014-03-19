using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Xml.Linq;
using System.IO;

namespace Background_Worker_App
{
    public partial class TextFileDownload : PhoneApplicationPage
    {

        BackgroundWorker bw = new BackgroundWorker();
        String[] url = new String[5];
        int x;

        public TextFileDownload()
        {
            InitializeComponent();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }

        private void Button_Click_Text(object sender, RoutedEventArgs e)
        {
            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Busy");
            }
            url[0] = Text1.Text;
            url[1] = Text2.Text;
            url[2] = Text3.Text;
            url[3] = Text4.Text;
            url[4] = Text5.Text;
        }

        private void Button_Click_Text_Cancel(object sender, RoutedEventArgs e)
        {
            if (bw.WorkerSupportsCancellation == true)
            {
                bw.CancelAsync();
            }
            NavigationService.GoBack();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for (int y = 0; y < 5; y++)
            {
                WebClient client = new WebClient();
                client.OpenReadCompleted += client_OpenReadCompleted;
                client.OpenReadAsync(new Uri(url[y]));
            }
            for (int z = 0; z < 5; z++)
            {
                for (int i = 1; i <= 10; i++)
                {
                    if ((worker.CancellationPending == true))
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    {
                        // Perform a time consuming operation and report progress.
                        System.Threading.Thread.Sleep(200);
                        worker.ReportProgress(i * 10);
                    }
                }
            }
        }

        //File Download
        async void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            byte[] buffer = new byte[e.Result.Length];
            Uri uri = new Uri(url[x]);
            string filename = System.IO.Path.GetFileName(uri.LocalPath);
            await e.Result.ReadAsync(buffer, 0, buffer.Length);

            x++;
            using (IsolatedStorageFile storageFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                //Check if file already exist
                if (!storageFile.FileExists(filename))
                {
                    using (IsolatedStorageFileStream stream = storageFile.OpenFile(filename, FileMode.Create))
                    {
                        await stream.WriteAsync(buffer, 0, buffer.Length);
                    }
                }
                else
                {
                    //storageFile.DeleteFile(filename);
                    //this.tbProgress.Text = ("Error: " + e.Error.Message);
                }
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                this.tbProgress.Text = "Canceled";
            }

            else if (!(e.Error == null))
            {
                this.tbProgress.Text = ("Error: " + e.Error.Message);
            }

            else
            {
                this.tbProgress.Text = "All Files Download Complete";
            }
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.tbProgress.Text = (e.ProgressPercentage.ToString() + "%");
            if (e.ProgressPercentage == 100)
            {
                this.tbProgress.Text = "Text File Download Complete";
            }
        }
    }
}