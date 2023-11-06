namespace Ex05
{
    public class Board
    {
        public enum eSymbol
        {
            X,
            O,
            Empty
        }

        private readonly eSymbol[,] r_BoardMatrix;
        private readonly int r_BoardSize;

        public Board(int i_BoardSize)
        { 
            r_BoardMatrix = new eSymbol[i_BoardSize, i_BoardSize];
            r_BoardSize = i_BoardSize;

            for (int row = 0; row < i_BoardSize; row++)
            {
                for (int col = 0; col < i_BoardSize; col++)
                {
                    r_BoardMatrix[row, col] = eSymbol.Empty;
                }
            }
        }

        public void ChangeCellOnBoard(int i_Row, int i_Col, eSymbol i_Symbol)
        {
            r_BoardMatrix[i_Row, i_Col]= i_Symbol; 
        }

        public bool IsCellOccupied(int i_Row, int i_Col)
        {
            return r_BoardMatrix[i_Row, i_Col] != eSymbol.Empty;
        }

        public bool IsBoardFull()
        {
            bool isFull = true;

            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int col = 0; col < r_BoardSize; col++)
                {
                    if (!IsCellOccupied(row, col))
                    {
                        isFull = false;
                        break;
                    }
                }
                if (!isFull)
                {
                    break;
                }
            }

            return isFull;
        }

        public eSymbol[,] BoardMatrix
        {
            get
            {
                return r_BoardMatrix;
            }
        }
    }
}
