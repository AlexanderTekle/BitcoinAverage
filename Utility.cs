using Android.Net.Http;
using Android.Util;
using Java.Io;
using Org.Apache.Http.Client.Methods;
using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BitcoinAverage
{
	public static class Utility
    {
        public static string WebGet(string url)
        {
            var wc = new WebClient();
            return wc.DownloadString(url);
        }

		public static Task<string> WebGetAsync(string url)
		{
            var wc = new WebClient();
            return new Task<string>(() => wc.DownloadString(url));

            //var result = "";
            //var t = new Thread(new ThreadStart(() =>
            //{
            //    try
            //    {
            //        var wc = new WebClient();
            //        result = wc.DownloadString(url);
            //    }
            //    catch (Exception ex)
            //    {
            //        Log.E("Utility", "WebClient failed", ex);
            //    }
            //}));
            //t.Start();
            //t.Join();
            //return result;
		}

    }
}

