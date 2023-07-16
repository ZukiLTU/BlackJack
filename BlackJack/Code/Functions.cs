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
        //FONCTIONS
        private void Reinitialiser()
        {
            sommejoueur = 0;
            sommecasino = 0;

            Distr = false;
            Fin = true;

            lblJoueur.Text = JoueurTX + sommejoueur;
            lblTxCasino.Text = CasinoTX + sommecasino;
            if (pictureBoxJoueur3.Visible == true)
            {
                pictureBoxJoueur3.Visible = false;
            }
            pictureBoxCasino4.Visible = false;

            textBoxInt.Clear();
            ListeCarteCasino.Clear();
            ListeCarteJoueur.Clear();
            CartesUtilisees.Clear();

            Banqueroute();
            InitCartes();

        }

        private void DetectionCredits()
        {
            int.TryParse(textBoxInt.Text, out Mise);
            if (!int.TryParse(textBoxInt.Text, out Mise))
            {
                if (Lietuviu)
                {
                    MessageBox.Show("Neįvedėte vertės arba ši vertė negalioja!",
                        "Klaida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                else
                {
                    MessageBox.Show("Vous n'avez pas saisi une valeur ou cette valeur n'est pas valide !",
                        "Erreur",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            else if (Mise > Argent)
            {
                if (Lietuviu)
                {
                    MessageBox.Show("Jums neužtenka pinigų !",
                        "Klaida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Vous n'avez pas assez de fonds.",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
           
            }

            else if (Mise <= 0)
            {

                if (Lietuviu)
                {
                    MessageBox.Show("Lažintis negalima mažiau nei 0.",
                        "Klaida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                else
                {
                     MessageBox.Show("Vous ne pouvez pas parier à moins de 0.",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }

               
            }

            else if (Distr == true)
            {
                if (Lietuviu)
                {
                    MessageBox.Show("Žaidimo metu negalima dalinti !",
                        "Klaida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error) ;
                }

                else
                {
                    MessageBox.Show("Vous ne pouvez pas distribuer pendant le jeu !",
                    "Erreur", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
                
            }
            else
            {
                Fin = false;
                Distr = true;

                if (Fin == true)
                {
                    if (Lietuviu)
                    {
                        MessageBox.Show("Jūs turite pradėti žaidimą prieš dalinti.",
                            "Klaida",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }

                    else
                    {
                        MessageBox.Show("Vous devez commencer une nouvelle partie avant de distribuer.",
                        "Erreur",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    }
                    
                }

                else
                {
                    Commencer();
                    AdditionJoueur();
                }
            }
        }

        private int CarteAleatoire()
        {
            int carteAlea;
            carteAlea = random.Next(0, jeu.Count); //génère entre 52 cartes
            return carteAlea;
        }

        private void AdditionJoueur()
        {
            sommejoueur = 0;
            for (int i = 0; i < ListeCarteJoueur.Count; i++)
            {
                sommejoueur += ListeCarteJoueur[i].Valeur;
                lblJoueur.Text = JoueurTX + sommejoueur;
            }

            if (sommejoueur > 21)
            {
                foreach (Cartes c in ListeCarteJoueur)
                {
                    if (c.Valeur == 11)
                    {
                        sommejoueur -= 10;
                        if (sommejoueur <= 21)
                        {
                            break;
                        }
                    }
                }
            }
        }

        private void AdditionCasino()
        {
            sommecasino = 0;
            for (int i = 0; i < ListeCarteCasino.Count; i++)
            {
                sommecasino += ListeCarteCasino[i].Valeur;
                lblTxCasino.Text = CasinoTX + sommecasino;
            }

            if (sommecasino > 21)
            {
                foreach (Cartes c in ListeCarteCasino)
                {
                    if (c.Valeur == 11)
                    {
                        sommecasino -= 10;
                        if (sommecasino <= 21)
                        {
                            break;
                        }
                    }
                }
            }
        }

        #region Abandons
        private void Abandon()
        {
            DialogResult warn = MessageBox.Show("Si vous recommencez, vous perdez " + Mise + "crédits.",
                 "Attention",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning);

            if (warn == DialogResult.Yes)
            {
                ArgentAbandon();
                Reinitialiser();
            }

            else { return; }
        }
        private void ArgentAbandon()
        {
            Argent -= Mise;
            sommecasino = 0; sommejoueur = 0;
            lblBal.Text = Def + Argent;
            Fin = true;
            Distr = false;
        }
        #endregion
        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.Show();
        }

        private void InitCartes()
        {
            //Cols();
            if (noir)
            {
                pictureBoxCasino.Image = Properties.Resources.noir;
                pictureBoxCasino2.Image = Properties.Resources.noir;
                pictureBoxJoueur.Image = Properties.Resources.noir;
                pictureBoxJoueur2.Image = Properties.Resources.noir;
            }

            else if (bleu)
            {
                pictureBoxCasino.Image = Properties.Resources.bleu;
                pictureBoxCasino2.Image = Properties.Resources.bleu;
                pictureBoxJoueur.Image = Properties.Resources.bleu;
                pictureBoxJoueur2.Image = Properties.Resources.bleu;
            }

            else if (orange)
            {
                pictureBoxCasino.Image = Properties.Resources.orange;
                pictureBoxCasino2.Image = Properties.Resources.orange;
                pictureBoxJoueur.Image = Properties.Resources.orange;
                pictureBoxJoueur2.Image = Properties.Resources.orange;
            }

            else if (rouge)
            {
                pictureBoxCasino.Image = Properties.Resources.rouge;
                pictureBoxCasino2.Image = Properties.Resources.rouge;
                pictureBoxJoueur.Image = Properties.Resources.rouge;
                pictureBoxJoueur2.Image = Properties.Resources.rouge;
            }

            else if (vert)
            {
                pictureBoxCasino.Image = Properties.Resources.vert;
                pictureBoxCasino2.Image = Properties.Resources.vert;
                pictureBoxJoueur.Image = Properties.Resources.vert;
                pictureBoxJoueur2.Image = Properties.Resources.vert;
            }

            else if (violet)
            {
                pictureBoxCasino.Image = Properties.Resources.violet;
                pictureBoxCasino2.Image = Properties.Resources.violet;
                pictureBoxJoueur.Image = Properties.Resources.violet;
                pictureBoxJoueur2.Image = Properties.Resources.violet;
            }

            else
            {
                Image Arriere = Properties.Resources.b1fv;
                pictureBoxJoueur.Image = Arriere;
                pictureBoxJoueur2.Image = Arriere;
                pictureBoxCasino.Image = Arriere;
                pictureBoxCasino2.Image = Arriere;
            }
        }

        private void Commencer()
        {
            Distr = true;
            Fin = false;

            sommejoueur = 0;
            sommecasino = 0;

            #region init Joueur // Génération cartes Joueur
            int CarteAlea1 = CarteAleatoire();
            Cartes carte1 = jeu[CarteAlea1];
            CartesUtilisees.Add(CarteAlea1);
            int CarteAlea2 = CarteAleatoire();

            while (CartesUtilisees.Contains(CarteAlea2))
            {
                CarteAlea2 = CarteAleatoire();
            }
            CarteAlea2 = 1 * CarteAlea2;

            Cartes carte2 = jeu[CarteAlea2];
            CartesUtilisees.Add(CarteAlea2);

            ListeCarteJoueur.Add(carte1);
            ListeCarteJoueur.Add(carte2);

            pictureBoxJoueur.ImageLocation = carte1.Image;
            pictureBoxJoueur.SizeMode = PictureBoxSizeMode.StretchImage;


            pictureBoxJoueur2.ImageLocation = carte2.Image;
            pictureBoxJoueur2.SizeMode = PictureBoxSizeMode.StretchImage;

            #endregion

            if (sommejoueur == 22)
            {
                sommejoueur = 21;
                lblJoueur.Text = JoueurTX + sommejoueur;
            }

            else if (sommecasino == 22)
            {
                sommecasino = 21;
                lblTxCasino.Text = CasinoTX + sommecasino;
            }

            AdditionJoueur();
            AdditionCasino();
            ConditionsScoreDistr();
            OPArgent();
        }

        public void OPArgent()
        {
            if (sommejoueur == 21)
            {
                int moitie = Mise / 2;
                Argent = (Mise * 2) - moitie;
            }

        }

        #region Conditions Score
        private void ConditionsScoreDistr()
        {
            int moitie = Mise / 2;
            if (sommejoueur == 21)
            {
                MessageBox.Show("Vous avez gagné ! (Blackjack) ",
                    "Félicitations", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                Argent = (Mise * 2) + moitie;
                lblBal.Text = Def + Argent;
                Reinitialiser();
            }

            else if (sommejoueur > 21)
            {
                Defaite();
            }

            else if (sommecasino == 21 && sommejoueur != 21)
            {
                Argent = -Mise;
                MessageBox.Show("Vous avez perdu ! (Blackjack)");
                lblBal.Text = Def + Argent;
                Reinitialiser();
            }
        }

        private void ConditionsScoreCarte()
        {
            if (sommejoueur == 21)
            {
                int moitie = Mise / 2;
                MessageBox.Show("Vous avez gagné ! (Blackjack) ",
                    "Félicitations", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                Argent += (Mise * 2) + moitie;
                lblBal.Text = Def + Argent;
                Reinitialiser();
            }
            if (sommejoueur > 21)
            {
                Defaite();
            }
        }

        void BlackJack()
        {
            int moitie = Mise / 2;
            MessageBox.Show("Vous avez gagné ! (Blackjack) ",
                "Félicitations", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            Argent += (Mise * 2) + moitie;
            lblBal.Text = Def + Argent;
            Reinitialiser();
        }

        private void ConditionsScore()
        {
            if (sommejoueur == 21)
            {
                BlackJack();
            }

            else if (sommecasino > 21)
            {
                Victoire();
            }

            else if (sommecasino == 21 && sommejoueur != 21)
            {
                if (Distr == false && Fin == true) return;
                else
                {
                    Defaite();
                    MessageBox.Show("Vous avez perdu ! (Blackjack)",
                        "Perdu !",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Reinitialiser();
                }
            }

            else if (sommejoueur > sommecasino)
            {
                Victoire();
            }

            else if (sommejoueur < sommecasino)
            {
                Defaite();
            }

            else if (sommejoueur == sommecasino)
            {
                Egalite();
            }


            Banqueroute();
        }
        public void Defaite()
        {
            Argent -= Mise;
            lblBal.Text = Def + Argent;
            MessageBox.Show("Vous avez perdu ! Votre argent: " + Argent,
                "Perdu !",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            Reinitialiser();
        }

        public void Victoire()
        {
            Argent += Mise;
            lblBal.Text = Def + Argent;
            MessageBox.Show("Vous avez gagné ! Votre argent: " + Argent,
                "Gagné !",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            Reinitialiser();
        }

        public void Egalite()
        {
            MessageBox.Show("Egalité, personne ne gagne.",
                "Egalité",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            Reinitialiser();
        }
        public void Banqueroute()
        {
            if (Argent == 0)
            {
                MessageBox.Show("Adieu !",
                    "Adieu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Close();
            }
        }
        #endregion
    }
}