using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe1
{
    public partial class Form1 : Form
    {
        Game game;
        IPlayer playerX;
        IPlayer playerO;
        String mode;
        public int currentButton;
        char personSide;
        char computerSide;
        IPlayer currentPlayer;
        AI ai;
        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("Choose game mode! Default mode is person x vs AI o.");
            enableButtons(false);
        }
        public void Form1_Load(object sender, EventArgs e)
        {
        }
        private void personVsPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = "Person vs Person";
            gameMode.Text = mode;
        }

        private void personVsAIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = "Person vs AI";
            gameMode.Text = mode;
            MessageBox.Show("Choose your side!");
        }

        private void aIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = "AI vs AI";
            gameMode.Text = mode;
        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = "Person vs AI";
            gameMode.Text = mode;
            personSide = 'x';
            computerSide = 'o';
        }

        private void oToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = "Person vs AI";
            gameMode.Text = mode;
            personSide = 'o';
            computerSide = 'x';
        }
        private void switchPlayer()
        {
            if (currentPlayer == playerX)
            {
                currentPlayer = playerO;
            }
            else
            {
                currentPlayer = playerX;
            }
        }
        private void enableButtons(bool enable)
        {
            button0.Enabled = enable;
            button1.Enabled = enable;
            button2.Enabled = enable;
            button3.Enabled = enable;
            button4.Enabled = enable;
            button5.Enabled = enable;
            button6.Enabled = enable;
            button7.Enabled = enable;
            button8.Enabled = enable;
        }
        public void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Simple Tic Tac Toe Program!","About");
        }

        public void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        public int getButtonPosition(Button b)
        {
         return Convert.ToInt32(b.Name.Substring(6));
        }
        public void performAction(int buttonPos)
        {
            switch (buttonPos)
            {
                case 0:
                    changeButton(button0);
                    break;
                case 1:
                    button0.Select();
                    changeButton(button1);
                    break;
                case 2:
                    changeButton(button2);
                    break;
                case 3:
                    changeButton(button3);
                    break;
                case 4:
                    changeButton(button4);
                    break;
                case 5:
                    changeButton(button5);
                    break;
                case 6:
                    changeButton(button6);
                    break;
                case 7:
                   changeButton(button7);
                    break;
                case 8:
                    changeButton(button8);
                    break;
            }
            
        }
        public void changeButton(Button b)
        {
            b.Text = currentPlayer.GetSide().ToString();
            b.Enabled = false;
        }
        private void StartGameButton_Click(object sender, EventArgs e)
        {
            if (mode == null)
            {
                mode = "Person vs AI";
                gameMode.Text = mode;
                personSide = 'x';
                computerSide = 'o';
            }
            enableButtons(true);
            game = new Game();
            switch (mode)
            {
                case "AI vs AI":
                    ai = new AI();
                    playerX = new AIPlayer('x', ai, this);
                    playerO = new AIPlayer('o', ai, this);
                    break;
                case "Person vs Person":
                    playerX = new Person('x', this);
                    playerO = new Person('o', this);
                    break;
                case "Person vs AI":
                    ai = new AI();
                    playerX = new Person(personSide, this);
                    playerO = new AIPlayer(computerSide, ai, this);
                    break;
            }
            currentPlayer = playerX;
            startGameButton.BackColor = Color.Red;
            startGameButton.Enabled = false;
            if (mode == "AI vs AI")
            {
                enableButtons(false);
                while (true)
                {
                    currentPlayer.MakeStep(game);
                    if (game.IsWin(currentPlayer.GetSide()))
                    {
                        currentPlayer.Win();
                        switchPlayer();
                        currentPlayer.Lose();
                        break;
                    }
                    if (game.IsDraw())
                    {
                        playerX.Draw();
                        playerO.Draw();
                        MessageBox.Show("It's draw!");
                        break;
                    }
                    switchPlayer();
               }
               ai.Save();
            }
        }

        public void buttonClick(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            currentButton = getButtonPosition(b);
            b.Text = currentPlayer.GetSide().ToString();
            b.Enabled = false;
            currentPlayer.MakeStep(game);
            if (game.IsWin(currentPlayer.GetSide()))
            {
                currentPlayer.Win();
                switchPlayer();
                currentPlayer.Lose();
                if (playerO is AIPlayer)
                {
                    ai.Save();
                }
                enableButtons(false);
                return;
            }
            if (game.IsDraw())
            {
                playerX.Draw();
                if (playerO is AIPlayer)
                {
                    playerO.Draw();
                    ai.Save();
                }
                enableButtons(false);
                return;
            }
            switchPlayer();
            if (currentPlayer is AIPlayer)
            {
                currentPlayer.MakeStep(game);
                if (game.IsWin(currentPlayer.GetSide()))
                {
                    currentPlayer.Win();
                    ai.Save();
                    enableButtons(false);
                    return;
                }
                if (game.IsDraw())
                {
                    currentPlayer.Draw();
                    ai.Save();
                    switchPlayer();
                    currentPlayer.Draw();
                    enableButtons(false);
                    return;
                }
                switchPlayer();
            }
    }
    }
}
