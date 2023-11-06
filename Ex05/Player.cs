namespace Ex05
{
    public class Player
    {
        private readonly int r_Id;
        private readonly string r_Name;
        private readonly Board.eSymbol r_Symbol;
        private readonly bool r_IsComputer;
        private int m_Score;

        public Player(int i_Id, string i_Name ,Board.eSymbol i_Symbol, bool i_IsComputer)
        {
            r_Id = i_Id;
            r_Name = i_Name;
            m_Score = 0;
            r_Symbol = i_Symbol;
            r_IsComputer = i_IsComputer;
        }

        public void IncrementPlayerScore()
        {
            m_Score++;
        }

        public Board.eSymbol Symbol 
        { 
            get
            {
                return r_Symbol;
            }
        }

        public string Name { get { return r_Name; } }

        public int Id
        {
            get
            {
                return r_Id;
            }
        }

        public int Score
        {
            get
            {
                return m_Score;
            }
        }

        public bool IsComputer
        {
            get
            {
                return r_IsComputer;
            }
        }
    }
}
