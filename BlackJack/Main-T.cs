using BlackJack.Code;
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
        #region valeurs
        int sommejoueur = 0;
        int sommecasino = 0;
        int Argent = 50;
        int Mise;
        public bool Fin, Distr;
        #endregion

        #region texte
        string Def = "Crédits :";
        public string JoueurTX = "Joueur :";
        string CasinoTX = "Casino : ";

        #endregion

        #region Couleurs
        bool noir, bleu, orange, rouge, violet, vert;
        #endregion
        Random random = new Random();

        List<int> CartesUtilisees = new List<int>();

        List<Cartes>ListeCarteJoueur = new List<Cartes>()
        {
            new Cartes() { Valeur  = 0, Nom = "null", Image = "null" }
        };

        List<Cartes> ListeCarteCasino = new List<Cartes>()
        {
            new Cartes() { Valeur  = 0, Nom = "null", Image = "null" }
        };

        #region Arriere
        List<Cartes> arriere = new List<Cartes>()
        {
            new Cartes{Valeur = 0, Nom = "Arrière", Image = "C:\\Users\\User\\Desktop\\Dev\\C#\\BlackJack\\BlackJack\\img\\cartes\\gray_back.png" }
        };
        #endregion

        #region 52Cartes

        List<Cartes> jeu = new List<Cartes>()
        {
            #region Coeurs //Liste des cartes de coeur

            new Cartes() { Valeur  = 2, Nom = "2 de coeur", Image = Environment.CurrentDirectory + "\\img\\2H.png"},
            new Cartes() { Valeur  = 3, Nom = "3 de coeur", Image = Environment.CurrentDirectory + "\\img\\3H.png"},
            new Cartes() { Valeur  = 4, Nom = "4 de coeur", Image =Environment.CurrentDirectory + "\\img\\4H.png"},
            new Cartes() { Valeur  = 5, Nom = "5 de coeur", Image =Environment.CurrentDirectory + "\\img\\5H.png"},
            new Cartes() { Valeur  = 6, Nom = "6 de coeur", Image =Environment.CurrentDirectory + "\\img\\6H.png"},
            new Cartes() { Valeur  = 7, Nom = "7 de coeur", Image =Environment.CurrentDirectory + "\\img\\7H.png"},
            new Cartes() { Valeur  = 8, Nom = "8 de coeur", Image =Environment.CurrentDirectory + "\\img\\8H.png"},
            new Cartes() { Valeur  = 9, Nom = "9 de coeur", Image =Environment.CurrentDirectory + "\\img\\9H.png"},
            new Cartes() { Valeur  = 10, Nom = "10 de coeur", Image =Environment.CurrentDirectory + "\\img\\10H.png"},
            new Cartes() { Valeur  = 10, Nom = "Valet de coeur", Image = Environment.CurrentDirectory +"\\img\\JH.png" },
            new Cartes() { Valeur  = 10, Nom = "Reine de coeur", Image = Environment.CurrentDirectory + "\\img\\QH.png" },
            new Cartes() { Valeur  = 10, Nom = "Roi de coeur", Image = Environment.CurrentDirectory + "\\img\\KH.png" },
            new Cartes() { Valeur  = 11, Nom = "As de coeur", Image = Environment.CurrentDirectory + "\\img\\AH.png" },

            #endregion

            #region Trefles //Liste des cartes de trèfle

            new Cartes() { Valeur  = 2, Nom = "2 de trèfle", Image  = Environment.CurrentDirectory + "\\img\\2C.png" },
            new Cartes() { Valeur  = 3, Nom = "3 de trèfle", Image = Environment.CurrentDirectory + "\\img\\3C.png" },
            new Cartes() { Valeur  = 4, Nom = "4 de trèfle", Image = Environment.CurrentDirectory + "\\img\\4C.png" },
            new Cartes() { Valeur  = 5, Nom = "5 de trèfle", Image = Environment.CurrentDirectory + "\\img\\5C.png" },
            new Cartes() { Valeur  = 6, Nom = "6 de trèfle", Image = Environment.CurrentDirectory + "\\img\\6C.png" },
            new Cartes() { Valeur  = 7, Nom = "7 de trèfle", Image = Environment.CurrentDirectory + "\\img\\7C.png" },
            new Cartes() { Valeur  = 8, Nom = "8 de trèfle", Image = Environment.CurrentDirectory +"\\img\\8C.png" },
            new Cartes() { Valeur  = 9, Nom = "9 de trèfle", Image = Environment.CurrentDirectory + "\\img\\9C.png" },
            new Cartes() { Valeur  = 10, Nom = "10 de trèfle", Image = Environment.CurrentDirectory + "\\img\\10C.png" },
            new Cartes() { Valeur  = 10, Nom = "Valet de trèfle", Image = Environment.CurrentDirectory + "\\img\\JC.png" },
            new Cartes() { Valeur  = 10, Nom = "Reine de trèfle", Image = Environment.CurrentDirectory + "\\img\\QC.png" },
            new Cartes() { Valeur  = 10, Nom = "Roi de trèfle", Image = Environment.CurrentDirectory + "\\img\\KC.png" },
            new Cartes() { Valeur  = 11, Nom = "As de trèfle", Image = Environment.CurrentDirectory + "\\img\\AC.png" },

            #endregion

            #region Carreaux //Liste des cartes de carreaux

            new Cartes() { Valeur  = 2, Nom = "2 de carreaux", Image = Environment.CurrentDirectory + "\\img\\2D.png" },
            new Cartes() { Valeur  = 3, Nom = "3 de carreaux", Image = Environment.CurrentDirectory + "\\img\\3D.png" },
            new Cartes() { Valeur  = 4, Nom = "4 de carreaux", Image = Environment.CurrentDirectory + "\\img\\4D.png" },
            new Cartes() { Valeur  = 5, Nom = "5 de carreaux", Image = Environment.CurrentDirectory + "\\img\\5D.png" },
            new Cartes() { Valeur  = 6, Nom = "6 de carreaux", Image = Environment.CurrentDirectory + "\\img\\6D.png" },
            new Cartes() { Valeur  = 7, Nom = "7 de carreaux", Image = Environment.CurrentDirectory + "\\img\\7D.png" },
            new Cartes() { Valeur  = 7, Nom = "8 de carreaux", Image = Environment.CurrentDirectory + "\\img\\8D.png" },
            new Cartes() { Valeur  = 9, Nom = "9 de carreaux", Image = Environment.CurrentDirectory + "\\img\\9D.png" },
            new Cartes() { Valeur  = 10, Nom = "10 de carreaux", Image = Environment.CurrentDirectory + "\\img\\10D.png" },
            new Cartes() { Valeur  = 10, Nom = "Valet de carreaux", Image = Environment.CurrentDirectory + "\\img\\JD.png" },
            new Cartes() { Valeur  = 10, Nom = "Reine de carreaux", Image = Environment.CurrentDirectory + "\\img\\QD.png" },
            new Cartes() { Valeur  = 10, Nom = "Roi de carreaux", Image = Environment.CurrentDirectory + "\\img\\KD.png" },
            new Cartes() { Valeur  = 11, Nom = "As de carreaux", Image = Environment.CurrentDirectory + "\\img\\AD.png" },

            #endregion

            #region Piques //Liste des cartes de piques

            new Cartes() { Valeur  = 2, Nom = "2 de piques", Image = Environment.CurrentDirectory + "\\img\\2S.png" },
            new Cartes() { Valeur  = 3, Nom = "3 de piques", Image = Environment.CurrentDirectory + "\\img\\3S.png" },
            new Cartes() { Valeur  = 4, Nom = "4 de piques", Image = Environment.CurrentDirectory + "\\img\\4S.png" },
            new Cartes() { Valeur  = 5, Nom = "5 de piques", Image = Environment.CurrentDirectory + "\\img\\5S.png" },
            new Cartes() { Valeur  = 6, Nom = "6 de piques", Image = Environment.CurrentDirectory + "\\img\\6S.png" },
            new Cartes() { Valeur  = 7, Nom = "7 de piques", Image = Environment.CurrentDirectory + "\\img\\7S.png" },
            new Cartes() { Valeur  = 8, Nom = "8 de piques", Image = Environment.CurrentDirectory + "\\img\\8S.png" },
            new Cartes() { Valeur  = 9, Nom = "9 de piques", Image = Environment.CurrentDirectory + "\\img\\9S.png" },
            new Cartes() { Valeur  = 10, Nom = "10 de piques", Image = Environment.CurrentDirectory + "\\img\\10S.png" },
            new Cartes() { Valeur  = 10, Nom = "Valet de piques", Image = Environment.CurrentDirectory + "\\img\\JS.png" },
            new Cartes() { Valeur  = 10, Nom = "Reine de piques", Image = Environment.CurrentDirectory + "\\img\\QS.png" },
            new Cartes() { Valeur  = 10, Nom = "Roi de piques", Image = Environment.CurrentDirectory + "\\img\\KS.png" },
            new Cartes() { Valeur  = 11, Nom = "As de piques", Image = Environment.CurrentDirectory + "\\img\\AD.png" },
            #endregion
        };
    #endregion

        public FormBlackJack()
        {
            InitializeComponent();
        }

        /*NOTES
         pos1 = 829; 107
         pos2 = 857; 123
         =      28; 16;
         */

        private void SetResource(Properties.Resources resources)
        {
            
        }

        private void resteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Fin == true)
            {
                if (Lietuviu)
                {
                    MessageBox.Show("Jūs turite pradėti žaidimą prieš tai !",
                        "Klaida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Vous devez commencer une partie avant !",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
                
            }

            else {
                //pictureBoxCasino4.Visible = true;
                while (sommecasino <= 17)
                {
                    int CarteAlea3 = CarteAleatoire();
                    while (CartesUtilisees.Contains(CarteAlea3))
                    {
                        CarteAlea3 = CarteAleatoire();
                    }
                    CarteAlea3 = 1 * CarteAlea3;
                    Cartes carte3 = jeu[CarteAlea3];
                    CartesUtilisees.Add(CarteAlea3);

                    ListeCarteCasino.Add(carte3);
                    int CarteCasino3 = CarteAleatoire();
                    Cartes Carte4 = jeu[CarteCasino3];
                    CartesUtilisees.Add(CarteCasino3);

                    if (CartesUtilisees.Contains(CarteCasino3)) CarteCasino3 = CarteAleatoire();
                    else CarteCasino3 = 1 * CarteCasino3;

                    pictureBoxCasino.ImageLocation = carte3.Image;
                    pictureBoxCasino.SizeMode = PictureBoxSizeMode.StretchImage;

                    ListeCarteCasino.Add(Carte4);
                    pictureBoxCasino2.ImageLocation = Carte4.Image;
                    AdditionCasino();
                }
                ConditionsScore();
            }
            
        }
      

        private void distribuerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetectionCredits();
            if(sommejoueur == 22)
            {
                sommejoueur = 21;
                lblJoueur.Text = JoueurTX + sommejoueur;
                BlackJack();
            }
        }

        private void carteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (Fin == true)
            {
                if (Lietuviu)
                {
                    MessageBox.Show("Jūs turite pradėti žaidimą prieš pradedant kitą žaidimą.",
                        "Klaida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                else
                {
                MessageBox.Show("Vous devez faire une nouvelle partie avant de commencer une autre.",
                   "Erreur",
                 MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                }

            }

            else if (Distr == false)
            {
                if (Lietuviu)
                {
                    MessageBox.Show("Jūs turite dalinti kortas prieš kortą įmti !",
                        "Įspėjimas",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }

                else
                {
                    MessageBox.Show("Vous devez distribuer les cartes avant de piocher une carte.",
                        "Attention",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }

            else
            {
                sommejoueur = 0;
                int carteAlea = CarteAleatoire();
                Cartes carte3 = jeu[carteAlea];
                CartesUtilisees.Add(carteAlea);

                if (CartesUtilisees.Contains(carteAlea)) carteAlea = CarteAleatoire();
                else carteAlea = 1 * carteAlea;


                //Nouveau Form
                pictureBoxJoueur3.Visible = true;

                ListeCarteJoueur.Add(carte3);
                pictureBoxJoueur3.ImageLocation = carte3.Image;
                AdditionJoueur();
            }
            ConditionsScoreCarte();
        }
        /// <summary>
        /// Commence une nouvelle partie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nouvellePartieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Distr)
            {
                Abandon();  
            }

            else if(Distr == false && Fin)
            {
                return;
            }
        }

        private void eventLog1_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {

        }

        private void bleuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            violet = false;
            vert = false;
            bleu = true;
            orange = false;
            rouge = false;
            noir = false;
            pictureBoxJoueur.Image = Properties.Resources.bleu;
            pictureBoxJoueur2.Image = Properties.Resources.bleu;
            pictureBoxJoueur3.Image = Properties.Resources.bleu;
            pictureBoxCasino.Image = Properties.Resources.bleu;
            pictureBoxCasino2.Image = Properties.Resources.bleu;
            pictureBoxCasino4.Image = Properties.Resources.bleu;
    }

        private void rougeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormBlackJack fbj = new FormBlackJack();
            Image bg = Properties.Resources.red_bg;
            fbj.BackgroundImage = bg;
        }

        private void orangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            violet = false;
            vert = false;
            orange = true;
            bleu = false;
            noir = false;
            rouge = false;
            pictureBoxJoueur.Image = Properties.Resources.orange;
            pictureBoxJoueur2.Image = Properties.Resources.orange;
            pictureBoxJoueur3.Image = Properties.Resources.orange;
            pictureBoxCasino.Image = Properties.Resources.orange;
            pictureBoxCasino2.Image = Properties.Resources.orange;
            pictureBoxCasino4.Image = Properties.Resources.orange;
        }

        private void noirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            violet = false;
            vert = false;
            noir = true;
            rouge = false;
            orange = false;
            bleu = false;
            CardActions.ChangeCardColor(Properties.Resources.noir, ActiveForm);
        }

        private void violetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            violet = true;
            vert = false;
            noir = false;
            rouge = false;
            orange = false;
            bleu = false;
            pictureBoxJoueur.Image = Properties.Resources.violet;
            pictureBoxJoueur2.Image = Properties.Resources.violet;
            pictureBoxJoueur3.Image = Properties.Resources.violet;
            pictureBoxCasino.Image = Properties.Resources.violet;
            pictureBoxCasino2.Image = Properties.Resources.violet;
            pictureBoxCasino4.Image = Properties.Resources.violet;
        }

        private void vertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            violet = false;
            vert = true;
            noir = false;
            rouge = false;
            orange = false;
            bleu = false;
            pictureBoxJoueur.Image = Properties.Resources.vert;
            pictureBoxJoueur2.Image = Properties.Resources.vert;
            pictureBoxJoueur3.Image = Properties.Resources.vert;
            pictureBoxCasino.Image = Properties.Resources.vert;
            pictureBoxCasino2.Image = Properties.Resources.vert;
            pictureBoxCasino4.Image = Properties.Resources.vert;
        }
    }
}

