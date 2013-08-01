using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace elpExtsTests
{
    [TestClass]
    public class IniFiles
    {
        [TestMethod]
        public void TestMethod1()
        {
            string expectedAppName = "MetaStock Professional";
            elp.Extensions.IniFile ini = new elp.Extensions.IniFile("D:\\setup.ini");
            string appName = ini.GetSection("Startup").GetParameter("AppName");
            string update = ini.GetSection("ISUPDATE").GetParameter("UpdateURL");
            Assert.AreEqual(expectedAppName, appName);
            Assert.AreEqual("http://", update);
        }
    }
}
