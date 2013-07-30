using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace elp.Extensions
{
    public static class Colors
    {
        private static Dictionary<int, SolidColorBrush> _colors;

        static Colors()
        {
            _colors = new Dictionary<int, SolidColorBrush>();
            _colors.Add(0, Brushes.Black);
            _colors.Add(1, Brushes.Red);
            _colors.Add(2, Brushes.Green);
            _colors.Add(3, Brushes.Blue);
            _colors.Add(4, Brushes.Violet);
            _colors.Add(5, Brushes.Brown);
            _colors.Add(6, Brushes.Aquamarine);
            _colors.Add(7,Brushes.Indigo);
            _colors.Add(8, Brushes.LightSalmon);
            _colors.Add(9, Brushes.Lavender);
            _colors.Add(10, Brushes.Gold);
            _colors.Add(11, Brushes.Gray);
        }
        public static SolidColorBrush getBrush(int index)
        {
            if (index < _colors.Count)
            {
                return _colors[index];
            }
            else
            {
                index = index % _colors.Count;
                return _colors[index];
            }
        }
    }
}
