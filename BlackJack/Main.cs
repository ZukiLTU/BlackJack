using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel.Com2Interop;
using BlackJack.Card;
using BlackJack.Code.Actions.GameActions;
using Serilog;

namespace BlackJack
{
    public partial class FormBlackJack : Form
    {
        #region valeurs
        public int PlayerSum { get; set; }
        public int CasinoSum { get; set; }
        public int Money { get; set; } = 50;
        public int bet;
        public bool Fin, Distr;
        public List<PictureBox> PlayerPictureBoxes { get; set; } = new List<PictureBox>();
        public List<PictureBox> CasinoPictureBoxes { get; set; } = new List<PictureBox>();

        #endregion

        private BlackjackAction BlackjackAction { get; set; }

        #region texte
        string Def = "Crédits :";
        public string JoueurTX = "Joueur :";
        string CasinoTX = "Casino : ";

        #endregion

        private Random random = new Random();
        List<int> UsedCards { get; set; } = new List<int>();
        List<Cards> UserCards { get; set; } = new List<Cards>();
        List<Cards> CasinoCards { get; set; } = new List<Cards>();
        #region 52Cartes

        List<Cards> allCards = new List<Cards>()
        {
            #region Coeurs //Liste des cartes de coeur

            new Cards() { Value  = 2, Name = "2 de coeur", Image = Environment.CurrentDirectory + "\\img\\2H.png"},
            new Cards() { Value  = 3, Name = "3 de coeur", Image = Environment.CurrentDirectory + "\\img\\3H.png"},
            new Cards() { Value  = 4, Name = "4 de coeur", Image =Environment.CurrentDirectory + "\\img\\4H.png"},
            new Cards() { Value  = 5, Name = "5 de coeur", Image =Environment.CurrentDirectory + "\\img\\5H.png"},
            new Cards() { Value  = 6, Name = "6 de coeur", Image =Environment.CurrentDirectory + "\\img\\6H.png"},
            new Cards() { Value  = 7, Name = "7 de coeur", Image =Environment.CurrentDirectory + "\\img\\7H.png"},
            new Cards() { Value  = 8, Name = "8 de coeur", Image =Environment.CurrentDirectory + "\\img\\8H.png"},
            new Cards() { Value  = 9, Name = "9 de coeur", Image =Environment.CurrentDirectory + "\\img\\9H.png"},
            new Cards() { Value  = 10, Name = "10 de coeur", Image =Environment.CurrentDirectory + "\\img\\10H.png"},
            new Cards() { Value  = 10, Name = "Valet de coeur", Image = Environment.CurrentDirectory +"\\img\\JH.png" },
            new Cards() { Value  = 10, Name = "Reine de coeur", Image = Environment.CurrentDirectory + "\\img\\QH.png" },
            new Cards() { Value  = 10, Name = "Roi de coeur", Image = Environment.CurrentDirectory + "\\img\\KH.png" },
            new Cards() { Value  = 11, Name = "As de coeur", Image = Environment.CurrentDirectory + "\\img\\AH.png" },

            #endregion

            #region Trefles //Liste des cartes de trèfle

            new Cards() { Value  = 2, Name = "2 de trèfle", Image  = Environment.CurrentDirectory + "\\img\\2C.png" },
            new Cards() { Value  = 3, Name = "3 de trèfle", Image = Environment.CurrentDirectory + "\\img\\3C.png" },
            new Cards() { Value  = 4, Name = "4 de trèfle", Image = Environment.CurrentDirectory + "\\img\\4C.png" },
            new Cards() { Value  = 5, Name = "5 de trèfle", Image = Environment.CurrentDirectory + "\\img\\5C.png" },
            new Cards() { Value  = 6, Name = "6 de trèfle", Image = Environment.CurrentDirectory + "\\img\\6C.png" },
            new Cards() { Value  = 7, Name = "7 de trèfle", Image = Environment.CurrentDirectory + "\\img\\7C.png" },
            new Cards() { Value  = 8, Name = "8 de trèfle", Image = Environment.CurrentDirectory +"\\img\\8C.png" },
            new Cards() { Value  = 9, Name = "9 de trèfle", Image = Environment.CurrentDirectory + "\\img\\9C.png" },
            new Cards() { Value  = 10, Name = "10 de trèfle", Image = Environment.CurrentDirectory + "\\img\\10C.png" },
            new Cards() { Value  = 10, Name = "Valet de trèfle", Image = Environment.CurrentDirectory + "\\img\\JC.png" },
            new Cards() { Value  = 10, Name = "Reine de trèfle", Image = Environment.CurrentDirectory + "\\img\\QC.png" },
            new Cards() { Value  = 10, Name = "Roi de trèfle", Image = Environment.CurrentDirectory + "\\img\\KC.png" },
            new Cards() { Value  = 11, Name = "As de trèfle", Image = Environment.CurrentDirectory + "\\img\\AC.png" },

            #endregion

            #region Carreaux //Liste des cartes de carreaux

            new Cards() { Value  = 2, Name = "2 de carreaux", Image = Environment.CurrentDirectory + "\\img\\2D.png" },
            new Cards() { Value  = 3, Name = "3 de carreaux", Image = Environment.CurrentDirectory + "\\img\\3D.png" },
            new Cards() { Value  = 4, Name = "4 de carreaux", Image = Environment.CurrentDirectory + "\\img\\4D.png" },
            new Cards() { Value  = 5, Name = "5 de carreaux", Image = Environment.CurrentDirectory + "\\img\\5D.png" },
            new Cards() { Value  = 6, Name = "6 de carreaux", Image = Environment.CurrentDirectory + "\\img\\6D.png" },
            new Cards() { Value  = 7, Name = "7 de carreaux", Image = Environment.CurrentDirectory + "\\img\\7D.png" },
            new Cards() { Value  = 7, Name = "8 de carreaux", Image = Environment.CurrentDirectory + "\\img\\8D.png" },
            new Cards() { Value  = 9, Name = "9 de carreaux", Image = Environment.CurrentDirectory + "\\img\\9D.png" },
            new Cards() { Value  = 10, Name = "10 de carreaux", Image = Environment.CurrentDirectory + "\\img\\10D.png" },
            new Cards() { Value  = 10, Name = "Valet de carreaux", Image = Environment.CurrentDirectory + "\\img\\JD.png" },
            new Cards() { Value  = 10, Name = "Reine de carreaux", Image = Environment.CurrentDirectory + "\\img\\QD.png" },
            new Cards() { Value  = 10, Name = "Roi de carreaux", Image = Environment.CurrentDirectory + "\\img\\KD.png" },
            new Cards() { Value  = 11, Name = "As de carreaux", Image = Environment.CurrentDirectory + "\\img\\AD.png" },

            #endregion

            #region Piques //Liste des cartes de piques

            new Cards() { Value  = 2, Name = "2 de piques", Image = Environment.CurrentDirectory + "\\img\\2S.png" },
            new Cards() { Value  = 3, Name = "3 de piques", Image = Environment.CurrentDirectory + "\\img\\3S.png" },
            new Cards() { Value  = 4, Name = "4 de piques", Image = Environment.CurrentDirectory + "\\img\\4S.png" },
            new Cards() { Value  = 5, Name = "5 de piques", Image = Environment.CurrentDirectory + "\\img\\5S.png" },
            new Cards() { Value  = 6, Name = "6 de piques", Image = Environment.CurrentDirectory + "\\img\\6S.png" },
            new Cards() { Value  = 7, Name = "7 de piques", Image = Environment.CurrentDirectory + "\\img\\7S.png" },
            new Cards() { Value  = 8, Name = "8 de piques", Image = Environment.CurrentDirectory + "\\img\\8S.png" },
            new Cards() { Value  = 9, Name = "9 de piques", Image = Environment.CurrentDirectory + "\\img\\9S.png" },
            new Cards() { Value  = 10, Name = "10 de piques", Image = Environment.CurrentDirectory + "\\img\\10S.png" },
            new Cards() { Value  = 10, Name = "Valet de piques", Image = Environment.CurrentDirectory + "\\img\\JS.png" },
            new Cards() { Value  = 10, Name = "Reine de piques", Image = Environment.CurrentDirectory + "\\img\\QS.png" },
            new Cards() { Value  = 10, Name = "Roi de piques", Image = Environment.CurrentDirectory + "\\img\\KS.png" },
            new Cards() { Value  = 11, Name = "As de piques", Image = Environment.CurrentDirectory + "\\img\\AD.png" },
            #endregion
        };
        #endregion

        public FormBlackJack()
        {
            InitializeComponent();
            IList<PictureBox> pictureBoxes = this.Controls.OfType<PictureBox>().ToList();
            PlayerPictureBoxes = pictureBoxes.Select(x => x).Where(w=>w.Name.StartsWith("pictureBoxJoueur")).ToList();
            CasinoPictureBoxes = pictureBoxes.Select(x => x).Where(w => w.Name.StartsWith("pictureBoxCasino")).ToList();
            BlackjackAction = new BlackjackAction(PlayerSum, CasinoSum, UserCards, CasinoCards, allCards, lblJoueur, lblTxCasino, PlayerPictureBoxes, CasinoPictureBoxes);
        }
        /*NOTES
         pos1 = 829; 107
         pos2 = 857; 123
         =      28; 16;
         */

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
                while (CasinoSum <= 17)
                {
                    int CarteAlea3 = CarteAleatoire();
                    while (UsedCards.Contains(CarteAlea3))
                    {
                        CarteAlea3 = CarteAleatoire();
                    }
                    CarteAlea3 = 1 * CarteAlea3;
                    Cards carte3 = allCards[CarteAlea3];
                    UsedCards.Add(CarteAlea3);

                    CasinoCards.Add(carte3);
                    int CarteCasino3 = CarteAleatoire();
                    Cards Carte4 = allCards[CarteCasino3];
                    UsedCards.Add(CarteCasino3);

                    if (UsedCards.Contains(CarteCasino3)) CarteCasino3 = CarteAleatoire();
                    else CarteCasino3 = 1 * CarteCasino3;

                    pictureBoxCasino1.ImageLocation = carte3.Image;
                    pictureBoxCasino1.SizeMode = PictureBoxSizeMode.StretchImage;

                    CasinoCards.Add(Carte4);
                    pictureBoxCasino2.ImageLocation = Carte4.Image;
                    AdditionCasino();
                }
                //ConditionsScore();
                BlackjackAction.ScoreConditions(default, default, default, bet);
            }
            
        }
      

        private void distribuerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Distr)
            {
                MessageBox.Show("A faire", "Klaida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
            else
            {
                DetectionCredits();
                Distr = true;
                Money -= bet;
                if (PlayerSum == 22)
                {
                    PlayerSum = 21;
                    lblJoueur.Text = JoueurTX + PlayerSum;
                    BlackJack();
                }
            }
        }

        private void carteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (Fin)
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

            else if (!Distr)
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
                PlayerSum = 0;
                int carteAlea = CarteAleatoire();
                Cards carte3 = allCards[carteAlea];
                UsedCards.Add(carteAlea);

                if (UsedCards.Contains(carteAlea)) carteAlea = CarteAleatoire();
                else carteAlea = 1 * carteAlea;


                //Nouveau Form
                pictureBoxJoueur3.Visible = true;

                UserCards.Add(carte3);
                pictureBoxJoueur3.ImageLocation = carte3.Image;
                AdditionJoueur();
            }
            BlackjackAction.DistributionConditions(bet, Money, PlayerSum, CasinoSum);
        }

        private void nouvellePartieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Distr == true)
            {
                Abandon();  
            }

            else if(Distr == false && Fin == true)
            {
                return;
            }
        }

        private void rougeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeCardColor(Properties.Resources.rouge, ActiveForm);
        }

        private void bleuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeCardColor(Properties.Resources.bleu, ActiveForm);
        }

        private void rougeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangeCardColor(Properties.Resources.red_bg, ActiveForm);
        }

        private void orangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeCardColor(Properties.Resources.orange, ActiveForm);
        }

        private void noirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeCardColor(Properties.Resources.noir, ActiveForm);
        }

        private void violetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeCardColor(Properties.Resources.violet, ActiveForm);
        }
        private void vertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeCardColor(Properties.Resources.vert, ActiveForm);
        }
    }
}

