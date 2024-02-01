using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_с_мощным_интерфейсом
{
    public class Lift : IParameters
    {
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public ushort Weight { get; set; }
        public ushort Current_Floor { get; set; }

        public float Volume => (Length * Width * Height);

        public List<Human> Inside = new List<Human>();
        public List<Human> To_Outside = new List<Human>();
        public List<int> Route = new List<int>();

        public void Come_in(Human human)// лобавление человека
        {

                Inside.Add(human);
        }
        public void Come_out(Human human)//вывод человека
        { Inside.Remove(human);
            //To_Outside.Remove(human);
        }

        public float Capacity() //загруженность
        {
            float W = 0.0f;
            foreach (Human man in Inside)
            {
                W += man.Weight;
            }
            return (W / Weight) * 100.0f;
        }

        public bool Check_Human(Human human)//проверка на возможность зайти в лифт
        {
            float V = 0.0f;
            int W = 0;
            foreach (Human man in Inside)
            {
                V += man.Volume;
                W += man.Weight;
            }
            if ((V + human.Volume < Volume ) && (W + human.Weight < Weight))
                    return true;
            else
                return false;
        }


        public bool Contains_in_route(int F)
        {
            bool Kostil = false;
            foreach (int R in Route)
            {
                if (R == F)
                    Kostil= true;
            }
            return Kostil;
        }

    }
}
