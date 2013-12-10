using elp.Extensions;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace elpExtsTests
{
    [TestClass]
    public class CSVWriterTest
    {
        class MyClass
        {
            public MyClass(int a)
            {
                myProperty = a;
            }
            public int myProperty { get; set; }
            public int myMethod(int b)
            {
                return (myProperty + b);
            }
        }
        [TestMethod]
        public void TestCSVWriter()
        {
            List<MyClass> l = new List<MyClass>();
            l.Add(new MyClass(1));
            l.Add(new MyClass(2));
            l.Add(new MyClass(3));
            l.Add(new MyClass(5));
            l.Add(new MyClass(7));

            CSVWriter csvw = new CSVWriter(l);
            csvw.AddColumnProperty("prop", "myProperty");
            csvw.AddColumnMethod("method", "myMethod", new object[] { 5 });
            csvw.AddColumnConst("const", "const value");
            try
            {
                csvw.SaveFile("test.csv");
            }
            catch (System.IO.IOException ex)
            {
            }
        }
    }
}
