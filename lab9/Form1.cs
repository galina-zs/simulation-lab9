using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab9
{
    public partial class Form1 : Form
    {
        double U = 1;
        const int p = 5087; // большое простое число
        int M = 2900;       // M = p - 3^n. Берем n = 7, потому что 3^8 > p
        double R;
        double[] probabilities = new double[5] { 0, 0, 0, 0, 0 }; // i-тый элемент хранит в себе сумму 0..i вероятностей
        double[] statistics = new double[5] { 0, 0, 0, 0, 0 };

        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            probabilities[0] = (double)probability1NumericUpDown.Value;
            probabilities[1] = (double)probability2NumericUpDown.Value + probabilities[0];
            probabilities[2] = (double)probability3NumericUpDown.Value + probabilities[1];
            probabilities[3] = (double)probability4NumericUpDown.Value + probabilities[2];
            probabilities[4] = 1;

            for (int i = 0; i < statistics.Length; i++)
                statistics[i] = 0;
            FindStatistics((int)numberOfExperimentsNumericUpDown.Value);

            for (int i = 0; i < statistics.Length; i++)
                chart1.Series[0].Points.AddXY(i+1, statistics[i] / (double)numberOfExperimentsNumericUpDown.Value);
        }

        private void FindStatistics(int numberOfExperiments)
        {
            for (int i=0; i< numberOfExperiments; i++)
            {
                Generator();
                for (int j =0; j < probabilities.Length; j ++)
                {
                    if (R < probabilities[j])
                    {
                        statistics[j]++;
                        break;
                    }
                }
            }
        }

        private void Generator() // метод вычетов. Модификация Коробова
        {
            R = U / p;
            U = (U * M) % p;
        }
    }
}
