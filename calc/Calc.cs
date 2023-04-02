using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calc
{
    // сделать историю линкед листом в pult в calc методы с 1 числом убрать introduced 
    interface Icommand
    {
        int Positive(int Introduced);
        int Negative(int Introduced);
    }
    interface IChange
    {
        void Change(int current);
    }
    class Calc
    {
        public int Current { get; set; } = 0;

        public int Plus(int Introduced)
        {
            Current = Current + Introduced;
            return Current;
        }
        public int Minus(int Introduced)
        {
            Current = Current - Introduced;
            return Current;
        }
        public int Multiplication(int Introduced)
        {
            Current = Current * Introduced;
            return Current;
        }
        public int Division(int Introduced)
        {
            Current = Current / Introduced;
            return Current;
        }
        public void ChangeCurrent(int current)
        {
            Current = current;
        }


    }
    class CalcSimpleCommand : Icommand
    {
        private Calc calc;
        public CalcSimpleCommand(Calc calc) => this.calc = calc;
        public int Positive(int Introduced) => calc.Plus(Introduced);
        public int Negative(int Introduced) => calc.Minus(Introduced);
    }
    class CalcHardCommand : Icommand
    {
        private Calc calc;
        public CalcHardCommand(Calc calc) => this.calc = calc;
        public int Positive(int Introduced) => calc.Multiplication(Introduced);
        public int Negative(int Introduced) => calc.Division(Introduced);
    }
    class CalcCancel : IChange
    {
        private Calc calc;
        public CalcCancel(Calc calc) => this.calc = calc;
        public void Change(int current) => calc.ChangeCurrent(current);

    }
    class pult
    {
        private List<Icommand> commands;
        private List<IChange> changes;
        private List<int> History;
        private int current { get; set; } = 0;

        public int CalcSimpleCommands { get; set; }
        public int CalcHardCommands { get; set; }
        public int Introduced { get; set; }
        public int result { get; set; }

        public pult()
        {
            commands = new List<Icommand>() { null, null };
            changes = new List<IChange>() { null, null };
            History = new List<int>();
            History.Add(0);
        }
        public void SetCommand(int button, Icommand command) => commands[button] = command;
        public void SetChange(int button, IChange Change) => changes[button] = Change;

        public void PressPlusMinus(int button)
        {
            for (int i = History.Count - 1; i > current; i--)
            {
                History.RemoveAt(i);
            }
            if (button == 1)
            {
                result = commands[0].Positive(Introduced);
                History.Add(result);
                Console.WriteLine(result);
            }
            else
            {
                result = commands[0].Negative(Introduced);
                History.Add(result);
                Console.WriteLine(result);
            }
            current++;
        }
        public void PressMulDiv(int button)
        {
            for (int i = History.Count - 1; i > current; i--)
            {
                History.RemoveAt(i);
            }
            if (button == 1)
            {
                result = commands[1].Positive(Introduced);
                History.Add(result);
                Console.WriteLine(result);
            }
            else
            {
                result = commands[1].Negative(Introduced);
                History.Add(result);
                Console.WriteLine(result);
            }
            current++;
        }
        public void PressCancel(int button)
        {

            if (button == 1)
            {
                if (current < 1)
                {
                    MessageBox.Show("Ошибка");
                }
                else
                {
                    Console.WriteLine(History[current - 1]);
                    changes[0].Change(History[current - 1]);
                    current--;
                    //undo
                }
            }
            else
            {
                if (current >= History.Count - 1)
                {
                    MessageBox.Show("Ошибка");
                }
                else
                {
                    Console.WriteLine(History[current + 1]);
                    changes[0].Change(History[current + 1]);
                    current++;
                }
            }
        }
        public void SetCurrent(int current)
        {
            changes[0].Change(current);
        }
    }
}
