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
        public bool Lietuviu = false;
        public bool Francais = true;
        private void toolStripMenuItem1_Click(object sender, EventArgs e) //Lietuvių
        {
            MessageBox.Show("Jūsų kalba nustatyta į " + toolStripMenuItem1.Text,
                "Informacija",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            Lietuviu = true;
            Francais = false;
            nouvellePartieToolStripMenuItem.Text = "Nauja partija";
            distribuerToolStripMenuItem.Text = "Dalinti";
            resteToolStripMenuItem.Text = "Baigti paskirstyti";
            paramètresToolStripMenuItem.Text = "Nustatymai";
            lblBal.Text = "Pinigai :" + Argent;
            label1.Text = "Suma :";
            lblTxCasino.Text = "Kazino : " + sommecasino;
            lblJoueur.Text = "Žaidėjas :" + sommejoueur;
            aProposToolStripMenuItem.Text = "Apie";
            cartesToolStripMenuItem.Text = "Kortą";
        }
    }
}
