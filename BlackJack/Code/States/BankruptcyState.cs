using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJack.Code.States
{
    public class BankruptcyState
    {
        public void DSisplayState()
        {
            MessageBox.Show("Adieu !",
                   "Adieu",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
            Application.Exit();
        }
    }
}
