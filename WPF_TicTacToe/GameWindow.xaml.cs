using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using WPF_TicTacToe.model;

namespace WPF_TicTacToe
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Player player1;
        private Player player2;

        private string[,] table;

        private bool Player1Turn = true;
        private string WhosTurn = $"'s turn";
        private bool IsThereAWinner = false;

        public GameWindow(Player player1Param, Player player2Param)
        {
            InitializeComponent();
            player1 = player1Param;
            player1.Color = Brushes.Blue;
            player2 = player2Param;
            player2.Color = Brushes.Red;

            SetTitle();

            tbTurn.Text = player1.Name + WhosTurn;

            table = FillArray();
        }

        private void SetTitle()
        {
            TextRange player1Range = new TextRange(tbTitle.Document.ContentEnd, tbTitle.Document.ContentEnd);
            player1Range.Text = player1.Name;
            player1Range.ApplyPropertyValue(TextElement.ForegroundProperty, player1.Color);

            TextRange textRange = new TextRange(tbTitle.Document.ContentEnd, tbTitle.Document.ContentEnd);
            textRange.Text = " is playing against ";
            textRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.White);

            TextRange player2Range = new TextRange(tbTitle.Document.ContentEnd, tbTitle.Document.ContentEnd);
            player2Range.Text = player2.Name;
            player2Range.ApplyPropertyValue(TextElement.ForegroundProperty, player2.Color);

            EditingCommands.AlignCenter.Execute(null, tbTitle);
        }

        private string[,] FillArray()
        {
            string[,] array = new string[3,3]
            {
                {"1","2","3"},
                {"4","5","6"},
                {"7","8","9" }
            };
            return array;
        }

        private void oneButton_Click(object sender, RoutedEventArgs e)
        {
            GameLogic_DoStuff(oneButton1, oneTB);
        }

        private void twoButton_Click(object sender, RoutedEventArgs e)
        {
            GameLogic_DoStuff(twoButton2, twoTB);
        }

        private void threeButton_Click(object sender, RoutedEventArgs e)
        {
            GameLogic_DoStuff(threeButton3, threeTB);
        }

        private void fourButton_Click(object sender, RoutedEventArgs e)
        {
            GameLogic_DoStuff(fourButton4, fourTB);
        }

        private void fiveButton_Click(object sender, RoutedEventArgs e)
        {
            GameLogic_DoStuff(fiveButton5, fiveTB);
        }

        private void sixButton_Click(object sender, RoutedEventArgs e)
        {
            GameLogic_DoStuff(sixButton6, sixTB);
        }

        private void sevenButton_Click(object sender, RoutedEventArgs e)
        {
            GameLogic_DoStuff(sevenButton7, sevenTB);
        }

        private void eightButton_Click(object sender, RoutedEventArgs e)
        {
            GameLogic_DoStuff(eightButton8, eightTB);
        }

        private void nineButton_Click(object sender, RoutedEventArgs e)
        {
            GameLogic_DoStuff(nineButton9, nineTB);
        }

        private void GameLogic_DoStuff(Button button, TextBlock textBlock)
        {
            if (Player1Turn)
            {
                tbTurn.Text = player2.Name + WhosTurn;
                InsertInArray(table, button.Name, player1);
                textBlock.Background = player1.Color;
                textBlock.Text = player1.Value.ToString();
                Player1Turn = false;
            }
            else
            {
                tbTurn.Text = player1.Name + WhosTurn;
                InsertInArray(table, button.Name, player2);
                textBlock.Background = player2.Color;
                textBlock.Text = player2.Value.ToString();
                Player1Turn = true;
            }

            button.Visibility = Visibility.Hidden;

            //IS PLAYER 1 WINNER ?
            if (CheckForWinner(table, player1.Value))
            {
                WeHaveAWinner(player1, player2);
            }
            //IS PLAYER 2 WINNER ? 
            if (CheckForWinner(table, player2.Value))
            {
                WeHaveAWinner(player2, player1);
            }

            if (CheckIfDraw(table, player1.Value, player2.Value) && !IsThereAWinner)
            {
                MessageBox.Show("It's draw! Well played both of you!\nBoth of you are winners!...(or losers?...) ;)",
                    "It's a draw", MessageBoxButton.OK, MessageBoxImage.Information);
                PlayAgain();
            }
        }

        private void InsertInArray(string[,] array, string buttonName, Player player)
        {
            for(int i = 0; i < array.GetLength(0); i++)
            {
                for(int j = 0; j < array.GetLength(1); j++)
                {
                    if(buttonName.Last().ToString().Equals(array[i,j]))
                    {
                        array[i, j] = player.Value.ToString();
                    }
                }
            }
        }

        private static bool CheckForWinner(string[,] array, char value)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                int counter = 0;
                if (array[i, counter].Equals(value.ToString()) && array[i, ++counter].Equals(value.ToString()) && array[i, ++counter].Equals(value.ToString()))
                {
                    return true;
                }
            }
            for (int j = 0; j < array.GetLength(1); j++)
            {
                int counter = 0;
                if (array[counter, j].Equals(value.ToString()) && array[++counter, j].Equals(value.ToString()) && array[++counter, j].Equals(value.ToString()))
                {
                    return true;
                }
            }
            int osis = 0;
            if (array[osis, osis].Equals(value.ToString()) && array[++osis, osis].Equals(value.ToString()) && array[++osis, osis].Equals(value.ToString()))
            {
                return true;
            }
            int osX = 0, osY = array.GetLength(1) - 1;
            if (array[osX, osY].Equals(value.ToString()) && array[++osX, --osY].Equals(value.ToString()) && array[++osX, --osY].Equals(value.ToString()))
            {
                return true;
            }
            return false;
        }

        private bool CheckIfDraw(string[,] array, char x, char o)
        {
            bool flag = true;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] != x.ToString() && array[i, j] != o.ToString())
                    {
                        flag = false;
                    }
                }
            }
            if (flag)
                return true;
            else
                return false;
        }

        

        private void WeHaveAWinner(Player winner, Player loser)
        {
            IsThereAWinner = true;
            MessageBox.Show(winner.Name + " won this game! CONGRATULATIONS " + winner.Name + "!!\n" + loser.Name +
                    " better luck next time ;)\n", "We have a winner", MessageBoxButton.OK, MessageBoxImage.Information);
            PlayAgain();
        }

        private void PlayAgain()
        {
            MessageBoxResult result = MessageBox.Show("Would you like to play a rematch?", "Rematch", MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MessageBoxResult newGame = MessageBox.Show("Do you want to change players?", "Choose players",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (newGame == MessageBoxResult.Yes)
                {
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }
                else
                {
                    GameWindow game = new GameWindow(player1, player2);
                    game.Show();
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }
    }
}
