using System;
using System.Windows.Forms;

namespace Ex05
{
    public partial class GameSettingsForm : Form
    {
        public GameSettingsForm()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            string player1Name = textBoxPlayer1.Text;
            string player2Name = textBoxPlayer2.Text;
            bool isPlayer2Computer = !checkBoxPlayer2.Checked;

            if (player1Name == string.Empty)
            {
                player1Name = "Player 1";
            }

            if (player2Name == string.Empty && !isPlayer2Computer)
            {
                player2Name = "Player 2";
            }

            Player player1 = new Player(1, player1Name, Board.eSymbol.X, false);
            Player player2 = new Player(2, player2Name, Board.eSymbol.O, isPlayer2Computer); 
            int boardSize = (int)numericUpDownRows.Value;
            GameForm gameForm = new GameForm(player1, player2, boardSize);
            this.Hide();
            gameForm.ShowDialog();
            this.Close();
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPlayer2.Checked)
            {
                textBoxPlayer2.Enabled = true;
                textBoxPlayer2.Text = string.Empty;
            } 
            else {
                textBoxPlayer2.Enabled = false;
                textBoxPlayer2.Text = "Computer";
            }
        }

        private void numericUpDownRows_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownColumns.Value = numericUpDownRows.Value;
        }

        private void numericUpDownColumns_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownRows.Value = numericUpDownColumns.Value;
        }
    }
}
