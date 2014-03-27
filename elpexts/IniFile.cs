using System;
using System.Collections.Generic;

namespace elp87.Helpers
{
    public class IniFile
    {
        #region Fields
        private string _filename;
        private string _curSection;
        private Dictionary<string, Section> sections;
        #endregion

        #region Constructors
        private IniFile()
        {
            sections = new Dictionary<string, Section>();
        }

        public IniFile(string filename)
            : this()
        {
            this._filename = filename;
            this.read();
        }
        #endregion

        #region Methods
        #region Public
        public Section GetSection(string key)
        {
            return this.sections[key];
        }
        #endregion

        #region Private
        private void read()
        {
            string line;
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(this._filename);
                while ((line = file.ReadLine()) != null)
                {
                    bool correctLine = false;
                    if (line.Contains("["))
                    {
                        int start = line.IndexOf('[');
                        int end = line.IndexOf(']');
                        string sectionName = line.Substring(start + 1, end - start - 1);
                        this.sections.Add(sectionName, new Section());
                        this._curSection = sectionName;
                        correctLine = true;
                    }
                    if (line.Contains("="))
                    {
                        int equalsIndex = line.IndexOf('=');
                        string key = line.Substring(0, equalsIndex);
                        string value = line.Substring(equalsIndex + 1);
                        this.sections[_curSection].Add(key, value);
                        correctLine = true;
                    }
                    if (line == "")
                    {
                        correctLine = true;
                    }
                    if (correctLine == false) throw new IncorrectIniLineException(line + " - не может быть прочитана", "Неверный формат строки", DateTime.Now);

                }
            }
            catch (System.IO.FileNotFoundException ex)
            {
                throw new System.IO.FileNotFoundException("Файл не найден", this._filename, ex);
            }
        }
        #endregion
        #endregion

        #region Classes
        public class Section
        {
            private Dictionary<string, string> parameters;

            public Section()
            {
                parameters = new Dictionary<string, string>();
            }

            public string name { get; set; }

            public string GetParameter(string key)
            {
                return this.parameters[key];
            }

            public void Add(string key, string value)
            {
                this.parameters.Add(key, value);
            }
        }        
        #endregion
    }
    public class IncorrectIniLineException : Exception
    {
        public DateTime ErrorTimeStamp { get; set; }
        public string CauseOfError { get; set; }
        public IncorrectIniLineException() { }
        public IncorrectIniLineException(string message, string cause, DateTime time)
            : base(message)
        {
            CauseOfError = cause;
            ErrorTimeStamp = time;
        }
    }
}
