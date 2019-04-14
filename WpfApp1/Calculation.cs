using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class ResultValue
    {
        private int value;
        private int result;

        public ResultValue(int result, int value)
        {
            Result = result;
            Value = value;
        }

        public int Result { get => result; set => result = value; }
        public int Value { get => value; set => this.value = value; }
    }
    class Calculation
    {
        public static List<string> div_result;
        public static List<string> div_step;

        private static int MoreCount(double a, double b)
        {
            int count_a = BitConverter.GetBytes(decimal.GetBits((decimal)a)[3])[2];
            int count_b = BitConverter.GetBytes(decimal.GetBits((decimal)b)[3])[2];
            if (count_a > count_b)
                return count_a;
            else
                return count_b;
        }
        private static int ConvertToInt(double a,int count)
        {
            int x = 1;
            for(int i = 0; i < count; i++)
            {
                x *= 10;
            }
            double z = a * x;
            return Int32.Parse(z.ToString());
        }
        public static string Divide(double a, double b)
        {
            div_result = new List<string>();
            div_step=new List<string> ();
            PreSetupDiv(a, b, out int a_int, out int b_int, out int count);
            string a_s = a_int.ToString();//	2	2	6	7
            string b_s = b_int.ToString();//    4   5   0

            List<ResultValue> result = new List<ResultValue>();

            int i = 0;
            string totalresult = "";
            string temp_s = "";
            int endA = a_s.Length - 1;
            int afterInt = 0;
            bool first = false;
            int force=1;
            int endInf = 0;
            bool first_s = false;
            const string space_c = "  ";
            while (true)
            {
                if (temp_s == "0" && endA <= i - 1)
                {
                    break;
                }
                if (endA >= i)
                {
                    if (temp_s == "0")
                    {
                        temp_s = a_s[i].ToString();
                        afterInt = a_s[i] - '0';
                    }
                    else
                    {
                        temp_s += a_s[i];
                        afterInt = a_s[i] - '0';
                    }
                    if (Int32.Parse(temp_s) >= b_int)
                    {
                        
                        result.Add(MultiplyElem(b_int, Int32.Parse(temp_s)));
                        int ty = Int32.Parse(temp_s);
                        int tx = result[result.Count - 1].Result;
                        int tc = ty - tx;
                        temp_s = (tc).ToString();
                        totalresult += result[result.Count - 1].Value.ToString();
                        //Steps
                        int lenght = (a_s.Length - 1)-((a_s.Length - 1) - force);
                        if (first_s == false)
                        {
                           
                            div_step.Add("" + AddSpace(lenght, space_c) + a_s);
                            first_s = true;
                        }
                        else
                        {

                            div_step.Add("" + AddSpace(lenght, space_c) + ty);
                        }
                        
                        div_step.Add("" + AddSpace(lenght, space_c) + tx);


                        div_result.Add("" + b_int  + " x " + result[result.Count - 1].Value + " = " + tx);
                        div_result.Add("" + ty + " - " + tx + " = " + tc);
                        force++;
                    }
                    else
                    {
                        
                        if(totalresult!="")
                        totalresult += "0";
                    }
                }
                else
                {
                    if (a_int < b_int && first==false)
                    {
                        totalresult += "0.";
                        first = true;
                    }
                    endInf++;
                    if (endInf == 24)
                    {
                        // Не реализовано поиск повторяемых элементов при делении
                        totalresult=EndInfinity(totalresult);
                        break;
                    }
                    afterInt = Int32.Parse(temp_s);
                    if (first == false)
                    {
                        totalresult += ".";
                        first = true;
                    }
                    int countMultiple = 0;
                    while (afterInt < b_int)
                    {
                        if (countMultiple > 0)
                        { totalresult += "0"; }
                        afterInt *= 10;
                        countMultiple++;
                    }
                    result.Add(MultiplyElem(b_int, afterInt));
                    
                    int ty = afterInt;
                    int tx = result[result.Count - 1].Result;
                    int tc = ty - tx;
                    temp_s = (tc).ToString();
                    totalresult += result[result.Count - 1].Value.ToString();

                    
                    if (CheckInfinity(ref totalresult) == true)
                    {
                        string tempes = afterInt.ToString().Remove(afterInt.ToString().Length-1);
                        div_step.Add("" + AddSpace(a_s.Length - countMultiple+1, space_c) + tempes);
                        break;
                    }
                    else
                    {
                        int lenght = (a_s.Length - 1) - ((a_s.Length - 1) - force);
                        //Шаги
                        div_step.Add("" + AddSpace(lenght, space_c) + ty); 
                        div_step.Add( "" + AddSpace(lenght, space_c) + tx);//


                        div_result.Add("" + b_int + " x " + result[result.Count - 1].Value + " = " + tx);//
                        div_result.Add("" + ty + " - " + tx + " = " + tc);//
                        force++;
                    }
                }
                i++;
            }
            return totalresult;
        }
        public static string AddSpace(int count,string c)
        {
            string temp = "";
            for (int i = 0; i < count; i++)
            {
                temp += c;
            }
                return temp;
        }
        private static bool CheckInfinity(ref string result)
        {
            int temp = 0;
            for(int i=0;i<result.Length;i++)
            {
                if (result[i]=='.')
                {
                    temp = i;
                    break;
                }
            }
            int check = (result.Length - 1) - temp;
            if (check > 1)
            {
                for (int j = temp; j < result.Length; j++)
                {
                    if (j + 1 != result.Length)
                    {
                        if (result[j] == result[j + 1])
                        {
                            result = result.Remove(j + 1, 1);
                            result = result.Insert(j, "(");
                            result += ")";
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private static string EndInfinity(string result)
        {
            int temp = 0;
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == '.')
                {
                    temp = i;
                    break;
                }
            }
            result = result.Insert(temp+1, "(");
            result += ")";

            return result;
        }
        private static ResultValue MultiplyElem(int multiple, int max)
        {
            int result=0;//результат
            int value=0;//на сколько умножить оригинальное число
            for (int i = 1; i < 10; i++)
            {                
               
                
                value = i;
                result = value * multiple;
                if (result + multiple == max)
                {
                    result += multiple;
                    value += 1;
                    break;
                }
                if (result+multiple > max)
                {
                    break;
                }
            }
            value=value;
            result=result;
            return new ResultValue(result, value);
        }

        public static string Minus(double a, double b)
        {
            double c = a - b;
            string temp = c.ToString();
            return temp;
        }

        public static string Sum(double a, double b)
        {
            double c = a + b;
            string temp = c.ToString();
            return temp;
        }

        public static string Multiple(double a, double b)
        {
            PreSetupDiv(a, b, out int a_int, out int b_int,out int count);
            div_step = new List<string>();
            string a_s = a_int.ToString();
            string b_s = b_int.ToString();

            int[] result = new int[b_s.Length];
            double total=0;
            int temp_i = 0;
            for (int i=b_s.Length-1;i>-1;i--)
            {
                int b_temp = int.Parse(b_s[i].ToString());
                result[temp_i] = a_int * b_temp;

                div_step.Add(result[temp_i].ToString()+AddSpace(temp_i,"  "));//

                total += result[temp_i] *Math.Pow(10,temp_i++);
                
            }
            total = total/Math.Pow(10, temp_i);
            string temp = total.ToString();
            return temp;
        }
        private static string FormatOutput(double a,int count)
        {
            string temp = "";
            for (int i = 0; i < count; i++)
            {
                temp+= "0";
            }
            return String.Format("{0:0."+temp+"}", a);
        }
        public static void PreSetupDiv(double a, double b,out int a_int,out int b_int, out int count)
        {
            count= MoreCount(a, b);
            string temp = FormatOutput(a,count) + "---" + FormatOutput(b, count);
            a_int = ConvertToInt(a, count);
            b_int = ConvertToInt(b, count);
        }
        public static void PreSetupMult(double a, double b, out int a_int, out int b_int, out int count)
        {
            count = MoreCount(a, b);
            string temp = FormatOutput(a, count) + "---" + FormatOutput(b, count);
            a_int = ConvertToIntMult(b, count);
            b_int = ConvertToIntMult(b, count);
        }
        private static int ConvertToIntMult(double a, int count)
        {
            count = BitConverter.GetBytes(decimal.GetBits((decimal)a)[3])[2];
            int x = 1;
            for (int i = 0; i < count; i++)
            {
                x *= 10;
            }
            double z = a * x;
            return Int32.Parse(z.ToString());
        }
        public static bool Check(string a)
        {
            return Double.TryParse(a,out double set);
        }
    }
}
