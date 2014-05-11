using elp87.Helpers;
using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace elpExtsTests
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void TestLoggerConstructor()
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Logger log = new Logger(dir, "testLog");

            Assert.AreEqual(File.Exists(log.FilePath), true);
        }

        [TestMethod]
        public void TestWriteLine()
        {
            string[] lines = new string[] { "first line", "second line", "third line", "fourth line" };
            
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Logger log = new Logger(dir, "testLogWriteLine");
            foreach (string line in lines)
            {
                log.WriteLine(line);
            }

        }
    }
}
