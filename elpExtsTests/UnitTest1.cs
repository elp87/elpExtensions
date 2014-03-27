using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using elp87.Helpers;

namespace elpExtsTests
{
    [TestClass]
    public class IniFiles
    {
        [TestMethod]
        [ExpectedException(typeof(IncorrectIniLineException))]
        public void iniTest()
        {
            string expectedAppName = "TradeStat";
            int expectedNum = 9;
            IniFile ini = new IniFile(@"Res\iniTest.ini");
            string appName = ini.GetSection("Main").GetParameter("AppName");
            int num = Convert.ToInt32(ini.GetSection("Main").GetParameter("int"));
            Assert.AreEqual(expectedAppName, appName);
            Assert.AreEqual(expectedNum, num);
        }

        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public void IniFileNotFoundTest()
        {
            IniFile ini = new IniFile(@"Res\no.ini");
        }
    }
}
