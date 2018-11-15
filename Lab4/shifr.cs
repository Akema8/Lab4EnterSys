using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class shifr
    {
        private int[] KeyTab;//массив ключа

        private void GetKeyTab(string str, string key)//заполнение массива ключа символами
        {
            int j = 0;
            KeyTab = new int[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                KeyTab[i] = Convert.ToInt32(key[j]);
                j++;
                if (str[i] == ' ')
                {
                    KeyTab[i] = 0;
                    j--;
                }
                if (j == key.Length) j = 0;

            }
        }

        public string GetShifr(string str, string key)
        {
            string st = "";
            int k = 0;
            if (key == "") st = "Введите ключ";
            else
            {
                GetKeyTab(str, key);
                for (int i = 0; i < str.Length; i++)
                {
                    k = str[i] + (char)KeyTab[i];
                    st = st + (char)k;
                }
            }
            return st;
        }

        public string ReShifr(string str, string key)
        {
            string st = "";
            int k = 0;
            if (key == "") st = "Введите ключ";
            else
            {
                GetKeyTab(str, key);
                for (int i = 0; i < str.Length; i++)
                {
                    k = str[i] - (char)KeyTab[i];
                    st = st + (char)k;
                }
            }
            return st;
        }
    }
}
