using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class FormBlackJack : Form
    {
        private void FormBlackJack_Load(object sender, EventArgs e)
        {
            lblBal.Text = Def + Argent;
            lblJoueur.Text = JoueurTX + sommejoueur;
            lblTxCasino.Text = CasinoTX + sommecasino;
            InitCartes();

            Fin = true;
            Distr = false;

        }
    }
}