using Org.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinAverage
{
    public class Exchange
    {
        public string name { get; private set; }
        public double ask { get; private set; }
        public double bid { get; private set; }
        public double last { get; private set; }
        public string source { get; private set; }
        public double volume_btc { get; private set; }
        public double volume_percent { get; private set; }

        public static IList<Exchange> ReadJson(string json)
        {
            var result = new List<Exchange>();

            var blob = new JSONObject(json);
            var keys = blob.Names();

            for (int i = 0; i < keys.Length(); ++i)
            {
                var name = keys.GetString(i);
                if (name == "timestamp")
                {
                    continue;
                }

                var xo = blob.GetJSONObject(name);
                try 
                {
                    var exchange = new Exchange();
                    exchange.name = name;
                    exchange.source = xo.GetString("source");
                    exchange.volume_btc = xo.GetDouble("volume_btc");
                    exchange.volume_percent = xo.GetDouble("volume_percent");

                    var rates = xo.GetJSONObject("rates");
                    exchange.ask = rates.GetDouble("ask");
                    exchange.bid = rates.GetDouble("bid");
                    exchange.last = rates.GetDouble("last");

                    result.Add(exchange);
                }
                catch
                {
                }
            }

            return result;
        }
    }
}
