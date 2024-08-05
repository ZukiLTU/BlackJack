using BlackJack.Card;
using BlackJack.Code.States;
using BlackJack.Properties;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJack.Code.Actions.GameActions
{
    public class BlackjackAction
    {
        public int PlayerSum { get; set; }
        public int CasinoSum { get; set; }

        private List<Cards> _playerCards;
        private List<Cards> _casinoCards;
        private List<Cards> _allCards;

        private List<PictureBox> _playerPictureBoxes;   
        private List<PictureBox> _casinoPictureBoxes;

        private Label _playerLabel;
        private Label _casinoLabel;

        public string PlayerText => $"Joueur : {PlayerSum}";
        public string CasinoText => $"Casino : {CasinoSum}";

        private LoggerConfiguration logConf = new LoggerConfiguration();
        private Logger logger;    
        public BlackjackAction(int playerSum, int casinoSum, List<Cards> playerCards, List<Cards> casinoCards, List<Cards> allCards, Label playerLabel, Label casinoLabel, List<PictureBox> p_pictureBoxes, List<PictureBox> c_pictureBoxes)
        {
            PlayerSum = playerSum;
            CasinoSum = casinoSum;
            _playerCards = playerCards;
            _casinoCards = casinoCards;
            _allCards = allCards;
            _casinoLabel = casinoLabel;
            _playerLabel = playerLabel; 
            _playerPictureBoxes = p_pictureBoxes;
            _casinoPictureBoxes = c_pictureBoxes;
            logger = logConf.WriteTo.Console().CreateLogger();
        }

        public void Restart(List<Cards> casinoCards, List<Cards> userCards, List<Cards> usedCards)
        {
            try
            {
                PlayerSum = 0;
                CasinoSum = 0;

                //Distr = false;
                //Fin = true;

                _playerLabel.Text = PlayerText + PlayerSum;
                _casinoLabel.Text = CasinoText + CasinoSum;
                //pictureBoxJoueur3.Visible = false;
                //pictureBoxCasino4.Visible = false;

                //textBoxInt.Clear();
                _casinoCards.Clear();
                _playerCards.Clear();
                //_usedCards.Clear();

                //Banqueroute();
                //InitCartes();
                if(_playerPictureBoxes.Count > 2)
                    _playerPictureBoxes.Remove(_playerPictureBoxes.Last());
                foreach (var item in _playerPictureBoxes)
                {
                    item.BackgroundImage = Resources.b1fv;
                }
                if(_casinoPictureBoxes.Count > 2)
                    _casinoPictureBoxes.Remove(_casinoPictureBoxes.Last());
                foreach (var item in _casinoPictureBoxes)
                {
                    item.BackgroundImage = Resources.b1fv;
                }
            }    
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int RandomCard(List<Cards> allCards)
        {
            Random random = new Random();
            return random.Next(0, allCards.Count); //génère entre 52 cartes;
        }
        #region  --INCREMENTATION--
        public void PlayerIncrementation(List<Cards> userCards)
        {
            CasinoSum = 0;
            for (int i = 0; i < userCards.Count; i++)
            {
                PlayerSum += userCards[i].Value;
                //lblJoueur.Text = JoueurTX + PlayerSum;
            }

            //première distribution
            if (PlayerSum > 21)
            {
                foreach (Cards c in userCards)
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
        public void CasinoIncrementation(List<Cards> casinoCards)
        {
            CasinoSum = 0;
            for (int i = 0; i < casinoCards.Count; i++)
            {
                CasinoSum += casinoCards[i].Value;
                //lblTxCasino.Text = CasinoTX + sommecasino;
            }

            if (CasinoSum > 21)
            {
                foreach (Cards c in casinoCards)
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
        #endregion
        public void Abandon(int bet, int money)
        {
            DialogResult warn = MessageBox.Show($"Si vous recommencez, vous perdez {bet} crédits.",
                 "Attention",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning);

            if (warn == DialogResult.Yes)
            {
                money -= bet;
                CasinoSum = 0; 
                PlayerSum = 0;
                //lblBal.Text = Def + Argent;
                //Fin = true;
                //Distr = false;
                Restart(default, default, default);
            }

            else return;
        }
        public void Start(List<Cards> allCards, List<Cards> usedCards, List<Cards> userCards, PictureBox pP1, PictureBox pP2, int bal, int bet)
        {
            //Distr = true;
            //Fin = false;

            //PlayerSum = 0;
            //CasinoSum = 0;

            #region init Joueur // Génération cartes Joueur
            int CarteAlea1 = RandomCard(allCards);
            Cards carte1 = allCards[CarteAlea1];
            usedCards.Add(carte1);
            int CarteAlea2 = RandomCard(allCards);

            while (usedCards.Contains(carte1))
            {
                CarteAlea2 = RandomCard(allCards);
            }
            CarteAlea2 = 1 * CarteAlea2;

            Cards carte2 = allCards[CarteAlea2];
            usedCards.Add(carte1);

            userCards.Add(carte1);
            userCards.Add(carte2);

            pP1.ImageLocation = carte1.Image;
            pP1.SizeMode = PictureBoxSizeMode.StretchImage;

            pP2.ImageLocation = carte2.Image;
            pP2.SizeMode = PictureBoxSizeMode.StretchImage;
            
            #endregion

            if (PlayerSum == 22)
            {
                PlayerSum = 21;
                //lblJoueur.Text = JoueurTX + PlayerSum;
            }

            else if (CasinoSum == 22)
            {
                CasinoSum = 21;
                //lblTxCasino.Text = CasinoTX + sommecasino;
            }

            PlayerIncrementation(userCards);
            CasinoIncrementation(userCards);
            DistributionConditions(bet, bal, PlayerSum, CasinoSum);
            OPArgent(bet,bal);
        }
        public void OPArgent(int bet, int balance)
        {
            if (PlayerSum == 21)
            {
                int moitie = bet / 2;
                balance = (bet * 2) - moitie;
            }

        }

        public void DistributionConditions(int bet, int bal, int pSum, int cSum)
        {
            int moitie = bet / 2;
            if (pSum == 21)
            {
                MessageBox.Show("Vous avez gagné ! (Blackjack) ",
                    "Félicitations", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                bal = (bet * 2) + moitie;
                //lblBal.Text = Def + Argent;
                Restart(default,default,default);
            }

            else if (pSum > 21)
            {
                new DefeatState(_playerCards, _casinoCards, _allCards, _playerLabel, _casinoLabel).DisplayState(bal);
                Restart(default, default, default);
            }

            else if (cSum == 21 && pSum != 21)
            {
                new DefeatState(_playerCards, _casinoCards, _allCards, _playerLabel, _casinoLabel).DisplayBlackjackState(bal);
                //lblBal.Text = Def + Argent;
                Restart(default, default, default);
            }
        }
        public void CardConditions(int bet, int bal)
        {
            if (PlayerSum == 21)
            {
                int moitie = bet / 2;
                new VictoryState().DisplayState(bal);
                bal += (bet * 2) + moitie;
                //lblBal.Text = Def + Argent;
                Restart(default, default, default);
            }
            if (PlayerSum > 21)
            {
                new DefeatState(_playerCards, _casinoCards, _allCards, _playerLabel, _casinoLabel).DisplayBlackjackState(bal);;
            }
        }
        public void ScoreConditions(int bal, bool distributed, bool end, int bet)
        {
            if (PlayerSum == 21)
                new VictoryState().DisplayBlackjackState(bal);

            else if (CasinoSum > 21)
                new VictoryState().DisplayState(bal);

            else if (CasinoSum == 21 && CasinoSum != 21)
            {
                if (!distributed && end) return;
                else
                {
                    new DefeatState(_playerCards, _casinoCards, _allCards, _playerLabel, _casinoLabel).DisplayBlackjackState(bal);
                }
            }

            else if (PlayerSum > CasinoSum)
                new VictoryState().DisplayState(bal);

            else if (PlayerSum < CasinoSum)
                new DefeatState(_playerCards, _casinoCards, _allCards, _playerLabel, _casinoLabel).DisplayState(bal);

            else if (PlayerSum == CasinoSum)
            {
                new TieState().DisplayState();
            }

            else new BankruptcyState();
        }
    }
}
