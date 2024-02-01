using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая_с_мощным_интерфейсом
{
    public class Human : IParameters
    {
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public ushort Weight { get; set; }

        public ushort Current_Floor { get; set; }

        public float Volume => ( Length * Width * Height * 0.4f);

        public Queue<int> Route = new Queue<int>();


        public Random random { get; set; }
        public Ultra_random random_ { get; set; }

        public void Make_Route(int Number_of_floors, int Index) //генерация маршрута человека по этажам
        {
            int floor = -1;
            int Limit=0;
            if (Index == 1)//нормальное
                Limit = (int)(Number_of_floors / ((int)Math.Abs(random_.r_.NextDouble())  +1 )) + 1;
            if (Index == 2)//равномерное
                Limit = (int)(Number_of_floors * random.NextDouble()) + 1;
            if (Index == 3)//экспоненциальное распределение
                Limit = (int)(Number_of_floors * (     (-Math.Log(random.NextDouble()))) / 2.0f + 1);
            for (int i = 0; i < Limit; i++)
            {
                floor = Return_random_floor(Number_of_floors);
                if (Route.Contains(floor)) { }
                else
                {
                    Route.Enqueue(floor);
                }
            }   
        }

        public int Return_random_floor(int Number_of_floors) // выбор случайного этажа из имеющихся
        {
            int r_;
            r_ = random.Next(2, Number_of_floors);
            return r_;
        }

        public Human Factory(float L, float Wi, float H, ushort We, ushort F, Random random_)
        {
            Human man_ = new Human();
            man_.Height = H;//генерация средних параметров человеческого тела
            man_.Width = Wi;
            man_.Length = L;
            man_.Weight = We;
            man_.Current_Floor= F;
            man_.random = random_;
            return man_;
        }

        public int Return_1_route()
        { 
            if (Route.Count() !=0)
                return Route.Peek();
            else
                return 0;
        }

        public int Call_lift() 
        {
            return Current_Floor;
        }

        public void Needed_floor()
        {
            Route.Dequeue();
        }
    }
}
