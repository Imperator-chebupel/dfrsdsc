using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_с_мощным_интерфейсом
{
    public class FileWorker
    {

        public void WriteDown(What_to_do what_To_Do)
        {
            string File_name = "Параметры_модели.txt";
            string FileFolderPath = @"C:\Users\azza1\OneDrive\Рабочий стол\Мощное хранилище данных\";
            string Full_Path = FileFolderPath + File_name;

            string Par1 = what_To_Do.Number_of_floors.ToString() + " ";
            string Par2 = what_To_Do.Number_of_people.ToString()+ " ";
            string Par3 = what_To_Do.Dispersion.ToString() + " ";
            string Par4 = what_To_Do.Number_of_moves.ToString() + " ";
            string Par5 = what_To_Do.Number_of_lifts.ToString() + " ";
            string Par6 = what_To_Do.Human_parameters.ToString() + " ";
            string Par7 = what_To_Do.Human_routes.ToString() + " ";

            string All_Par = Par1 + Par2 + Par3 + Par4 + Par5 + Par6 + Par7;

            FileStream file = new FileStream(Full_Path, FileMode.Open);
            StreamWriter write = new StreamWriter(file);
            write.Write(All_Par);
            write.Dispose();
            file.Dispose();
        }

        public string[] Read_data()
        {
            string File_name = "Параметры_модели.txt";
            string FileFolderPath = @"C:\Users\azza1\OneDrive\Рабочий стол\Мощное хранилище данных\";
            string Full_Path = FileFolderPath + File_name;
            FileStream file = new FileStream(Full_Path, FileMode.Open);
            StreamReader read = new StreamReader(file);
            string Not_Result = read.ReadToEnd();
            string[] Result = Not_Result.Split(' ');
            read.Dispose();
            file.Dispose();
            return Result;
        }

        public void Delete_Data()//костыльное удаление данных
        {
            string File_name = "Параметры_модели.txt";
            string FileFolderPath = @"C:\Users\azza1\OneDrive\Рабочий стол\Мощное хранилище данных\";
            string Full_Path = FileFolderPath + File_name;
            File.Delete(Full_Path);
            var file = File.Create(Full_Path);
            file.Dispose();
        }

       
    }
}
