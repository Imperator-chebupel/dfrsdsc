using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_с_мощным_интерфейсом
{
    public class DataWorker
    {
        public List <List <float>> Capacity = new List<List <float>>();

        public void Add_Capacity()//добавление типа хранилища данных для отдельного лифта
        {
            Capacity.Add(new List<float>());
        }

        public void Save_Capacity(int Index, float data) //запись данных в хранилище данных лифта
        {
            int i = 1;
            foreach (List<float> Spisok in Capacity)
            {
                if (i == Index)
                {
                    Spisok.Add(data);
                }
                i++;
            }
        }

        public List<float> Return_Capacity(int Index) //типа выгрузка данных
        {
            int i = 1;
            foreach (List<float> Spisok in Capacity)
            {
                if (i == Index)
                {
                    return Spisok;
                }
                i++;
            }
            return null;
        }


    }
}
