using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace elpExtsTests
{
    [TestClass]
    public class IniFiles
    {
        [TestMethod]
        [ExpectedException(typeof(elp.Extensions.IncorrectIniLineException))]
        public void iniTest()
        {
            string expectedAppName = "TradeStat";
            int expectedNum = 9;
            elp.Extensions.IniFile ini = new elp.Extensions.IniFile(@"Res\iniTest.ini");
            string appName = ini.GetSection("Main").GetParameter("AppName");
            int num = Convert.ToInt32(ini.GetSection("Main").GetParameter("int"));
            Assert.AreEqual(expectedAppName, appName);
            Assert.AreEqual(expectedNum, num);
        }

        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public void IniFileNotFoundTest()
        {
            elp.Extensions.IniFile ini = new elp.Extensions.IniFile(@"Res\no.ini");
        }
    }
}
