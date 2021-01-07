using NUnit.Framework;
using PriceGrpcService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceTest.Models
{
    [TestFixture]
    public class PageDownloaderTest
    {
        [Test]
        public void DownloadPageTest()
        {
            var url = $"https://www.investing.com/commodities/real-time-futures";

            var result =  new PageDownloader().DownloadData(url).Result;

            Assert.IsNotNull(result);
        }
        [Test]
        public void DownloadPageWrongUrlTest()
        {
            var url = $"https://www.aefaefeafafegvfaeg.com";

            var result = new PageDownloader().DownloadData(url).Result;

            Assert.IsNull(result);
        }

    }
}
