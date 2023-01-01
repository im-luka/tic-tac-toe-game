using System;

namespace WPF_TicTacToe.exceptions
{
    class EmptyPlayerInputException : Exception
    {
        public EmptyPlayerInputException() { }

        public EmptyPlayerInputException(string message) : base(message) { }

        public EmptyPlayerInputException(string message, Exception exception) : base(message, exception) { }

    }
}
