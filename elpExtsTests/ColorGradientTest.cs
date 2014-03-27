using System;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using elp87.Helpers.Colors;

namespace elpExtsTests
{
    [TestClass]
    public class ColorGradientTest
    {
        [TestMethod]
        public void TestFromRedToYellow()
        {
            SolidColorBrush expRed = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            SolidColorBrush expYellow = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));

            Assert.AreEqual(expRed.Color, ColorGradient.FromRedToYellow(0, 100, 0).Color);
            Assert.AreEqual(expYellow.Color, ColorGradient.FromRedToYellow(0, 100, 100).Color);

            Assert.AreEqual(expRed.Color, ColorGradient.FromRedToYellow(0, 50, 0).Color);
            Assert.AreEqual(expYellow.Color, ColorGradient.FromRedToYellow(0, 50, 50).Color);
        }

        [TestMethod]
        public void TestFromYellowToGreen()
        {
            SolidColorBrush expYellow = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));
            SolidColorBrush expGreen = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));

            Assert.AreEqual(expYellow.Color, ColorGradient.FromYellowToGreen(0, 100, 0).Color);
            Assert.AreEqual(expGreen.Color, ColorGradient.FromYellowToGreen(0, 100, 100).Color);

            Assert.AreEqual(expYellow.Color, ColorGradient.FromYellowToGreen(0, 50, 0).Color);
            Assert.AreEqual(expGreen.Color, ColorGradient.FromYellowToGreen(0, 50, 50).Color);
        }

        [TestMethod]
        public void TestFromRedToGreen()
        {
            SolidColorBrush expRed = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            SolidColorBrush expYellow = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));
            SolidColorBrush expGreen = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));

            Assert.AreEqual(expRed.Color, ColorGradient.FromRedToGreen(0, 100, 0).Color);
            Assert.AreEqual(expYellow.Color, ColorGradient.FromRedToGreen(0, 100, 50).Color);
            Assert.AreEqual(expGreen.Color, ColorGradient.FromRedToGreen(0, 100, 100).Color);
        }
    }
}
