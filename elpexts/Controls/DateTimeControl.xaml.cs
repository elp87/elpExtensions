using System;
using System.Windows.Controls;
using elp87.Helpers.Controls.InternalClasses;

namespace elp87.Helpers.Controls
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class DateTimeControl : UserControl
    {
        private int[] _hours;
        private int[] _minutes;
        private DateTimeControlContext _dtcContext;
        public DateTimeControl()
        {
            InitializeComponent();
            InitArrays();
            HourComboBox.ItemsSource = _hours;
            MinutesComboBox.ItemsSource = _minutes;

            _dtcContext = new DateTimeControlContext(DateTime.Now);
            DateCalendar.DataContext = _dtcContext;
            ResultTextBox.DataContext = _dtcContext;
            HourComboBox.DataContext = _dtcContext;
        }

        public DateTime Value => _dtcContext.Date;

        #region Methods
        private void InitArrays()
        {
            _hours = new int[24];
            for (int i = 0; i < 24; i++)
            {
                _hours[i] = i;
            }

            _minutes = new int[60];
            for (int i = 0; i < 60; i++)
            {
                _minutes[i] = i;
            }
        }
        #endregion

        private void HourComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResultTextBox.DataContext = null;
            ResultTextBox.DataContext = _dtcContext;
        }
    }
}
