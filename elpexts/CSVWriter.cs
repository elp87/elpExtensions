using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace elp.Extensions
{
    public class CSVWriter
    {
        #region Поля
        private IList _source;
        private List<Column> _columnsList = new List<Column>();
        private char _separator;
        #endregion

        #region Конструкторы
        public CSVWriter()
            : this(';')
        {
        }

        public CSVWriter(IList source)
            : this(source, ';')
        {
        }

        public CSVWriter(char separator)
        {
            itemsSource = new List<object>();
            this._separator = separator;
        }

        public CSVWriter(IList source, char separator)
        {
            this.itemsSource = source;
            _separator = separator;
        }
        #endregion

        #region Свойства
        public IList itemsSource
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
            }
        }
        #endregion

        #region Методы
        public void AddColumn(string header)
        {
            _columnsList.Add(new Column(header));
        }

        public void AddColumn(string header, string bind)
        {
            _columnsList.Add(new Column(header, bind));
        }

        public void SetBind(string header, string bind)
        {
            Column selectColumn = this._columnsList.First(column => column.header == header);
            selectColumn.bind = bind;
        }

        public void SetBind(int index, string bind)
        {
            Column selectColumn = this._columnsList[index];
            selectColumn.bind = bind;
        }

        public void SaveFile(string fileName)
        {
            List<string> lineList = new List<string>();

            string header = this.GenHeaderLine();
            lineList.Add(header);
            foreach (var element in _source)
            {
                string line = this.GenLine(element);
                lineList.Add(line);
            }
            File.WriteAllLines(fileName, lineList, Encoding.UTF8);
        }

        private string GenLine(object obj)
        {
            Type type = obj.GetType();
            StringBuilder line = new StringBuilder();

            foreach (Column column in this._columnsList)
            {
                string cellValue = type.GetProperty(column.bind).GetValue(obj, null).ToString();
                line.Append(cellValue);
                line.Append(_separator);

            }
            return line.ToString();
        }

        private string GenHeaderLine()
        {
            StringBuilder headerLine = new StringBuilder();
            foreach (Column column in this._columnsList)
            {
                headerLine.Append(column.header);
                headerLine.Append(_separator);
            }
            return headerLine.ToString();
        }
        #endregion

        #region Подклассы
        private class Column
        {
            public Column() { }

            public Column(string header)
            {
                this.header = header;
            }

            public Column(string header, string bind)
            {
                this.header = header;
                this.bind = bind;
            }

            public string header { get; set; }
            public string bind { get; set; }
        }
        #endregion
    }
}
