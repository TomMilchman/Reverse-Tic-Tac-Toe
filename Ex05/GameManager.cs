using System;
using System.Collections.Generic;
using static Ex05.Board;

namespace Ex05
{
    public class GameManager
    {
        private Board m_GameBoard;
        private readonly int r_BoardSize;
        private readonly Player r_Player1;
        private readonly Player r_Player2;
        private Player m_CurrentPlayer;

        public GameManager(Board i_GameBoard, int i_BoardSize, Player i_Player1, Player i_Player2)
        {
            m_GameBoard = i_GameBoard;
            r_BoardSize = i_BoardSize;
            r_Player1 = i_Player1;
            r_Player2 = i_Player2;
            m_CurrentPlayer = i_Player1;
        }

        public Board GameBoard 
        {
            get
            {
                return m_GameBoard;
            }
            set
            {
                m_GameBoard = value;
            }
        }

        public int BoardSize
        {
            get
            {
                return r_BoardSize;
            }
        }

        public Player Player1
        {
            get
            {
                return r_Player1;
            }
        }

        public Player Player2
        {
            get
            {
                return r_Player2;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }
            set
            {
                m_CurrentPlayer = value;
            }
        }

        public void PlayerSwap()
        {
            if (m_CurrentPlayer.Id == 1)
            {
                m_CurrentPlayer = r_Player2;
            } 
            else
            {
                m_CurrentPlayer = r_Player1;
            } 
        }

        private bool checkForLoss(eSymbol i_Symbol)
        {
            // Check rows
            for (int row = 0; row < r_BoardSize; row++)
            {
                bool hasLoss = true;

                for (int col = 0; col < r_BoardSize; col++)
                {
                    if (m_GameBoard.BoardMatrix[row, col] != i_Symbol)
                    {
                        hasLoss = false;
                        break;
                    }
                }

                if (hasLoss)
                {
                    return true;
                }
            }

            // Check columns
            for (int col = 0; col < r_BoardSize; col++)
            {
                bool hasLoss = true;

                for (int row = 0; row < r_BoardSize; row++)
                {
                    if (m_GameBoard.BoardMatrix[row, col] != i_Symbol)
                    {
                        hasLoss = false;
                        break;
                    }
                }

                if (hasLoss)
                {
                    return true;
                }
            }

            // Check diagonals
            bool diagonal1Loss = true;
            bool diagonal2Loss = true;

            for (int i = 0; i < r_BoardSize; i++)
            {
                if (m_GameBoard.BoardMatrix[i, i] != i_Symbol)
                {
                    diagonal1Loss = false;
                }
                if (m_GameBoard.BoardMatrix[i, r_BoardSize - 1 - i] != i_Symbol)
                {
                    diagonal2Loss = false;
                }
            }

            if (diagonal1Loss || diagonal2Loss)
            {
                return true;
            }

            return false;
        }

        public Player HandleWinningPlayer()
        {
            Player winningPlayer = null;

            if (checkForLoss(CurrentPlayer.Symbol))
            {
                if (CurrentPlayer.Id == 1)
                {
                    Player2.IncrementPlayerScore();
                    winningPlayer = Player2;
                }
                else
                {
                    Player1.IncrementPlayerScore();
                    winningPlayer = Player1;
                }
            }
            else if (!m_GameBoard.IsBoardFull())
            {
                CurrentPlayer.IncrementPlayerScore();
                winningPlayer = CurrentPlayer;
            }

            return winningPlayer;
        }

        public bool IsGameOver()
        {
            return (m_GameBoard.IsBoardFull() || checkForLoss(m_CurrentPlayer.Symbol));
        }

        public void ChangeCellOnBoard(int i_Row, int i_Col)
        {
            m_GameBoard.ChangeCellOnBoard(i_Row, i_Col, m_CurrentPlayer.Symbol);
        }

        public int[] MakeAIMove()
        {
            Random random = new Random();
            List<int[]> availableMoves = getAvailableMoves();

            int[] randomMove = availableMoves[random.Next(availableMoves.Count)];
            ChangeCellOnBoard(randomMove[0], randomMove[1]);
            
            return randomMove;
        }

        private List<int[]> getAvailableMoves()
        {
            List<int[]> availableMoves = new List<int[]>();

            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int col = 0; col < r_BoardSize; col++)
                {
                    if (m_GameBoard.BoardMatrix[row, col] == Board.eSymbol.Empty)
                    {
                        availableMoves.Add(new int[] { row, col });
                    }
                }
            }

            return availableMoves;
        }
    }
}
