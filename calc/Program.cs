using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //0 redo 1 undo   // 0 minus 1 plus 
            Calc calc = new Calc();
            calc.Current = 0;
            pult pult = new pult();
            pult.CalcSimpleCommands = 0;
            pult.CalcHardCommands = 1;
            pult.SetCommand(0, new CalcSimpleCommand(calc));
            pult.SetCommand(1, new CalcHardCommand(calc));
            pult.SetChange(0, new CalcCancel(calc));
            Console.WriteLine("0");
            string current = Console.ReadLine();
            pult.SetCurrent(int.Parse(current));
            string Oper = Console.ReadLine();

            while (Oper != "")
            {
                string Number = " ";
                switch (Oper)
                {
                    case "+":
                        Number = Console.ReadLine();
                        pult.Introduced = int.Parse(Number);
                        pult.PressPlusMinus(1);
                        break;
                    case "-":
                        Number = Console.ReadLine();
                        pult.Introduced = int.Parse(Number);
                        pult.PressPlusMinus(0);
                        break;
                    case "*":
                        Number = Console.ReadLine();
                        pult.Introduced = int.Parse(Number);
                        pult.PressMulDiv(1);
                        break;
                    case "/":
                        Number = Console.ReadLine();
                        pult.Introduced = int.Parse(Number);
                        pult.PressMulDiv(0);
                        break;
                    case "undo":
                        pult.PressCancel(1);
                        break;
                    case "redo":
                        pult.PressCancel(0);
                        break;
                    default:
                        MessageBox.Show("Неправильный ввод");
                        current = Console.ReadLine();
                        pult.SetCurrent(int.Parse(current));
                        break;
                }
                Oper = Console.ReadLine();
            }

        }
    }
}
