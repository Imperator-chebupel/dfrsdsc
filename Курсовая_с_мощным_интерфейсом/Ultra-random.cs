using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_с_мощным_интерфейсом
{
    public class Ultra_random
    {
        NormalRandom r = new NormalRandom();
        public Random r_ = new Random();
        public float Parametre(float min, float max)
        {
            float result = (float)(r.NextDouble());
            return (float)(min + Math.Abs(result)*(max - min)); // очень сложный алгоритм
        }

        public float Parametre_exp(float min, float max)//экспоненциальное распределение
        {
            float result = ((float)(-Math.Log(r_.NextDouble()))/2.0f)  ;
            return (float)(min + result * (max - min));
        }

        public float Parametre_ravn(float min, float max)//равномерное распределение
        {
            return (float)(min + r_.NextDouble() * (max - min));
        }

        public int Razbros_na_floors(int Procent, int Number_of_humans) // генерация количества людей на этажах
        {
            return (int)(r.NextDouble() * Procent * 2 - Procent) + Number_of_humans;
        }

        class NormalRandom : Random//нормальное распределение
        {
            double prevSample = double.NaN;
            protected override double Sample()
            {
                if (!double.IsNaN(prevSample))
                {
                    double result = prevSample;
                    prevSample = double.NaN;
                    return result;
                }
                double u, v, s;
                do
                {
                    u = 2 * base.Sample() - 1;
                    v = 2 * base.Sample() - 1;
                    s = u * u + v * v;
                }
                while (u <= -1 || v <= -1 || s >= 1 || s == 0);
                double r = Math.Sqrt(-2 * Math.Log(s) / s);
                prevSample = r * v;
                return r * u;
            }
        }
    }
}
