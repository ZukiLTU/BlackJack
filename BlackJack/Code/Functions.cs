using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using BlackJack;
using System.Linq;
using BlackJack.Card;

namespace BlackJack
{
    public partial class FormBlackJack : Form
    {
        //FONCTIONS
        //private void Reinitialiser()
        //{
        //    PlayerSum = 0;
        //    sommecasino = 0;

        //    Distr = false;
        //    Fin = true;

        //    lblJoueur.Text = JoueurTX + PlayerSum;
        //    lblTxCasino.Text = CasinoTX + sommecasino;
        //    if (pictureBoxJoueur3.Visible == true)
        //    {
        //        pictureBoxJoueur3.Visible = false;
        //    }
        //    pictureBoxCasino4.Visible = false;

        //    textBoxInt.Clear();
        //    CasinoCards.Clear();
        //    UserCards.Clear();
        //    UsedCards.Clear();

        //    Banqueroute();
        //    InitCartes();

        //}

        private void DetectionCredits()
        {
            int.TryParse(textBoxInt.Text, out bet);
            lblBal.Text = $"Crédits : {Money}";
            if (!int.TryParse(textBoxInt.Text, out bet))
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

            else if (bet > Money)
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

            else if (bet <= 0)
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
                    Money -= bet;
                }
            }
        }

        private int CarteAleatoire()
        {
            int carteAlea;
            carteAlea = random.Next(0, allCards.Count); //génère entre 52 cartes
            return carteAlea;
        }

        private void AdditionJoueur()
        {
            PlayerSum = 0;
            for (int i = 0; i < UserCards.Count; i++)
            {
                PlayerSum += UserCards[i].Value;
                lblJoueur.Text = JoueurTX + PlayerSum;
            }

            if (PlayerSum > 21)
            {
                foreach (Cards c in UserCards)
                {
                    if (c.Value == 11)
                    {
                        PlayerSum -= 10;
                        if (PlayerSum <= 21)
                        {
                            break;
                        }
                    }
                }
            }
        }

        private void AdditionCasino()
        {
            CasinoSum = 0;
            for (int i = 0; i < CasinoCards.Count; i++)
            {
                CasinoSum += CasinoCards[i].Value;
                lblTxCasino.Text = CasinoTX + CasinoSum;
            }

            if (CasinoSum > 21)
            {
                foreach (Cards c in CasinoCards)
                {
                    if (c.Value == 11)
                    {
                        CasinoSum -= 10;
                        if (CasinoSum <= 21)
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
            DialogResult warn = MessageBox.Show("Si vous recommencez, vous perdez " + bet + "crédits.",
                 "Attention",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning);

            if (warn == DialogResult.Yes)
            {
                ArgentAbandon();
                BlackjackAction.Restart(CasinoCards, UserCards, allCards);
            }

            else { return; }
        }
        private void ArgentAbandon()
        {
            Money -= bet;
            CasinoSum = 0; PlayerSum = 0;
            lblBal.Text = Def + Money;
            Fin = true;
            Distr = false;
        }
        #endregion
        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.Show();
        }

        /// <summary>
        /// Evènement lors du changement de la couleur des cartes
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="currentForm"></param>
        public static void ChangeCardColor(Bitmap bitmap, Form currentForm)
        {
            //gets all pictureboxes in the form
            var pictureboxes = currentForm.Controls.OfType<PictureBox>();
            foreach (var item in pictureboxes)
            {
                item.Image = bitmap;
            }
        }

        private void InitCartes()
        {
            Image Arriere = Properties.Resources.b1fv;
            pictureBoxJoueur.Image = Arriere;
            pictureBoxJoueur2.Image = Arriere;
            pictureBoxCasino1.Image = Arriere;
            pictureBoxCasino2.Image = Arriere;
        }

        private void Commencer()
        {
            Distr = true;
            Fin = false;

            PlayerSum = 0;
            CasinoSum = 0;

            #region init Joueur // Génération cartes Joueur
            int CarteAlea1 = CarteAleatoire();
            Cards carte1 = allCards[CarteAlea1];
            UsedCards.Add(CarteAlea1);
            int CarteAlea2 = CarteAleatoire();

            while (UsedCards.Contains(CarteAlea2))
            {
                CarteAlea2 = CarteAleatoire();
            }
            CarteAlea2 = 1 * CarteAlea2;

            Cards carte2 = allCards[CarteAlea2];
            UsedCards.Add(CarteAlea2);

            UserCards.Add(carte1);
            UserCards.Add(carte2);

            pictureBoxJoueur.ImageLocation = carte1.Image;
            pictureBoxJoueur.SizeMode = PictureBoxSizeMode.StretchImage;


            pictureBoxJoueur2.ImageLocation = carte2.Image;
            pictureBoxJoueur2.SizeMode = PictureBoxSizeMode.StretchImage;

            #endregion

            if (PlayerSum == 22)
            {
                PlayerSum = 21;
                lblJoueur.Text = JoueurTX + PlayerSum;
            }

            else if (CasinoSum == 22)
            {
                CasinoSum = 21;
                lblTxCasino.Text = CasinoTX + CasinoSum;
            }

            AdditionJoueur();
            AdditionCasino();
            ConditionsScoreDistr();
            OPArgent();
        }

        public void OPArgent()
        {
            if (PlayerSum == 21)
            {
                int moitie = bet / 2;
                Money = (bet * 2) - moitie;
            }

        }

        #region Conditions Score
        private void ConditionsScoreDistr()
        {
            int moitie = bet / 2;
            if (PlayerSum == 21)
            {
                MessageBox.Show("Vous avez gagné ! (Blackjack) ",
                    "Félicitations", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                Money = (bet * 2) + moitie;
                lblBal.Text = Def + Money;
                BlackjackAction.Restart(CasinoCards, UserCards, allCards);
            }

            else if (PlayerSum > 21)
            {
                Defaite();
            }

            else if (CasinoSum == 21 && PlayerSum != 21)
            {
                Money = -bet;
                MessageBox.Show("Vous avez perdu ! (Blackjack)");
                lblBal.Text = Def + Money;
                BlackjackAction.Restart(CasinoCards, UserCards, allCards);
            }
        }

        private void ConditionsScoreCarte()
        {
            if (PlayerSum == 21)
            {
                int moitie = bet / 2;
                MessageBox.Show("Vous avez gagné ! (Blackjack) ",
                    "Félicitations", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                Money += (bet * 2) + moitie;
                lblBal.Text = Def + Money;
                BlackjackAction.Restart(CasinoCards, UserCards, allCards);
            }
            if (PlayerSum > 21)
            {
                Defaite();
            }
        }

        void BlackJack()
        {
            int moitie = bet / 2;
            MessageBox.Show("Vous avez gagné ! (Blackjack) ",
                "Félicitations", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            Money += (bet * 2) + moitie;
            lblBal.Text = Def + Money;
            BlackjackAction.Restart(CasinoCards, UserCards, allCards);
        }

        private void ConditionsScore()
        {
            if (PlayerSum == 21)
            {
                BlackJack();
            }

            else if (CasinoSum > 21)
            {
                Victoire();
            }

            else if (CasinoSum == 21 && PlayerSum != 21)
            {
                if (Distr == false && Fin == true) return;
                else
                {
                    Defaite();
                    MessageBox.Show("Vous avez perdu ! (Blackjack)",
                        "Perdu !",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    BlackjackAction.Restart(CasinoCards, UserCards, allCards);
                }
            }

            else if (PlayerSum > CasinoSum)
            {
                Victoire();
            }

            else if (PlayerSum < CasinoSum)
            {
                Defaite();
            }

            else if (PlayerSum == CasinoSum)
            {
                Egalite();
            }


            Banqueroute();
        }
        public void Defaite()
        {
            Money -= bet;
            lblBal.Text = Def + Money;
            MessageBox.Show("Vous avez perdu ! Votre argent: " + Money,
                "Perdu !",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            BlackjackAction.Restart(CasinoCards, UserCards, allCards);
        }

        public void Victoire()
        {
            Money += bet;
            lblBal.Text = Def + Money;
            MessageBox.Show("Vous avez gagné ! Votre argent: " + Money,
                "Gagné !",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            BlackjackAction.Restart(CasinoCards, UserCards, allCards);
        }

        public void Egalite()
        {
            MessageBox.Show("Egalité, personne ne gagne.",
                "Egalité",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            BlackjackAction.Restart(CasinoCards, UserCards, allCards);
        }
        public void Banqueroute()
        {
            if (Money == 0)
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