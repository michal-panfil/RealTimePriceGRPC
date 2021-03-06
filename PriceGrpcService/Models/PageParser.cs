﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PriceGrpcService.Models
{
    public class PageParser : IDataParser
    {
        public async Task <List<InstrumentPrice>> ParseData(string rawPage)
        {
            if(String.IsNullOrEmpty(rawPage)) { return null; }
            List<InstrumentPrice> instruments = new List<InstrumentPrice>();
            var dictInstruments =  await Pars(rawPage);
            
            foreach (var item in dictInstruments )
            {
                var instrument = new InstrumentPrice(item.Key, item.Value);
                instruments.Add(instrument);
            }
            return instruments;
        }

        private async  Task<Dictionary<string,string>> Pars(string html)
        {
            var trGroup = Regex.Matches(html, @"<tbody\b[^>]*>(.*?)<\/tbody>");
            var tdGroup = trGroup.Count > 1 ? Regex.Matches(trGroup[0]?.ToString() + trGroup[1]?.ToString(), @"<tr\b[^>]*>(.*?)<\/tr>") : null;

            var result = new Dictionary<string, string>();
            if(tdGroup != null) {
                var task = Task.Run(() =>
                {
                foreach (var item in tdGroup)
                {
                    var x = (Regex.Matches(item.ToString(), @"<a\b[^>]*>(.*?)<\/a>"));
                    var key = x[0].Groups[1].Value;
                    var y = (Regex.Matches(item.ToString(), @"<td\b[^>]*>(.*?)<\/td>"));
                    var value = y[2].Groups[1].Value;

                    result.Add(key, value);
                }

                });
                await task;
            }
            return result;
        }
    }
}
