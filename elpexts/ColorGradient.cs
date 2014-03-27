using System;
using System.Windows.Media;

namespace elp87.Helpers.Colors
{
    public static class ColorGradient
    {
        public static SolidColorBrush FromRedToGreen(double min, double max, double value)
        {
            Color color = new Color();

            double percentageValue = ((value - min) * 100) / (max - min);
            if (value >= min && value <= max)
            {                
                if (percentageValue > 50)
                {
                    return FromYellowToGreen(50, 100, percentageValue);
                }
                else
                {
                    return FromRedToYellow(0, 50, percentageValue);
                }
                
            }
            return Brushes.Transparent;
        }

        public static SolidColorBrush FromRedToYellow(double min, double max, double value)
        {
            Color color = new Color();

            double percentageValue = ((value - min) * 100) / (max - min);

            if (value >= min && value <= max)
            {
                color.A = 255;
                color.R = 255;
                color.B = 0;

                double g = 2.55 * percentageValue;
                color.G = (byte)Math.Round(g);

                SolidColorBrush brush = new SolidColorBrush(color);
                return brush;

            }
            return Brushes.Transparent;
        }

        public static SolidColorBrush FromYellowToGreen(double min, double max, double value)
        {
            Color color = new Color();

            double percentageValue = ((value - min) * 100) / (max - min);

            if (value >= min && value <= max)
            {
                color.A = 255;
                color.G = 255;
                color.B = 0;

                double r = 255 - (2.55 * percentageValue);
                color.R = (byte)Math.Round(r);

                SolidColorBrush brush = new SolidColorBrush(color);
                return brush;
            }
            return Brushes.Transparent;
        }
    }
}
