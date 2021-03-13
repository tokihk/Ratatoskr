using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.General
{
    public class WebDownloader
    {
        private readonly object download_sync_ = new object();

        private Queue<string> download_url_ = new Queue<string>();
        private WebClient     download_wc_ = null;          
        private string        download_output_ = null;

        private bool   download_complete_ = false;
        private string download_result_ = null;


        public WebDownloader()
        {
        }

        public bool IsComplete
        {
            get { return (download_complete_); }
        }

        public string ResultString
        {
            get { return (download_result_); }
        }

        public void Clear()
        {
            download_url_ = new Queue<string>();

            if (download_wc_ != null) {
                download_wc_.CancelAsync();
                download_wc_ = null;
            }

            download_complete_ = false;
            download_result_ = null;
        }

        public void DownloadString(IEnumerable<string> url_list)
        {
            Clear();

            /* 対象URLをバックアップ */
            foreach (var url in url_list) {
                download_url_.Enqueue(url);
            }

            DownloadStringExec();
        }

        private void DownloadStringExec()
        {
            try {
                lock (download_sync_) {
                    if (download_url_.Count == 0)return;

                    var url = download_url_.Dequeue();

                    download_wc_ = new WebClient();
                    download_wc_.DownloadStringCompleted += OnDownloadStringCompleted;
                    download_wc_.DownloadStringAsync(new Uri(url));
                }
            } catch {
                DownloadStringExec();
            }
        }

        private void DownloadComplete(string result)
        {
            lock (download_sync_) {
                download_result_ = result;
                download_complete_ = true;
            }
        }

        private void OnDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (   (!e.Cancelled)
                && (e.Error == null)
            ) {
                DownloadComplete(e.Result);
            } else {
                DownloadStringExec();
            }
        }

        public void DownloadFile(IEnumerable<string> url_list, string output)
        {
            Clear();

            /* 対象URLをバックアップ */
            foreach (var url in url_list) {
                download_url_.Enqueue(url);
            }

            download_output_ = output;

            DownloadFileExec();
        }

        private void DownloadFileExec()
        {
            try {
                lock (download_sync_) {
                    if (download_url_.Count == 0)return;

                    var url = download_url_.Dequeue();

                    download_wc_ = new WebClient();
                    download_wc_.DownloadFileCompleted += OnDownloadFileCompleted;
                    download_wc_.DownloadFileAsync(new Uri(url), download_output_);
                }
            } catch {
                DownloadStringExec();
            }
        }

        private void DownloadFileComplete()
        {
            lock (download_sync_) {
                download_complete_ = true;
            }
        }

        private void OnDownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (   (!e.Cancelled)
                && (e.Error == null)
            ) {
                DownloadComplete(null);
            } else {
                DownloadStringExec();
            }
        }
    }
}
