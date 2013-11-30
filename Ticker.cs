using Org.Json;
using System;

namespace BitcoinAverage
{
    public class Ticker
    {
		public double avg { get; private set; }
		public double ask { get; private set; }
		public double bid { get; private set; }
		public double last { get; private set; }
		//public string timestamp { get; private set; }
		public double total_vol { get; private set; }

        public Ticker()
        {
            avg = ask = bid = last = total_vol = -1;
        }

        public Ticker(string json)
        {
            var tkr = new JSONObject(json);

            avg = GetDouble(tkr, "24h_avg");
            ask = GetDouble(tkr, "ask");
            bid = GetDouble(tkr, "bid");
            last = GetDouble(tkr, "last");
            total_vol = GetDouble(tkr, "total_vol");
        }

        static double GetDouble(JSONObject jsonObject, string name)
        {
            try
            {
                return jsonObject.GetDouble(name);
            }
            catch
            {
                return -1;
            }
        }
    }
}

