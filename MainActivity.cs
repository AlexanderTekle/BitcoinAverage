using System;
using Android.App;
using Android.Os;
using Android.Widget;
using Dot42;
using Dot42.Manifest;
using Android.Util;
using Org.Json;
using Java.Util;
using System.Threading.Tasks;

[assembly: Application("Bitcoin Average")]
[assembly: UsesPermission(Android.Manifest.Permission.INTERNET)]

namespace BitcoinAverage._42
{
    [Activity]
    public class MainActivity : Activity
    {
        TextView txtLastPrice { get { return FindViewById<TextView>(R.Ids.txtLastPrice); } }
        TextView txtAsk { get { return FindViewById<TextView>(R.Ids.txtAsk); } }
        TextView txtBid { get { return FindViewById<TextView>(R.Ids.txtBid); } }
        TextView txtAvg { get { return FindViewById<TextView>(R.Ids.txtAvg); } }
        TextView txtVolume { get { return FindViewById<TextView>(R.Ids.txtVolume); } }

        Ticker lastTicker;
        Timer updateTimer;

        protected override void OnCreate(Bundle savedInstance)
        {
            base.OnCreate(savedInstance);
            SetContentView(R.Layouts.MainLayout);
        }

        protected override void OnResume()
        {
            base.OnResume();
            new Task(() => DoUpdateTicker()).Start();
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        void DoUpdateTicker()
        {
            try
            {
                var content = Utility.WebGet("https://api.bitcoinaverage.com/ticker/USD");
                var tkr = new Ticker(content);

                RunOnUiThread(() =>
                {
                    txtLastPrice.Text = tkr.last < 0 ? "N/A" : string.Format("${0}", tkr.last);
                    txtAsk.Text = tkr.ask < 0 ? "N/A" : string.Format("${0}", tkr.ask);
                    txtBid.Text = tkr.bid < 0 ? "N/A" : string.Format("${0}", tkr.bid);
                    txtAvg.Text = tkr.avg < 0 ? "N/A" : string.Format("${0}", tkr.avg);
                    txtVolume.Text = tkr.total_vol < 0 ? "N/A" : string.Format("฿{0}", tkr.total_vol);
                });

                lastTicker = tkr;
            }
            catch (Exception ex)
            {
                Log.E("Main", "DoUpdateTicker failed", ex);

                var toast = new Toast(this);
                toast.SetText("DoUpdateTicker failed");
                RunOnUiThread(() => toast.Show());
            }
        }
    }
}
