using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace elp87.Helpers
{
    public class Logger
    {
        #region Fields
        private string _fileName;
        private string _filePath;
        #endregion

        #region Constructors
        public Logger(string dirPath, string logName)
        {
            if(!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            _fileName = logName + "_" + DateTime.Now.ToString("yyMMdd-HH-mm-ss") + ".txt";
            if (dirPath.EndsWith(@"\"))
            {
                _filePath = dirPath + _fileName;
            }
            else
            {
                _filePath = dirPath + @"\" + _fileName;
            }
            File.Create(_filePath).Close();            
        }
        #endregion

        #region Properties
        public string FileName { get { return _fileName; } }
        
        public string FilePath { get { return _filePath; } }
        #endregion

        #region Methods
        public void WriteLine(string line)
        {
            using (StreamWriter file = new StreamWriter(_filePath, true))
            {
                file.WriteLine(line);
            }
        }
        #endregion
    }
}
