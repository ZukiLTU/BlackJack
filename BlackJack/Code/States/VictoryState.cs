using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJack.Code.States
{
    public class VictoryState : BaseState
    {
        public void DisplayState(int bal)
        {
            DialogResult = MessageBox.Show(
                $"Vous avez gagné ! (Blackjack) Votre argent : ${bal}",
                "Félicitations", 
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
        }
        public void DisplayBlackjackState(int bal)
        {
            DialogResult = MessageBox.Show($"Vous avez gagné ! (Blackjack) Votre argent : ${bal}",
                "Félicitations", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
        }
    }
}
