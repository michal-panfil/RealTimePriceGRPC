using NUnit.Framework;
using PriceGrpcService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ServiceTest.Models
{
    [TestFixture]
    public class PageParserTest
    {
        [Test]
        public void ParsHtmlTest()
        {
            string txt;

            using (StreamReader sr = new StreamReader(@"..\..\..\TestData\InstrumentPage.txt"))
            {
                txt = sr.ReadToEnd();
            }

            var result =  PageParser.GetData(txt).Result;

            Assert.IsTrue(result.Count == 14);
        }
        [Test]

        public void ParsHtmlNullTest()
        {
            var result = PageParser.GetData(null).Result;
            Assert.IsNull(result);
        }
        [Test]
        public void ParsHtmlWithNoPatternsTest()
        {
            string txt; 
            
            using (StreamReader sr = new StreamReader(@"..\..\..\TestData\NoPatternText.txt"))
            {
                txt = sr.ReadToEnd();
            }

            var result = PageParser.GetData(txt).Result;

            Assert.IsTrue(result.Count == 0);


        }
    }
}
