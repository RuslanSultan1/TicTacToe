using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe1
{
   public class Game : ICloneable
    {
        public char[] field;

        public Game(char[] _field=null)
        {
            if (_field != null)
            {
                this.field = _field;
            }
            else
            {
                Start();
            }
        }

        public void Start()
        {
            this.field = new char[9];
            for (int i = 0; i < 9; i++)
            {
                this.field[i] = ' ';
            }
        }

        public void SetCell(int _position, char _side)
        {
            this.field[_position] = _side;
        }

        public List<int> GetFreeCells()
        {
            List<int> free = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                char cell = this.field[i];
                if (cell == ' ')
                {
                    free.Add(i);
                }

            }
            return free;
        }

        public bool IsDraw()
        {
            List<int> free = this.GetFreeCells();
            return free.Count() == 0;
        }

        public bool IsWin(char side)
        {
            bool win;
            // проверяем на выигрыш по строкам
            for (int i = 0; i < 3; i++)
            {
                win = true;
                for (int j = 0; j < 3; j++)
                {
                    if (this.field[i * 3 + j] != side)
                    {
                        win = false;
                        break;
                    }
                }
                if (win)
                {
                    return win;
                }
            }
            // по столбцам
            for (int i = 0; i < 3; i++)
            {
                win = true;
                for (int j = 0; j < 3; j++)
                {
                    if (this.field[j * 3 + i] != side)
                    {
                        win = false;
                        break;
                    }
                }
                if (win)
                {
                    return win;
                }
            }

            win = true;
            // главная диагональ
            for (int i = 0; i < 3; i++)
            {
                if (this.field[i * 3 + i] != side)
                {
                    win = false;
                    break;
                }
            }
            if (win)
            {
                return win;
            }

            win = true;
            // побочная диагональ
            for (int i = 0; i < 3; i++)
            {
                if (this.field[i * 3 + 2 - i] != side)
                {
                    win = false;
                    break;
                }
            }
            if (win)
            {
                return win;
            }
            return false;

        }

        public char[] GetState(char side)
        {
            if (side == 'x')
            {
                return this.field;
            }

            char[] newField = new char[this.field.Length];
            //Array.Copy(this.field, newField, this.field.Length);

            for (int i = 0; i < this.field.Length; i++)
            {
                switch (this.field[i])
                {
                    case 'x':
                        newField[i] = 'o';
                        break;
                    case 'o':
                        newField[i] = 'x';
                        break;
                    default:
                        newField[i] = this.field[i];
                        break;
                }
            }
            return newField;
        }

        public object Clone()
        {
            char[] _field = new char[this.field.Length];
            Array.Copy(this.field, _field, field.Length);
            return new Game(_field);
        }
    }
}
