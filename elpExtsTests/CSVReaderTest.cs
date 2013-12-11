using elp.Extensions;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace elpExtsTests
{
    [TestClass]
    public class CSVReaderTest
    {
        private class TestClass
        {
            public int[] array {get;set;}

            public TestClass()
            {
                array = new int[5];
            }

            public TestClass(int[] array)
            {
                this.array = array;
            }
        }

        [TestMethod]
        public void TestArrays()
        {
            List<TestClass> list = new List<TestClass>();
            CSVReader csv = new CSVReader(@"Res\csvReader-Array.csv", list);
            csv.AddColumnArray("array", 0, 0);
            csv.AddColumnArray("array", 1, 1);
            csv.AddColumnArray("array", 2, 2);
            csv.AddColumnArray("array", 3, 3);
            csv.AddColumnArray("array", 4, 4);
            list = (List<TestClass>)csv.finalList;

            List<TestClass> expList = new List<TestClass>();
            expList.Add(new TestClass(new int[] { 0, 1, 2, 3, 4 }));
            expList.Add(new TestClass(new int[] { 10, 20, 30, 40, 50 }));
            expList.Add(new TestClass(new int[] { 51, 52, 53, 54, 55 }));

            Assert.AreEqual(expList.Count, list.Count);
            for (int i = 0; i < expList.Count; i++)
            {
                CollectionAssert.AreEqual(expList[i].array, list[i].array);
            }
        }
    }
}
