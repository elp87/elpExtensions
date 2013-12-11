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

        public void AddColumnProperty(string header, string bind)
        {
            _columnsList.Add(new Column(header, bind));
        }

        public void AddColumnMethod(string header, string bind, object[] args)
        {
            _columnsList.Add(new Column(header, bind, args));
        }

        public void AddColumnConst(string header, string value)
        {
            _columnsList.Add(new Column(header, value, Column.BindType.ConstBind));
        }



        public void SetBindProperty(string header, string bind)
        {
            Column selectColumn = this._columnsList.First(column => column.header == header);
            selectColumn.bind = bind;
            selectColumn.bindType = Column.BindType.PropertyBind;

        }

        public void SetBindProperty(int index, string bind)
        {
            Column selectColumn = this._columnsList[index];
            selectColumn.bind = bind;
            selectColumn.bindType = Column.BindType.PropertyBind;
        }

        public void SetBindMethod(string header, string bind, object[] args)
        {
            Column selectColumn = this._columnsList.First(column => column.header == header);
            selectColumn.bind = bind;
            selectColumn.methodArgs = args;
            selectColumn.bindType = Column.BindType.MethodBind;
        }

        public void SetBindMethod(int index, string bind, object[] args)
        {
            Column selectColumn = this._columnsList[index];
            selectColumn.bind = bind;
            selectColumn.methodArgs = args;
            selectColumn.bindType = Column.BindType.MethodBind;
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
                string cellValue = "";
                switch (column.bindType)
                {
                    case Column.BindType.PropertyBind:
                        cellValue = type.GetProperty(column.bind).GetValue(obj, null).ToString();
                        break;
                    case Column.BindType.MethodBind:
                        cellValue = type.GetMethod(column.bind).Invoke(obj, column.methodArgs).ToString();
                        break;
                    case Column.BindType.ConstBind:
                        cellValue = column.bind;
                        break;
                    default:
                        throw new UnsetBindTypeException();
                }

                /*if (column.bindType == Column.BindType.propertyBind) { cellValue = type.GetProperty(column.bind).GetValue(obj, null).ToString(); }
                else if (column.bindType == Column.BindType.methodBind) { cellValue = type.GetMethod(column.bind).Invoke(obj, column.methodArgs).ToString(); }
                else throw new UnsetBindTypeException();*/
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
                this.bindType = BindType.PropertyBind;
            }

            public Column(string header, string bind, Object[] args)
            {
                this.header = header;
                this.bind = bind;
                this.methodArgs = args;
                this.bindType = BindType.MethodBind;
            }

            public Column(string header, string bind, BindType bindType)
            {
                this.header = header;
                this.bind = bind;
                this.bindType = bindType;
            }

            public string header { get; set; }
            public string bind { get; set; }
            public object[] methodArgs { get; set; }
            public BindType bindType { get; set; }

            public enum BindType
            {
                PropertyBind,
                MethodBind,
                ConstBind
            }            
        }
        #endregion
    }
}
