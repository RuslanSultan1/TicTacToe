using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe1
{
    class Person : IPlayer
    {
        private Form1 form;
        private char side = 'x';

        public Person(char side,Form1 form)
        {
            this.side = side;
            this.form = form;
        }

        public char GetSide()
        {
            return this.side;
        }

        public void MakeStep(Game game)
        {
            List<int> free = game.GetFreeCells();
            int input = form.currentButton;
            game.SetCell(input, this.side);
        }

        public void Win()
        {
            MessageBox.Show("Person " + side+" wins!");
        }

        public void Lose()
        {
        }

        public void Draw()
        {
            MessageBox.Show("It's a draw.");
        }
    }
}
