using System.Windows.Forms;

namespace Ex05
{
    public class CellButton : Button
    {
        private readonly int r_Row;
        private readonly int r_Col;

        public CellButton(int i_Row, int i_Col)
        {
            r_Row = i_Row;
            r_Col = i_Col;
        }
        
        public int Row { get { return r_Row; } }

        public int Col { get { return r_Col; } }

    }
}
