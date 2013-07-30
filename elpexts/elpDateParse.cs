using System;

namespace elp.Extensions
{
    public static class elpDateParse
    {
        /// <summary>
        /// Разбирает строку в формат Дата-Время
        /// </summary>
        /// <param name="inString">Строка в формате "dd.MM.yyyy hh.mm.ss" или ""ddMMyy hhmm"</param>
        /// <returns></returns>
        /// /// <exception cref="elpDateTimeParseExeption"></exception>
        public static DateTime DateTimeParse(string inString)
        {
            DateTime parsedDateTime = new DateTime();
            bool isParsed = false;
            string temp = "";
            try
            {
                parsedDateTime = DateTime.Parse(inString);
                isParsed = true;
            }
            catch (Exception)
            {
                isParsed = false;
            }
            if (isParsed == true) return parsedDateTime;
            else
            {
                string day, month, year, hour, minute;
                if (inString.Length != 11) throw new elpDateTimeParseExeption("Неверный формат даты.", "Длина строки короткой записи должна быть в формате DDMMYY hhmm", DateTime.Now);
                day = inString[0].ToString() + inString[1].ToString();
                month = inString[2].ToString() + inString[3].ToString();
                year = "20" + inString[4].ToString() + inString[5].ToString();
                hour = inString[7].ToString() + inString[8].ToString();
                minute = inString[9].ToString() + inString[10].ToString();
                try
                {
                    temp = day + "." + month + "." + year + " " + hour + ":" + minute;
                    parsedDateTime = DateTime.Parse(temp);
                    isParsed = true;
                }
                catch
                {
                    isParsed = false;
                }
                if (isParsed == true) return parsedDateTime;
                else
                {
                    throw new elpDateTimeParseExeption("Неверный формат даты.", "Ошибка разбора - " + temp, DateTime.Now);
                }
            }
        }
    }
}
