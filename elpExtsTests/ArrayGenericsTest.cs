using elp87.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace elpExtsTests
{
    [TestClass]
    public class ArrayGenericsTest
    {
        [TestMethod]
        public void Concat_Simple()
        {
            int[] a = {0, 1, 2, 3};
            int[] b = {4, 5, 6, 7};

            int[] exp = {0, 1, 2, 3, 4, 5, 6, 7};

            int[] c = ArrayGeneric.Concat(a, b);

            CollectionAssert.AreEqual(exp, c);
        }

        [TestMethod]
        public void Concat_SameArray()
        {
            int[] a = { 0, 1, 2, 3 };
            int[] b = { 4, 5, 6, 7 };

            int[] exp = { 0, 1, 2, 3, 4, 5, 6, 7 };

            a = ArrayGeneric.Concat(a, b);

            CollectionAssert.AreEqual(exp, a);
        }

        [TestMethod]
        public void Concat_EmptyFirstArray()
        {
            int[] a = {};
            int[] b = {4, 5, 6, 7 };

            int[] exp = {4, 5, 6, 7 };

            int[] c = ArrayGeneric.Concat(a, b);

            CollectionAssert.AreEqual(exp, c);
        }

        [TestMethod]
        public void Concat_EmptyStart()
        {
            int[] c = {};
            int[] a = { 0, 1, 2, 3 };
            int[] b = { 4, 5, 6, 7 };

            int[] exp = { 0, 1, 2, 3, 4, 5, 6, 7 };

            c = ArrayGeneric.Concat(c, a);
            c = ArrayGeneric.Concat(c, b);

            CollectionAssert.AreEqual(exp, c);
        }
    }
}
