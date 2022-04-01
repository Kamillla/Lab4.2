using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4._2
{
    public partial class Form1 : Form
    {
        Model model;

        public Form1()
        {
            InitializeComponent();

            NewValue();

            model = new Model(Int32.Parse(textBox1.Text), Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text));
            model.observers += new System.EventHandler(this.UpdateFromModel);
            model.observers.Invoke(this, null);

        }

        //считывание начальных значений перменных А и С из файла
        private void NewValue()
        {
            StreamReader text = new StreamReader(@"C:\Users\kmlll\source\repos\lab4.2\lab4(2).txt");
            while (!text.EndOfStream)
            {
                textBox1.Text = text.ReadLine();
                textBox2.Text = textBox1.Text;
                textBox3.Text = text.ReadLine();
            }
            text.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
                model.SetValue(1, Decimal.ToInt32(numericUpDown1.Value));
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                    model.SetValue(1, Int32.Parse(textBox1.Text));
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
                model.SetValue(1, trackBar1.Value);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            model.SetValue(2, Decimal.ToInt32(numericUpDown2.Value));
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                model.SetValue(2, Int32.Parse(textBox2.Text));
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            model.SetValue(2, trackBar2.Value);
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                model.SetValue(3, Int32.Parse(textBox3.Text));
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            model.SetValue(3, Decimal.ToInt32(numericUpDown3.Value));
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            model.SetValue(3, trackBar3.Value);
        }

        //сохранение текущих значений переменных А и С в txt файл
        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text + "\n" + textBox3.Text;
            File.WriteAllText(@"C:\Users\kmlll\source\repos\lab4.2\lab4(2).txt", text);

            MessageBox.Show("Файл сохранен:)");
        }

        private void UpdateFromModel(object sender, EventArgs e)
        {
            textBox1.Text = model.getValue(1).ToString();
            numericUpDown1.Value = model.getValue(1);
            trackBar1.Value = model.getValue(1);

            textBox2.Text = model.getValue(2).ToString();
            numericUpDown2.Value = model.getValue(2);
            trackBar2.Value = model.getValue(2);

            textBox3.Text = model.getValue(3).ToString();
            numericUpDown3.Value = model.getValue(3);
            trackBar3.Value = model.getValue(3);
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////
    public class Model
    {
        int value1;
        int value2;
        int value3;
        public System.EventHandler observers;

        public Model(int value1, int value2, int value3)
        {
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
        }

        public void SetValue(int sign, int value)
        {
            if (sign == 1)
            {
                if (value > 100)
                    value = 100;
                if (value < 0)
                    value = 0;
                if (value3 >= value)
                {
                    value1 = value;
                    if (value1 >= value2)
                        value2 = value1;
                }
                else 
                    value1 = getValue(1);
                observers.Invoke(this, null);

            }

            else if (sign == 2)
            {
                if (value3 >= value)
                {
                    if (value1 <= value)
                        value2 = value;
                    else value2 = value1;
                }
                else 
                    value2 = value3;
                observers.Invoke(this, null);
            }

            else
            {
                if (value > 100)
                    value = 100;
                if (value < 0)
                    value = 0;
                if (value1 <= value)
                {
                    value3 = value;
                    if (value3 < value2)
                        value2 = value;
                }
                else 
                    value3 = getValue(3);
                observers.Invoke(this, null);
            }
        }

        public int getValue(int sign)
        {
            if (sign == 1)
                return value1;
            else if (sign == 2)
                return value2;
            else 
                return value3;
        }
    }
}
