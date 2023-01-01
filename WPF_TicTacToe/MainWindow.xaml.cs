using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using WPF_TicTacToe.exceptions;
using WPF_TicTacToe.model;

namespace WPF_TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckPlayerInput();
                Player player1 = CreatePlayer(Player1Name.Text, Player1X.IsChecked == true ? Player1X.Content.ToString() : Player1O.Content.ToString());
                Player player2 = CreatePlayer(Player2Name.Text, Player2X.IsChecked == true ? Player2X.Content.ToString() : Player2O.Content.ToString());

                var gameWindow = new GameWindow(player1, player2);
                gameWindow.Show();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message, "Create Players", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CheckPlayerInput()
        {
            if(Player1Name.Text == string.Empty || Regex.IsMatch(Player1Name.Text, @"^\s+$") ||
               Player2Name.Text == string.Empty || Regex.IsMatch(Player2Name.Text, @"^\s+$"))
            {
                throw new EmptyPlayerInputException("Please enter Player names.");
            }

            if((Player1X.IsChecked == false && Player1O.IsChecked == false) ||
               (Player2X.IsChecked == false && Player2O.IsChecked == false))
            {
                throw new EmptyPlayerInputException("Please enter player values.");
            }

            if((Player1X.IsChecked == true && Player2X.IsChecked == true) ||
               (Player1O.IsChecked == true && Player2O.IsChecked == true))
            {
                throw new EmptyPlayerInputException("Players must have different values.");
            }
        }

        private Player CreatePlayer(string name, string value)
        {
            return new Player(name, value.First());
        }
    }
}
