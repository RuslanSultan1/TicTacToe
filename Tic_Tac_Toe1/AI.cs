using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Tic_Tac_Toe1
{
    class AI
    {
        private Dictionary<string, double> table;
        private const float L = 0.3f;
        public AI()
        {
            if (File.Exists("rewards.json"))
            {

                string file = File.ReadAllText("rewards.json");
                this.table = JsonConvert.DeserializeObject<Dictionary<string, double>>(file);
            }
            else
            {
                this.table = new Dictionary<string, double>();
            }
        }


        public double GetReward(char[] _state)
        {
            Game game = new Game(_state);
            string state = new String(_state);
            if (game.IsWin('x'))
            {
                return 1;
            }

            if (game.IsWin('o'))
            {
                return 0;
            }

            if (table.ContainsKey(state))
            {
                return this.table[state];
            }
            return 0.5;
        }

        public void Correct(char[] _state, double newReward)
        {

            double oldReward = this.GetReward(_state);
            string state = new string(_state);
            this.table[state] = oldReward + L * (newReward - oldReward);
        }

        public void Save()
        {
            string file = JsonConvert.SerializeObject(this.table);
            File.WriteAllText("rewards.json", file);
        }
    }
}
