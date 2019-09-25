using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe1
{
    class AIPlayer : IPlayer
    {
        private Form1 form;
        private char side = 'x';
        private AI ai = null;
        private char[] oldState = null;

        public AIPlayer(char side, AI ai,Form1 form)
        {
            this.side = side;
            this.ai = ai;
            this.form = form;
        }

        public char GetSide()
        {
            return this.side;
        }

        public void MakeStep(Game game)
        {
            List<int> free = game.GetFreeCells();
            Random rand = new Random();
            //случайным образом решаем, является ли текущий ход 
            //зондирующим (случайным) или жадным (максимально выгодным)
            if (rand.Next(0, 100) < 20)
            {
                //случайный ход
                int _step = free[rand.Next(0, free.Count)];
                game.SetCell(_step, this.side);
                form.performAction(_step);
                this.oldState = game.GetState(this.side);
                return;
            }

            //жадный ход
            Dictionary<int, double> rewards = new Dictionary<int, double>();
            foreach (int free_step in free)
            {
                //Console.WriteLine(free_step);
                //для каждого доступного хода оцениваем состояние игры после него
                Game newGame = (Game)game.Clone();
                newGame.SetCell(free_step, this.side);
                rewards[free_step] = this.ai.GetReward(newGame.GetState(this.side));
            }

            //выясняем, какое вознаграждение оказалось максимальным
            double maxReward = 0;
            int maxStep = -1;
            foreach (int _step in rewards.Keys)
            {
                double reward = rewards[_step];
                if (reward >= maxReward)
                {
                    maxReward = reward;
                    maxStep = _step;
                }
            }

            //корректируем оценку прошлого состояния
            //с учетом ценности нового состояния
            if (this.oldState != null)
            {
                this.ai.Correct(this.oldState, maxReward);
            }
            game.SetCell(maxStep, this.side);
            form.performAction(maxStep);
            //сохраняем текущее состояние для того, 
            //чтобы откорректировать её ценность на следующем ходе
            this.oldState = game.GetState(this.side);
        }

        public void Win()
        {
            if (oldState != null)
            {
                this.ai.Correct(this.oldState, 1);
            }
            MessageBox.Show("Computer "+this.side + " wins!");
        }

        public void Lose()
        {
            if (oldState != null)
            {
                this.ai.Correct(this.oldState, 0);
            }
        }

        public void Draw()
        {
            if (oldState != null)
            {
                this.ai.Correct(this.oldState, 0.5);
            }
        }


    }
}
