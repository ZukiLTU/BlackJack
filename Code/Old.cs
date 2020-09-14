/*
 * 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class FormBlackJack : Form
    {
        int jetons = 50;
        int scoreJ = 0;
        int scoreC = 0;
        int Soustr;
        public bool Distr = false;
        public bool Fin = false;
        string Def = "Crédits :";
        public string JoueurTX = "Joueur :";
        string CasinoTX = "Casino : ";
        public FormBlackJack()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        void InitJoueur()
        {
            for (int i = 0; i < 2; i++)
            {
                Random alea = new Random();
                int tempo = 2 + alea.Next(13);
                if (tempo == 14)
                {
                    tempo = 11;
                }

                else
                {
                    if (tempo > 10)
                    {
                        tempo = 10;
                    }
                }
                scoreJ += tempo;
                lblResJoueur.Text += tempo + "\n";
                lblJoueur.Text = JoueurTX + scoreJ;
            }
        }

        void InitCasino()
        {
            Random aleaCasino = new Random();
            for (int j = 0; j < 2; j++)
            {
                int tempoC = 2 + aleaCasino.Next(13);
                if (tempoC == 14)
                {
                    tempoC = 11;
                }

                else
                {
                    if (tempoC > 10)
                    {
                        tempoC = 10;
                    }
                }
                scoreC += tempoC;
                lblResCasino.Text += tempoC + "\n";
                lblTxCasino.Text = CasinoTX + scoreC;
            }
        }

        void Init()
        {
            InitCasino();
            InitJoueur();
            if (scoreC == 20 && scoreJ == 20)
            {
                Egalite();
            }
        }
        private void resteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Fin == true)
            {
                MessageBox.Show("Vous devez commencer une partie avant !",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            else
            {
                if (Distr == true)
                {
                    Random Decision = new Random();
                    while (scoreC < 17)
                    {
                        GenCarteCasino();
                    }
                    int chance = Decision.Next(0, 1);
                    if (chance == 0)
                    {
                        if (scoreC == scoreJ && scoreC != 21 && scoreJ != 21)
                        {
                            Egalite();
                        }

                        else if (scoreC > 21)
                        {
                            verif();
                            gg();
                        }

                        if (scoreC == 19 && scoreC <= 21)
                        {
                            return;
                        }

                        else
                        {
                            GenCarteCasino();
                        }
                    }
                    else return;
                    ConditionsReste();
                    BlackJack();
                    Exit();
                }
                else
                {
                    MessageBox.Show("Vous devez distribuer les cartes avant de piocher une carte.",
                    "Attention",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                }
            }
        }
        private void FormBlackJack_Load(object sender, EventArgs e)
        {
            lblBal.Text = Def + jetons;
            lblJoueur.Text = JoueurTX + scoreJ;
            lblTxCasino.Text = CasinoTX + scoreC;
        }

        private void distribuerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            verifBal();
            if (scoreC == 22)
            {
                scoreC = 21;
                lblTxCasino.Text = CasinoTX + scoreC;
                verifInv();
                def();
            }

            else if (scoreJ == 22)
            {
                scoreJ = 21;
                lblJoueur.Text = JoueurTX + scoreJ;
                verif();
                gg();
            }

            else return;

            BlackJack();
        }

        private void carteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Fin == true)
            {
                MessageBox.Show("Vous devez faire une nouvelle partie avant de commencer une autre.",
                "Erreur",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }

            else if (Distr == false)
            {
                MessageBox.Show("Vous devez distribuer les cartes avant de piocher une carte.",
                    "Attention",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            else
            {
                void GenCartes()
                {
                    Random carteJoueur = new Random();
                    int tempoCarteJ = 2 + carteJoueur.Next(13);
                    if (tempoCarteJ == 14)
                    {
                        tempoCarteJ = 11;
                    }

                    else
                    {
                        if (tempoCarteJ > 10)
                        {
                            tempoCarteJ = 10;
                        }

                        scoreJ += tempoCarteJ;
                        lblResJoueur.Text += tempoCarteJ + "\n";
                        lblJoueur.Text = JoueurTX + scoreJ;
                    }
                }
                GenCartes();
            }

            ConditionsCarte();
            Exit();
        }

        private void nouvellePartieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerifSiAbandon();

            radioMise10.Enabled = true;
            radioMise20.Enabled = true;
            radioMise50.Enabled = true;
            radioMise100.Enabled = true;

            if (Fin == true)
            {
                Fin = false;
            }

            else
            {
                return;
            }
        }

        //FONCTIONS
        void GenCarteCasino()
        {
            Random CarteCasino = new Random();
            int tempoCarteC = 2 + CarteCasino.Next(13);
            if (tempoCarteC == 14)
            {
                tempoCarteC = 11;
            }

            else
            {
                if (tempoCarteC > 10)
                {
                    tempoCarteC = 10;
                }

                scoreC += tempoCarteC;
                lblResCasino.Text += tempoCarteC + "\n";
                lblTxCasino.Text = CasinoTX + scoreC;
            }
        }

        void def()
        {
            MessageBox.Show("Vous avez perdu !\nVos crédits : " + jetons
                , "Perdu !",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            Distr = false;
            Fin = true;
        }

        void verifInv()
        {
            if (radioMise10.Checked)
            {
                jetons -= 10;
                lblBal.Text = Def + jetons;
                scoreC = 0;
                scoreJ = 0;
            }
            else if (radioMise20.Checked)
            {
                jetons -= 20;
                lblBal.Text = Def + jetons;
                scoreC = 0;
                scoreJ = 0;
            }
            else if (radioMise50.Checked)
            {
                jetons -= 50;
                lblBal.Text = Def + jetons;
                scoreC = 0;
                scoreJ = 0;
            }
            if (radioMise100.Checked)
            {
                jetons -= 100;
                lblBal.Text = Def + jetons;
                scoreC = 0;
                scoreJ = 0;
            }
        }

        void verif()
        {
            int moitie;
            if (radioMise10.Checked)
            {
                if (scoreJ == 21)
                {
                    moitie = 10 / 2;
                    jetons += 10 + moitie;
                    lblBal.Text = Def + jetons;
                }

                else
                {
                    jetons += 10;
                    lblBal.Text = Def + jetons;
                }
                scoreC = 0;
                scoreJ = 0;
            }
            else if (radioMise20.Checked)
            {
                if (scoreJ == 21)
                {
                    moitie = 20 / 2;
                    jetons += 20 + moitie;
                    lblBal.Text = Def + jetons;
                }

                else
                {
                    jetons += 20;
                    lblBal.Text = Def + jetons;
                }
                scoreC = 0;
                scoreJ = 0;
            }
            else if (radioMise50.Checked)
            {
                if (scoreJ == 21)
                {
                    moitie = 50 / 2;
                    jetons += 50 + moitie;
                    lblBal.Text = Def + jetons;
                }
                else
                {
                    jetons += 50;
                    lblBal.Text = Def + jetons;
                }
                scoreC = 0;
                scoreJ = 0;
            }
            if (radioMise100.Checked)
            {
                if (scoreJ == 21)
                {
                    moitie = 100 / 2;
                    jetons += 100 + moitie;
                    lblBal.Text = Def + jetons;
                }

                else
                {
                    jetons += 100;
                    lblBal.Text = Def + jetons;
                }
                scoreC = 0;
                scoreJ = 0;
            }
        }

        void verifBal()
        {
            if (Distr == true)
            {
                MessageBox.Show("Vous ne pouvez pas distribuer pendant le jeu !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                if (Fin == true)
                {
                    return;
                }

                else
                {
                    Distr = true;
                    void Aff()
                    {
                        MessageBox.Show("Vous n'avez pas assez de fonds.\nVos fonds : " + jetons, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Distr = false;
                    }

                    if (radioMise10.Checked && jetons < 10)
                    {
                        Aff();
                    }

                    else if (radioMise10.Checked)
                    {
                        radioMise20.Enabled = false;
                        radioMise50.Enabled = false;
                        radioMise100.Enabled = false;
                        Init();
                    }

                    else if (radioMise20.Checked && jetons < 20)
                    {
                        Aff();
                    }

                    else if (radioMise20.Checked)
                    {
                        radioMise10.Enabled = false;
                        radioMise50.Enabled = false;
                        radioMise100.Enabled = false;
                        Init();
                    }

                    else if (radioMise50.Checked && jetons < 50)
                    {
                        Aff();
                    }

                    else if (radioMise50.Checked)
                    {
                        radioMise10.Enabled = false;
                        radioMise20.Enabled = false;
                        radioMise100.Enabled = false;
                        Init();
                    }

                    else if (radioMise100.Checked && jetons < 100)
                    {
                        Aff();
                    }

                    else if (radioMise100.Checked)
                    {
                        radioMise10.Enabled = false;
                        radioMise20.Enabled = false;
                        radioMise50.Enabled = false;
                        Init();
                    }
                }
                if (Fin == true)
                {
                    MessageBox.Show("Vous devez faire une nouvelle partie avant de commencer une autre.",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }

                else
                {
                    return;
                }
            }
        }

        void VerifSiAbandon()
        {
            if (Distr == true)
            {
                if (radioMise10.Checked)
                {
                    Soustr = 10;
                    DialogResult warn = MessageBox.Show("Si vous quittez la partie, vous perderez 10 crédits.\nContinuer ?",
                        "Attention !",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (warn == DialogResult.Yes)
                    {
                        jetons -= Soustr;
                        lblBal.Text = Def + jetons;

                        lblResJoueur.Text = " ";
                        lblResCasino.Text = " ";

                        lblTxCasino.Text = CasinoTX + "0";
                        lblJoueur.Text = JoueurTX + "0";

                        scoreC = 0;
                        scoreJ = 0;

                        Distr = false;
                        Fin = false;
                    }

                    else
                    {
                        return;
                    }
                }
                else if (radioMise20.Checked)
                {

                    Soustr = 20;
                    DialogResult warn = MessageBox.Show("Si vous quittez la partie, vous perderez 20 crédits.\nContinuer ?",
                        "Attention !",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (warn == DialogResult.Yes)
                    {
                        jetons -= Soustr;
                        lblBal.Text = Def + jetons;

                        lblResJoueur.Text = " ";
                        lblResCasino.Text = " ";

                        scoreC = 0;
                        scoreJ = 0;

                        lblTxCasino.Text = CasinoTX + "0";
                        lblJoueur.Text = JoueurTX + "0";
                        Distr = false;
                    }

                    else
                    {
                        return;
                    }

                }
                else if (radioMise50.Checked)
                {
                    Soustr = 50;
                    DialogResult warn = MessageBox.Show("Si vous quittez la partie, vous perderez 50 crédits.\nContinuer ?",
                        "Attention !",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (warn == DialogResult.Yes)
                    {
                        jetons -= Soustr;
                        lblBal.Text = Def + jetons;

                        lblResJoueur.Text = " ";
                        lblResCasino.Text = " ";

                        scoreC = 0;
                        scoreJ = 0;

                        lblTxCasino.Text = CasinoTX + "0";
                        lblJoueur.Text = JoueurTX + "0";
                        Distr = false;
                    }

                    else
                    {
                        return;
                    }
                }
                else if (radioMise100.Checked)
                {
                    Soustr = 10;
                    DialogResult warn = MessageBox.Show("Si vous quittez la partie, vous perderez 10 crédits.\nContinuer ?",
                        "Attention !",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (warn == DialogResult.Yes)
                    {
                        jetons -= Soustr;
                        lblBal.Text = Def + jetons;

                        lblResJoueur.Text = " ";
                        lblResCasino.Text = " ";

                        scoreC = 0;
                        scoreJ = 0;

                        lblTxCasino.Text = CasinoTX + "0";
                        lblJoueur.Text = JoueurTX + "0";
                        Distr = false;
                    }

                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                lblResJoueur.Text = " ";
                lblResCasino.Text = " ";
                lblTxCasino.Text = CasinoTX + "0";
                lblJoueur.Text = JoueurTX + "0";
                scoreC = 0;
                scoreJ = 0;
            }

            if (jetons <= 0)
            {
                MessageBox.Show("Vous n'avez plus d'argent !\nAdieu ! ", "Adieu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        void gg()
        {
            MessageBox.Show("Vous avez gagné !\nVos crédits : " + jetons, "Félicitations",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            Distr = false;
            Fin = true;
        }

        void ConditionsCarte()
        {
            BlackJack();

            if (scoreJ > 21)
            {
                verifInv();
                def();
            }

        }

        void ConditionsReste()
        {
            if (scoreC > 21)
            {
                verif();
                gg();
            }

            else if (scoreJ > 21)
            {
                verif();
                def();
            }

            else if (scoreJ < scoreC)
            {
                verifInv();
                def();
            }

            else if (scoreJ > scoreC)
            {
                verif();
                gg();
            }

            else if (scoreC == scoreJ && scoreC != 21 && scoreJ != 21)
            {
                Egalite();
            }

            else
            {
                return;
            }
            BlackJack();
        }

        void Egalite()
        {
            MessageBox.Show("Egalité, personne ne gagne.",
                "Egalité",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            lblResJoueur.Text = " ";
            lblResCasino.Text = " ";

            lblTxCasino.Text = CasinoTX + "0";
            lblJoueur.Text = JoueurTX + "0";

            scoreC = 0;
            scoreJ = 0;
            Distr = false;
            Fin = true;
        }

        void Exit()
        {
            if (jetons <= 9)
            {
                MessageBox.Show("Vous n'avez plus d'argent !\nAdieu ! ", "Adieu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        void BlackJack()
        {
            if (scoreC == 21)
            {
                verifInv();
                MessageBox.Show("Vous avez perdu ! (Blackjack du Casino)\nVos crédits : " + jetons,
                    "Perdu !",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Fin = true;
            }

            else if (scoreJ == 21)
            {
                verif();
                MessageBox.Show("Vous avez gagné ! (Blackjack)\nVos crédits : " + jetons,
                    "Perdu !",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}

*/