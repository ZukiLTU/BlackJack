using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJack.Code.States
{
    public class TieState : BaseState
    {
        public void DisplayState()
        {
            DialogResult = MessageBox.Show("Egalité, personne ne gagne.",
                "Egalité",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}
