using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace elp.Extensions
{
    public class CSVReader : IEnumerable
    {
        #region Поля
        private char _separator;
        private IList _dataList;
        private List<Column> _columnList;
        private List<string> _lineList;
        private List<CSVLine> _csvLineList;
        private string _filename;
        private int _firstLine;
        #endregion

        #region Конструкторы
        public CSVReader(char separator)
        {
            _lineList = new List<string>();
            _csvLineList = new List<CSVLine>();
            _columnList = new List<Column>();
            _separator = separator;
            _firstLine = 1;
        }

        private CSVReader()
            : this(';')
        { }

        public CSVReader(string filename, IList dataList, char separator)
            : this(separator)
        {
            this._filename = filename;
            this._dataList = dataList;
        }

        public CSVReader(string filename, IList dataList)
            : this(filename, dataList, ';')
        { }

        public CSVReader(char separator, string filename)
            : this(separator)
        {
            this._filename = filename;
            this.readCSV();
        }

        public CSVReader(string filename)
            : this(';', filename)
        { }
        #endregion

        #region Свойства
        public IList finalList
        {
            get
            {
                if (_dataList == null) throw new ElpNullSourceException("Отсутствует коллекция объектов", "dataList == null", DateTime.Now);
                this.readCSV();
                return _dataList;
            }
        }

        public bool hasHeader
        {
            get
            {
                if (_firstLine == 0)
                {
                    return false;
                }
                if (_firstLine == 1)
                {
                    return true;
                }
                throw new ElpInvalidHeaderException("Передано неверное значение индекса первой строки", "Индекс первой строки должен быть 0 или 1", DateTime.Now);
            }
            set
            {
                if (value == true) _firstLine = 1;
                else _firstLine = 0;
            }
        }

        public CSVLine this[int param]
        {
            get
            {
                if (_dataList != null) throw new ElpLineAccessException("Отказ доступа к строке", "Доступ к строке невозможен при установленном dataList", DateTime.Now);
                return this._csvLineList[param];
            }
        }

        public int count
        {
            get
            {
                return _csvLineList.Count;
            }
        }
        #endregion

        #region Методы
        #region Public
        public void AddColumn(string bind, int index)
        {
            Column newColumn = new Column(bind, index, Column.BindTypes.PropertyBind);
            newColumn.source = this._dataList;
            this._columnList.Add(newColumn);
        }

        public void AddColumnArray(string bind, int index, int arrayIndex)
        {
            Column newColumn = new Column(bind, index, arrayIndex, Column.BindTypes.ArrayBind);
            newColumn.source = this._dataList;
            this._columnList.Add(newColumn);
        }

        public void AddColumnMethod1(string bind, int index)
        {
            Column newColumn = new Column(bind, index, Column.BindTypes.Method1Bind);
            newColumn.source = this._dataList;
            this._columnList.Add(newColumn);
        }


        public IEnumerator GetEnumerator()
        {
            return this._lineList.GetEnumerator();
        }
        #endregion

        #region Private
        private void readCSV()
        {
            string csvLine;
            StreamReader file;
            try
            {
                file = new StreamReader(this._filename);
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex);
            }
            while ((csvLine = file.ReadLine()) != null)
            {
                _lineList.Add(csvLine);
            }
            foreach (string line in _lineList)
            {
                CSVLine newLine = new CSVLine(_separator, line);
                _csvLineList.Add(newLine);
            }
            if (_dataList != null)
            {
                this.goBind();
            }
        }

        private void goBind()
        {
            Type listType = _dataList.GetType();
            Type elementType = listType.GetGenericArguments()[0];
            for (int i = _firstLine; i < _csvLineList.Count; i++)
            {
                var element = Activator.CreateInstance(elementType);
                foreach (Column column in _columnList)
                {
                    string val = _csvLineList[i].value(column.index);

                    try
                    {
                        switch (column.bindType)
                        {
                            case Column.BindTypes.PropertyBind:
                                if (column.elementType != typeof(string))
                                {
                                    elementType.GetProperty(column.bind).SetValue(element, Convert.ChangeType(val, column.elementType), null);
                                }
                                else
                                {
                                    elementType.GetProperty(column.bind).SetValue(element, val, null);
                                }
                                break;
                            case Column.BindTypes.ArrayBind:
                                Array tempArray = (Array)elementType.GetProperty(column.bind).GetValue(element, new object[] {});
                                Type arrayType = tempArray.GetType();
                                Type arrayElementType = arrayType.GetElementType();
                                
                                if (column.elementType != typeof(string))
                                {
                                    arrayType.GetMethod("SetValue", new Type[] { arrayElementType, typeof(int) }).Invoke(tempArray, new object[] { Convert.ChangeType(val, arrayElementType), column.arrayIndex });                                    
                                    
                                }
                                else
                                {
                                    // TODO: Протестировать эту ветку
                                    arrayType.GetMethod("SetValue", new Type[] { arrayElementType, typeof(int) }).Invoke(tempArray, new object[] {val, column.arrayIndex });
                                }
                                break;
                            case Column.BindTypes.Method1Bind:
                                MethodInfo method = elementType.GetMethod(column.bind);
                                Type argType = method.GetParameters()[0].ParameterType;
                                if (argType != typeof(string))
                                {
                                    method.Invoke(element, new object[] { Convert.ChangeType(val, argType) });
                                }
                                else
                                {
                                    method.Invoke(element, new object[] { val });
                                }
                                break;
                        }
                    }
                    catch (Exception) { }

                }
                try
                {
                    _dataList.Add(element);
                }
                catch (Exception) { }
            }
        }
        #endregion
        #endregion

        #region Подклассы
        private class Column
        {
            private IList _parentList;            
            
            public Column(string bind, int index, BindTypes bindType)
            {
                this.bind = bind;
                this.index = index;
                this.bindType = bindType;
            }

            public Column(string bind, int index, int arrayIndex, BindTypes bindType)
            {
                this.bind = bind;
                this.index = index;
                this.arrayIndex = arrayIndex;
                this.bindType = bindType;
            }

            public string bind { get; private set; }
            public int index { get; private set; }
            public BindTypes bindType { get; private set; }
            public int arrayIndex { get; set; }

            public IList source
            {
                get
                {
                    return _parentList;
                }
                set
                {
                    _parentList = value;
                }
            }
            public Type elementType
            {
                get
                {
                    Type listType = _parentList.GetType();
                    Type elementType = listType.GetGenericArguments()[0];
                    Type propType = elementType.GetProperty(this.bind).PropertyType;
                    return propType;
                }
            }

            public enum BindTypes
            {
                PropertyBind,
                ArrayBind,
                //ArrayNestedProperty
                Method1Bind
            }

            
        }

        public class CSVLine
        {
            #region Поля
            private List<string> _line = new List<string>();
            private string _csvLine;
            private char _separator;
            #endregion

            #region Конструкторы
            public CSVLine(char separator, string csvLine)
            {
                this._separator = separator;
                this._csvLine = csvLine;
                this.parseLine();
            }
            #endregion

            #region Методы
            public string value(int param)
            {
                return this._line[param];
            }

            private void parseLine()
            {
                if (_csvLine.Length == 0)
                {
                    this.Add("");
                    return;
                }
                List<int> separatorPos = new List<int>();
                for (int i = 0; i < _csvLine.Length; i++)
                {
                    if (_csvLine[i] == _separator)
                    {
                        separatorPos.Add(i);
                    }
                }

                if (separatorPos.Count == 0) this.Add(_csvLine);
                else
                {
                    string first = _csvLine.Substring(0, separatorPos[0]);
                    this.Add(first);
                    for (int j = 1; j < separatorPos.Count; j++)
                    {
                        string val = _csvLine.Substring(separatorPos[j - 1] + 1, separatorPos[j] - separatorPos[j - 1] - 1);
                        this.Add(val);
                    }
                    string final = _csvLine.Substring(separatorPos[separatorPos.Count - 1] + 1, _csvLine.Length - separatorPos[separatorPos.Count - 1] - 1);
                    this.Add(final);
                }
            }

            public void Add(string newString)
            {
                this._line.Add(newString);
            }
            #endregion
        }
        #endregion
    }
}
