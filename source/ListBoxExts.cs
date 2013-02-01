using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace elpexts
{
    public static class ListBoxExts
    {
        /// <summary>
        /// Возвращает значение индекса выбранного элемента
        /// </summary>
        public static int getSelectedIndex (this ListBox curListBox)
        {
            return curListBox.SelectedIndex;
        }

        /// <summary>
        /// Возвращает коллекцию значений индексов выбранных элементов
        /// </summary>
        public static List<int> getSelectedIndeces(this ListBox curListBox)
        {
            List<int> selectedIndeces = new List<int>();
            foreach (ListBoxItem lbItem in curListBox.Items)
            {
                int index = curListBox.Items.IndexOf(lbItem);
                if (lbItem.IsSelected)
                {
                    selectedIndeces.Add(index);
                }
            }
            return selectedIndeces;
        }

        /// <summary>
        /// Добавляет новый item в ListBox или ListView
        /// </summary>
        /// <param name="curItemCollection">коллекция элементов</param>
        /// <param name="value">Текст нового элемента</param>
        public static void addString(this ItemCollection curItemCollection, string value)
        {
            ListViewItem newLBItem = new ListViewItem();
            newLBItem.Content = value;
            curItemCollection.Add(newLBItem);
        }        
    }
}
