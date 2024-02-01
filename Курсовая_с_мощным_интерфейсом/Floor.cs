using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_с_мощным_интерфейсом
{
    public class Floor
    {
        public int Number { get; set; }

        public Queue<Human> Humans= new Queue<Human>();

        public void Add_human(Human man)//добавление человека
        {
            Humans.Enqueue(man);
        }

        public void Remove_human(Human man) { Humans.Dequeue(); }//удаление человека

        public Human First_human() 
        {
            return Humans.Peek();
        }

        public bool Any_humans()
        {
            if (Humans.Count >0)
                return true;
            else
                return false;
        }

    }
}