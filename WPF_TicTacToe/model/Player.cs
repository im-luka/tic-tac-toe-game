using System.Windows.Media;

namespace WPF_TicTacToe.model
{
    public class Player
    {
        private string name;
        private char value;

        public string Name { get { return name; } set { name = value; } }
        public char Value { get { return value; } set { this.value = value; } }

        public SolidColorBrush Color { get; set; }

        public Player(string name, char value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
