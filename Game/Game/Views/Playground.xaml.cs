using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using System.Globalization;
using Plugin.Vibrate;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game
{
    /// <summary>
    /// Strana s průběhem celé hry
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Playground : ContentPage
    {
        private question currentQ; //současná otázka pro kontrolu správnosti kliknutí      
        public Item player = new Item(); //hráč se skóre
        public myTimer gameTimer;  //časovač jednotlivých otázek
        public myTimer pauseTimer; //časovač při kliknutí
        public Generators generator = new Generators();
        public Vibrate vb;
        //▶

        public int baseCount = 200; //proměná odčítaná při stopování jdné otázky
        public int maxQ = 17; //omezení počtu otázek - omezuje pouze počet správných otázek
        public bool paused = false;

        private int _count;
        public Playground()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            player.Score = 0; //přednastaví skóre
            _count = baseCount;
            vb = new Vibrate();           

            //nastavení timerů
            gameTimer = new myTimer(3);
            gameTimer.Elapsed += Play;

            pauseTimer = new myTimer(100);
            pauseTimer.Elapsed += gamePaused;

            //new question("Question text",bool, "text color", "background color");

            resetQ(); //načtě první otázku

            gameTimer.Start();

            //kontrola klinutí - herní pole změní barvu na základě odpovědi - nsáledně se spustí timer aby s změna bylůa vyditelná, přičtě/odečté se skóre
            correct.Clicked += (s, e) => {

                if (currentQ.Correct)
                {
                    correctAnsw();
                }
                else
                {
                    incorrectAnsw();
                }
            };

            incorrect.Clicked += (s, e) => {

                if (currentQ.Correct)
                {
                    incorrectAnsw();
                }
                else
                {
                    correctAnsw();
                }
            };

            //pauza hry - zobrazí menu
            pause.Clicked += (s, e) =>
            {
                if (!paused)
                {
                    gameTimer.Stop();
                    pause.Text = "▶";
                    collapse(question, false);
                    collapse(correct, false);
                    collapse(reset, true);
                    collapse(menu, true);
                    collapse(cScore, true);
                    cScore.Text = "Score: " + player.Score.ToString();
                    paused = true;
                }
                else
                {
                    gameTimer.Start();
                    pause.Text = "❚❚";
                    collapse(question, true);
                    collapse(correct, true);
                    collapse(reset, false);
                    collapse(menu, false);
                    collapse(cScore, false);
                    paused = false;
                }
            };

            //resetuje hru
            reset.Clicked += (s, e) => {
                gameTimer.Stop();
                Navigation.PushAsync(new Playground(), false);
            };
          
            //odkazuje na menu
            menu.Clicked += (s, e) => {
                gameTimer.Stop();
                Navigation.PushAsync(new MainPage(), false);
            };

        }
        
        //pokud hráč odpověděl správně
        public void correctAnsw()
        {
            gameTimer.Stop();
            collapse(correct, false);
            player.Score += 100;
            question.TextColor = Color.FromHex("#76FF03");
            plgLay.BackgroundColor = Color.FromHex("#76FF03");
            pauseTimer.Start();
        }

        //pokud hráč odpověděl špatně
        public void incorrectAnsw()
        {
            gameTimer.Stop();
            collapse(correct, false);
            player.Score -= 50;
            question.TextColor = Color.FromHex("#D50000");
            plgLay.BackgroundColor = Color.FromHex("#D50000");            
            vb.Vibration(100);
            pauseTimer.Start();
        }

        /// <summary>
        /// funkce spuštěná časovačem
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        public void Play(object o, EventArgs e)
        {
            _count--;
            timeC.FontSize += 0.25; //zvětšuje text - pouze o malý kousek pro plynulost efektu
            timeC.Text = _count.ToString();                       
            if (_count == 0)
            {
                incorrectAnsw();
            }
        }

        //obnoví odpočet, zeptá se na další otázku
        public void resetQ()
        {
            adjustDifficulty();
            _count = baseCount;
            askQ();
        }
        //načtení otázky do UI
        private void loadQuestion(question myQ)
        {
            question.Text = myQ.Question;
            question.TextColor = myQ.ColorText;
            plgLay.BackgroundColor = myQ.ColorBackground;
        }


        //po pausze znovu spustí hlavní timer
        private void gamePaused(object o, EventArgs e)
        {
            pauseTimer.Stop();
            timeC.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)); //reset velikosti textu
            //pokud bylo zobrazeno n otázek se správnou odpovědí - ukončí hru 
            if (maxQ <= 0)
            {
                gameTimer.Stop();
                Navigation.PushAsync(new Outro(player), false);
            }
            else
            {               
                gameTimer.Start();
                collapse(correct, true);
                pause.Text = "❚❚";

                resetQ();
            }

        }

        //zeptá se na náhodný typ otázky
        public void askQ()
        {
            question q = generator.loadRndQ();

            currentQ = q;
            loadQuestion(q);

            maxQ--;
        }
        /// <summary>
        /// Upraví rychlost hry v závislosti na skóre
        /// </summary>
        private void adjustDifficulty()
        {
            baseCount = (player.Score <= 0) ? 155
                      : (player.Score < 201 && player.Score > 0) ? 133
                      : (player.Score < 301) ? 101
                      : (player.Score < 401) ? 77
                      : (player.Score < 601) ? 66
                      : (player.Score < 801) ? 55
                      : 40;
        }
        //stejná funkčnost jako collapse ve WPF
        public void collapse(View elm,bool show)
        {
            elm.IsEnabled = show;
            elm.IsVisible = show;
        }

        //blokuje tlačítko zpět
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
