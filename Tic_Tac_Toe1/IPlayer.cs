using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe1
{
    interface IPlayer
    {
        char GetSide();
        void MakeStep(Game game);
        void Lose();
        void Win();
        void Draw();
    }
}
