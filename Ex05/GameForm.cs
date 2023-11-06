using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05
{
    public partial class GameForm : Form
    {
        private readonly Player r_Player1;
        private readonly Player r_Player2;  
        private readonly int r_BoardSize;
        private readonly Dictionary<Tuple<int, int>, CellButton> r_CellButtons;
        private readonly GameManager r_Game;

        public GameForm(Player i_Player1, Player i_Player2, int i_BoardSize)
        {
            InitializeComponent();
            r_Player1 = i_Player1;
            r_Player2 = i_Player2;
            r_BoardSize = i_BoardSize;
            r_CellButtons = new Dictionary<Tuple<int, int>, CellButton>();

            Board gameBoard = new Board(i_BoardSize);
            r_Game = new GameManager(gameBoard, i_BoardSize, i_Player1, i_Player2);

            labelPlayer1NameAndScore.Text = $"{r_Player1.Name}: {r_Player1.Score}";
            labelPlayer1NameAndScore.Font =
                        new Font(labelPlayer1NameAndScore.Font, FontStyle.Bold);
            labelPlayer2NameAndScore.Text = $"{r_Player2.Name}: {r_Player2.Score}";

            initializeButtons();
        }

        private void initializeButtons()
        {
            int buttonSize = 70; 
            int spacing = 8; 
            int windowWidth = r_BoardSize * buttonSize + (r_BoardSize + 1) * spacing;
            int windowHeight = r_BoardSize * buttonSize + (r_BoardSize + 2) * spacing + 35;

            this.ClientSize = new Size(windowWidth, windowHeight);
            labelPlayer1NameAndScore.Top = windowHeight - 30;
            labelPlayer2NameAndScore.Top = windowHeight - 30;
            labelPlayer1NameAndScore.Left = windowWidth / 2 - 70;
            labelPlayer2NameAndScore.Left = windowWidth / 2;
            
            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int column = 0; column < r_BoardSize; column++)
                {
                    CellButton cellButton = new CellButton(row, column);
                    cellButton.Click += CellButton_Click;
                    cellButton.Size = new Size(buttonSize, buttonSize);
                    cellButton.Location = new Point(spacing + column * (buttonSize + spacing), spacing + row * (buttonSize + spacing));
                    cellButton.TabStop = false;
                    r_CellButtons.Add(Tuple.Create(row, column), cellButton);
                    Controls.Add(cellButton);
                }
            }
        }

        private void CellButton_Click(object sender, EventArgs e)
        {
            handleChosenButton(sender as CellButton);

            if (r_Game.CurrentPlayer.IsComputer)
            {
                makeAIMove();
            }
        }

        private void checkAndHandleWin()
        {
            if (r_Game.IsGameOver())
            {
                Player winningPlayer = r_Game.HandleWinningPlayer();
                labelPlayer1NameAndScore.Text = $"{r_Player1.Name}: {r_Player1.Score}";
                labelPlayer2NameAndScore.Text = $"{r_Player2.Name}: {r_Player2.Score}";
                DialogResult result;

                if (winningPlayer != null)
                {
                    result = MessageBox.Show($@"The winner is {winningPlayer.Name}!
Would you like to play another round?", 
                             "A Win!",
                             MessageBoxButtons.YesNo);
                }
                else
                {
                    result = MessageBox.Show($@"A tie!
Would you like to play another round?",
                            "A Tie!",
                            MessageBoxButtons.YesNo);
                }

                switch (result)
                {
                    case DialogResult.Yes:
                        playAgain();
                        break;
                    case DialogResult.No:
                        Close();
                        break;
                }
            } 
            else
            {
                r_Game.PlayerSwap();

                if (r_Game.CurrentPlayer.Id == 1)
                {
                    switchPlayer1LabelToBold();
                }
                else
                {
                    switchPlayer2LabelToBold();
                }
            }
        }

        private void playAgain()
        {
            r_Game.GameBoard = new Board(r_BoardSize);

            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int column = 0; column < r_BoardSize; column++)
                {
                    CellButton button = r_CellButtons[Tuple.Create(row, column)];
                    button.BackColor = Color.Transparent;
                    button.Text = String.Empty;
                    button.Enabled = true;
                }
            }

            r_Game.CurrentPlayer = r_Player1;
            switchPlayer1LabelToBold();
        }

        private void makeAIMove()
        {
            int[] chosenAIMove = r_Game.MakeAIMove();
            int row = chosenAIMove[0];
            int col = chosenAIMove[1];
            CellButton cellButton = r_CellButtons[Tuple.Create(row, col)];

            handleChosenButton(cellButton);
        }

        private void handleChosenButton (CellButton button)
        {
            button.Enabled = false;
            button.BackColor = Color.White;
            button.Text = convertSymbolToString(r_Game.CurrentPlayer.Symbol);
            r_Game.ChangeCellOnBoard(button.Row, button.Col);
            checkAndHandleWin();
        }

        private string convertSymbolToString(Board.eSymbol i_Symbol)
        {
            string convertedSymbol = "";

            switch (i_Symbol)
            {
                case Board.eSymbol.X:
                    convertedSymbol = "X";
                    break;
                case Board.eSymbol.O:
                    convertedSymbol = "O";
                    break;
            }

            return convertedSymbol;
        }

        private void switchPlayer1LabelToBold()
        {
            labelPlayer1NameAndScore.Font = new Font(labelPlayer1NameAndScore.Font, FontStyle.Bold);
            labelPlayer2NameAndScore.Font = new Font(labelPlayer2NameAndScore.Font, FontStyle.Regular);
        }

        private void switchPlayer2LabelToBold()
        {
            labelPlayer2NameAndScore.Font = new Font(labelPlayer2NameAndScore.Font, FontStyle.Bold);
            labelPlayer1NameAndScore.Font = new Font(labelPlayer1NameAndScore.Font, FontStyle.Regular);
        }
    }
}
