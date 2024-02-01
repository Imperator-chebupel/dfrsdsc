using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Курсовая_с_мощным_интерфейсом
{
    public class What_to_do
    {
        public int Number_of_floors { get; set; }
        public int Number_of_people { get; set; }
        public int Dispersion { get; set; }
        public int Number_of_moves { get; set; }
        public int Number_of_lifts { get; set; }

        public int Human_parameters { get; set; }
        public int Human_routes { get; set; }
        Ultra_random r = new Ultra_random();
        Random r_ = new Random();

        List<Lift> Lifts = new List<Lift>();
        List<Floor> Floors = new List<Floor>();

        public DataWorker data_worker;

        public void Set_ALL(int number_of_floors, int number_of_people, int dispersion, int number_of_lifts, int number_of_moves, int human_parameters, int human_routes, DataWorker DW) 
        {
            Number_of_floors = (number_of_floors.ToString() != "") ? number_of_floors : 10;
            Number_of_people = (number_of_people.ToString() != "") ? number_of_people : 20;
            Dispersion = (dispersion.ToString() != "") ? dispersion : 5;
            Number_of_lifts = (number_of_lifts.ToString() != "") ? number_of_lifts : 3;
            Number_of_moves = (number_of_moves.ToString() != "") ? number_of_moves : 15;
            Human_parameters = human_parameters;
            Human_routes = human_routes;
            data_worker = DW;
        }

        public bool Check_ALL_floors()
        {
            int Humans_here = 0;
            foreach (Floor floor in Floors)
            {
                if (floor.Humans.Count() >=1) { Humans_here++; }
            }
            if (Humans_here == 0)
                return false;
            else return true;
        }

        public void Generate_Humans(int razbros)//генерация количества людей на этаже
        {
            int i, j;
            int Humans_on_floor;
            Human man = new Human();
            for (i = 1; i < Number_of_floors+1; i++)
            {
                Floor F = new Floor {Number = i };
                Floors.Add(F);
                Humans_on_floor = r.Razbros_na_floors(razbros,  Number_of_people);
                for (j = 0; j < Humans_on_floor; j++)
                {
                    if (Human_parameters == 1)
                    {
                        Human man_ = man.Factory(r.Parametre(0.5f, 0.7f), r.Parametre(0.3f, 0.5f), r.Parametre(1.5f, 1.7f), Convert.ToUInt16(r.Parametre(30.0f, 60.0f)), Convert.ToUInt16(i), r_);
                        man_.Make_Route(Number_of_floors, Human_routes);
                        F.Add_human(man_);
                    }
                    if (Human_parameters == 2)
                    {
                        Human man_ = man.Factory(r.Parametre_ravn(0.5f, 0.7f), r.Parametre_ravn(0.3f, 0.5f), r.Parametre_ravn(1.5f, 1.7f), Convert.ToUInt16(r.Parametre_ravn(30.0f, 60.0f)), Convert.ToUInt16(i), r_);
                        man_.Make_Route(Number_of_floors, Human_routes);
                        F.Add_human(man_);
                    }
                    if (Human_parameters ==3)
                    {
                        Human man_ = man.Factory(r.Parametre_exp(0.5f, 0.7f), r.Parametre_exp(0.3f, 0.5f), r.Parametre_exp(1.5f, 1.7f), Convert.ToUInt16(r.Parametre_exp(30.0f, 60.0f)), Convert.ToUInt16(i), r_);
                        man_.Make_Route(Number_of_floors,Human_routes);
                        F.Add_human(man_);
                    }

                }
            }
        }

        public void Generate_Lifts(float L, float H, float Wi, ushort We )//создание одинаковых лифтов
        {
            for (int i = 0; i < Number_of_lifts; i++)
            {
                Lift lift = new Lift {Length = L, Height = H, Width = Wi, Weight = We, Current_Floor = (ushort)(r_.Next(1, Number_of_floors)) };
                lift.Current_Floor = Convert.ToUInt16(r_.Next(1, Number_of_floors));
                Lifts.Add(lift);
            }
        }

        public void Start_work(float L, float H, float Wi, ushort We)
        {
            Generate_Humans(Dispersion);//раскидал людей по этажам
            Generate_Lifts(L, H, Wi, We);
            for (int j = 0; j < Number_of_lifts; j++)//создание списка заддных о нагрузке
            {
                data_worker.Add_Capacity();
            }
            int i = 1;
            foreach (Lift lift in Lifts)
            {
                First_come_in(lift,i);
                i++;
            }
            for (int j =0; j<Number_of_moves; j++)
            {
                i = 1;
                foreach (Lift lift in Lifts)
                {
                        Start_move(lift, i);
                        i++;
                }
            }
            i = 1;
            foreach (Lift lift in Lifts)
            {
                if ((Check_ALL_floors()))
                {
                    Start_move(lift, i);
                    i++;
                }
            }
        }

        public void First_come_in(Lift lift, int i)
        {
            bool Inside_ = true;
            while (Inside_)//тут оно работает
            {
                if (Floors.Find(delegate (Floor F) { return F.Number == lift.Current_Floor; }).Any_humans())
                {
                    if (lift.Check_Human(Floors.Find(delegate (Floor F) { return F.Number == lift.Current_Floor; }).First_human())) //первый в очереди человек влезает?
                    {
                        lift.Come_in(Floors.Find(delegate (Floor F) { return F.Number == lift.Current_Floor; }).First_human());//человек залез в лифт
                        (Floors.Find(delegate (Floor F) { return F.Number == lift.Current_Floor; })).Remove_human((Floors.Find(delegate (Floor F) { return F.Number == lift.Current_Floor; })).First_human());//в очереди его больше нет
                    }
                    else
                        Inside_ = false;
                }

                else
                    break;

            }
            data_worker.Save_Capacity(i, lift.Capacity());//Сохранили изначальную нагрузку
            foreach (Human human in lift.Inside)//люди понажимали кнопки в лифте
            {
                if (human.Route.Count() != 0)
                {
                    if (lift.Route.Contains(human.Return_1_route())) { }
                    else
                    {
                        if (human.Return_1_route() != 0)
                        {
                            lift.Route.Add(human.Return_1_route());
                            human.Route.Dequeue();
                        }

                    }
                }
            }

        }




        public void Start_move(Lift lift, int j)
        {
            int min_Index;
            ushort To_floor;

            min_Index = 999;
            To_floor = 999;
            for (int i = 1; i < Number_of_floors + 1; i++)//проверка куда ехать, чтобы путь был минимальным
            {
                if ((i != lift.Current_Floor) && (lift.Current_Floor - i <= min_Index) && lift.Contains_in_route(i))//не текущий этаж + меньше предполагаемого маршрута + есть в маршруте
                {
                    min_Index = lift.Current_Floor - i;
                    To_floor = Convert.ToUInt16(i);
                }
            }
            lift.Current_Floor = To_floor;//лифт уехал на ближайших из вызванных этажей
            foreach (Human human in (lift.Inside))//вышли те, кому надо
            {
                if ((int)human.Route.Count() != 0)
                {
                    if (((int)human.Return_1_route() == (int)lift.Current_Floor))
                    {
                        lift.To_Outside.Add(human);
                        human.Needed_floor();
                        Floors[lift.Current_Floor].Add_human(human);
                    }
                }
                else
                    lift.To_Outside.Add(human);//если он всё посетил, то валит
            }
            foreach (Human human in lift.To_Outside)//исправил ошибку с невозможностью перечисления изменённой коллекции
            {
                lift.Come_out(human);
            }
            lift.To_Outside.Clear();
            data_worker.Save_Capacity(j, lift.Capacity());//Сохранили нагрузку
            if ((Floors.Find(delegate (Floor F) { return F.Number == lift.Current_Floor; }).Any_humans()))
            {
                First_come_in(lift, j);
            }
        }
    }
}