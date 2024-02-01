using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Windows.Forms.DataVisualization.Charting;

namespace Курсовая_с_мощным_интерфейсом
{
    public partial class Form1 : Form
    {
        Ultra_random r = new Ultra_random();
        int Humans_parameters = 1;
        int Humans_routes = 2;
        What_to_do Work = new What_to_do();
        FileWorker FW = new FileWorker();
        DataWorker DW = new DataWorker();
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Text += "Моделирование системы лифтов\n\rПараметры по умолчанию:\nэтажей - 10;\nв среднем людей на этаже - 80;\nразборс - +-5%\nколичество лифтов - 3";
            checkBox1.Visible= false;//не видим расширенные настройки
            checkBox2.Visible= false;
            checkBox3.Visible= false;
            label5.Visible = false;

            checkBox1.Checked = true;

            label7.Visible = false;
            checkBox4.Visible = false;
            checkBox5.Visible = false;
            checkBox6.Visible = false;

            checkBox5.Checked= true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            What_to_do Work = new What_to_do();
            
            string Number_of_floors = Floors.Text;
            string Number_of_people = People.Text;
            string Dispersion = Razbros.Text;
            string Number_of_lifts = Lifts.Text;
            string Number_of_moves_ = Number_of_moves.Text; ;
            Work.Set_ALL((Number_of_floors != "") ? Math.Abs(Int32.Parse(Number_of_floors)) : 5,
                Number_of_people!= "" ? Math.Abs(Int32.Parse(Number_of_people)) : 20,
                Dispersion != "" ? Math.Abs(Int32.Parse(Dispersion)) : 5, 
                Number_of_lifts != "" ? Math.Abs(Int32.Parse(Number_of_lifts)) : 3,
                Number_of_moves_ != "" ? Math.Abs(Int32.Parse(Number_of_moves_)) : 15,
                Humans_parameters, Humans_routes, DW );
            Work.Start_work(1.5f, 2.2f, 1.5f, 700);
            FW.WriteDown(Work);
            richTextBox1.Text += "\nУспех";
            int N_O_L = 1;
            int Time = 0;
            foreach (List<float> Data in Work.data_worker.Capacity)
            {
                Series series = chart1.Series.Add(N_O_L.ToString());
                foreach (float Cp in Data)
                {
                    series.Points.AddXY(Time, Cp);
                    Time++;
                }
                N_O_L++;
                Time = 0;
            }
            
        }



        private void button2_Click(object sender, EventArgs e)//инструкция
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button3_Click(object sender, EventArgs e)//загрузка данных
        {
            Floors.Text = "";
            Floors.Text = FW.Read_data()[0].ToString();
            Number_of_moves.Text = "";
            Number_of_moves.Text = FW.Read_data()[3].ToString();
            Razbros.Text = "";
            Razbros.Text = FW.Read_data()[2].ToString();
            People.Text = "";
            People.Text = FW.Read_data()[1].ToString();
            Lifts.Text = "";
            Lifts.Text = FW.Read_data()[4].ToString();

            richTextBox1.Text += "\n Данные загружены";
        }

        private void button5_Click(object sender, EventArgs e)//удаление данных
        {
            FW.Delete_Data();
        }

        private void button6_Click_1(object sender, EventArgs e)//настройки
        {
            checkBox1.Visible = !checkBox1.Visible;
            checkBox2.Visible = !checkBox2.Visible;
            checkBox3.Visible = !checkBox3.Visible;
            label5.Visible = !label5.Visible;

            label7.Visible = !label7.Visible;
            checkBox4.Visible = !checkBox4.Visible;
            checkBox5.Visible = !checkBox5.Visible;
            checkBox6.Visible = !checkBox6.Visible;
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
            Humans_parameters = 1;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
            }
            Humans_parameters = 2;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)//задание параметров генерации
        {
            if (checkBox3.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
            }
            Humans_parameters = 3;
        }


        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                checkBox5.Checked = false;
                checkBox6.Checked = false;
            }
            Humans_routes = 1;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                checkBox6.Checked = false;
                checkBox4.Checked = false;
            }
            Humans_routes = 2;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                checkBox4.Checked = false;
                checkBox5.Checked = false;
            }
            Humans_routes = 3;
        }

        private void button4_Click(object sender, EventArgs e)//чистка графиков
        {
            for (int i = 0; i < Work.Number_of_lifts; i++)
            {
                this.chart1.Series[i].Points.Clear();
            }
        }

    
    }
}
