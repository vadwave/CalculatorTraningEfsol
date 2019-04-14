using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double a_d;
        double b_d;


        public MainWindow()
        {
            InitializeComponent();
            Clear();

        }

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
        }

        private void DivLabel(double a,double b)
        {
            Clear();
            string temp=Calculation.Divide(a, b);
            Calculation.PreSetupDiv(a, b, out int a_int, out int b_int, out int count);

            int xsize = a_int.ToString().Length;
            outputText.Margin = new Thickness(155-(xsize*12), 20, 0, 0);
            outputText2.Margin = new Thickness(155, 20, 0, 0);
            outputText3.Margin = new Thickness(360, 20, 0, 0);
            outputText4.Margin = new Thickness(145, 20, 0, 0);
            outputText4.Content = Calculation.AddSpace(temp.Length,"___");

            for (int i = 0; i < Calculation.div_result.Count; i++)
            {
                outputText3.Content += Calculation.div_result[i] + Environment.NewLine;            
            }
           
            for (int i = 0; i < Calculation.div_step.Count; i++)
            {
                outputText.Content += Calculation.div_step[i] + Environment.NewLine;             
            }          
            outputText2.Content = "|"+ b_int.ToString();
            outputText2.Content+= Environment.NewLine + "|" + temp;
        }
        private void MultLabel(double a, double b)
        {
            Clear();
            string temp = Calculation.Multiple(a, b);
            Calculation.PreSetupMult(a, b, out int a_int, out int b_int, out int count);

            int xsize = temp.ToString().Length;

            outputText2.Margin = new Thickness(145 - xsize*2, 20, 0, 0);
            outputText2.Content = "X";

            textBlock.Margin = new Thickness(145, 20, 0, 0);
            textBlock.TextAlignment=TextAlignment.Right;

            textBlock.Text= a.ToString() + Environment.NewLine;
            textBlock.Text += b.ToString() + Environment.NewLine;
            outputText4.Margin = new Thickness(145, 35, 0, 0);
            outputText4.Content = Calculation.AddSpace(temp.Length, "__") + "______";

            for (int i = 0; i < Calculation.div_step.Count; i++)
            {
                textBlock.Text += Calculation.div_step[i] + Environment.NewLine;
                
            }
            outputText3.Margin = new Thickness(145, 35+(Calculation.div_step.Count*15), 0, 0);
            outputText3.Content = Calculation.AddSpace(temp.Length, "__") + "______";

            textBlock.Text += "  "+temp;

        }
        private void PlusLabel(double a,double b)
        {
            Clear();
            string a_s;
            string b_s;
            string temp = Calculation.Sum(a, b);
            Calculation.PreSetupDiv(a, b, out int a_int, out int b_int, out int count);
            int xsize = temp.ToString().Length;
            outputText2.Margin = new Thickness(145 - xsize * 2, 20, 0, 0);
            outputText2.Content = "+";

            textBlock.Margin = new Thickness(145, 20, 0, 0);
            textBlock.TextAlignment = TextAlignment.Right;

            if (count == 0)
            {
                a_s = a_int.ToString();
                b_s = b_int.ToString();
            }
            else
            {
                a_s = a_int.ToString().Insert(a_int.ToString().Length - count, ",");
                b_s = b_int.ToString().Insert(b_int.ToString().Length - count, ",");
            }
            textBlock.Text = a_s + Environment.NewLine;
            textBlock.Text += b_s + Environment.NewLine;
            outputText4.Margin = new Thickness(145, 35, 0, 0);
            outputText4.Content = Calculation.AddSpace(temp.Length, "__") + "" ;
            textBlock.Text += temp + "";


        }
        private void MinusLabel(double a, double b)
        {
            Clear();
            string a_s;
            string b_s;
            string temp = Calculation.Minus(a, b);
            Calculation.PreSetupDiv(a, b, out int a_int, out int b_int, out int count);
            int xsize = temp.ToString().Length;
            outputText2.Margin = new Thickness(145 - xsize * 2, 20, 0, 0);
            outputText2.Content = "-";

            textBlock.Margin = new Thickness(145, 20, 0, 0);
            textBlock.TextAlignment = TextAlignment.Right;
            if (count == 0)
            {
                a_s = a_int.ToString();
                b_s = b_int.ToString();
            }
            else
            {
                a_s = a_int.ToString().Insert(a_int.ToString().Length - count, ",");
                b_s = b_int.ToString().Insert(b_int.ToString().Length - count, ",");
            }
            
            if (a_int >= b_int)
            {
                textBlock.Text = a_s + Environment.NewLine;
                textBlock.Text += b_s + Environment.NewLine;
            }
            else
            {
                textBlock.Text = b_s + Environment.NewLine;
                textBlock.Text += a_s + Environment.NewLine;
            }
            outputText4.Margin = new Thickness(145, 35, 0, 0);
            outputText4.Content = Calculation.AddSpace(temp.Length, "__") + "";
            textBlock.Text += temp + "";


        }


        private void ButtonMultiple_Click(object sender, RoutedEventArgs e)
        {
            if (Accept())
            {
                MultLabel(a_d, b_d);
                labelOperation.Content = "x";
            }
        }

        private void ButtonDiv_Click(object sender, RoutedEventArgs e)
        {
            if (Accept())
            {
                DivLabel(a_d, b_d);
                labelOperation.Content = "/";
            }
        }
        private void Clear()
        {
            outputText.Content = "";
            outputText2.Content = "";
            outputText3.Content = "";
            outputText4.Content = "";
            textBlock.Text = "";
        }

        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            if (Accept())
            {
                PlusLabel(a_d, b_d);
                labelOperation.Content = "+";
            }
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            if (Accept())
            {
                MinusLabel(a_d, b_d);
                labelOperation.Content = "-";
            }

        }
        private bool Accept()
        {
            string a = inputA.Text;
            string b = inputB.Text;
            if (Calculation.Check(a) && Calculation.Check(b))
            {
                a_d = Double.Parse(a);
                b_d = Double.Parse(b);
                return true;
            }
            else
            {
                
                MessageBox.Show("Введите десятичные или целые числа");
                return false;
            }
        }
    }
}
