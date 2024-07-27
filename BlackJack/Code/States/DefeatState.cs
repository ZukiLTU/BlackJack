using BlackJack.Card;
using BlackJack.Code.Actions.GameActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJack.Code.States
{
    public class DefeatState : BaseState
    {
        private Label _playerLabel;
        private Label _casinoLabel;
        private BlackjackAction _bAction = new BlackjackAction(default, default, default, default, default, default, default, default, default);
        public DefeatState(List<Cards> playerCards, List<Cards> casinoCards, List<Cards> usedCards, Label playerLabel, Label casinoLabel)
        {
            //_bAction.Restart(casinoCards, playerCards, usedCards);
            _playerLabel = playerLabel;
            _casinoLabel = casinoLabel;
        }
        public void DisplayState(int bal)
        {
            DialogResult = MessageBox.Show(
                "Vous avez perdu ! Votre argent: " + bal,
                "Perdu !",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        public void DisplayBlackjackState(int bal)
        {
            DialogResult = MessageBox.Show($"Vous avez perdu ! (Blackjack) Votre argent : ${bal}");
        }
    }
}
