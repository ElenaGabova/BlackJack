using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    class BlackJack
    {
        Player   _player;
        Computer _computer;
        public Player Player { get => _player; set => _player = value; }
        public Computer Computer { get => _computer; set => _computer = value; }

        public BlackJack()
        {
            _player = new Player();
            _computer = new Computer();
        }

        public void StartGame()
        {
            InitConsole();

            while (true)
            {
                Console.WriteLine(Player.GetScoreInfo());
                Console.WriteLine(Computer.GetScoreInfo());
                Player.Pull();
                Console.Clear();
            }
        }

        private void InitConsole()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        private bool isPlayerWin()
        {
            int playerScore = Player.GetScoreInfo();

            if (playerScore > 21)
               Player.SetState(User.StateNames.Lose);
        }
    }

    public class Player : User
    {
        public override void Pull()
        {
            Console.Write("\nВведите количество очков: ");
            int number;
            bool sucess = int.TryParse(Console.ReadLine(), out number);
        
            if (sucess && number >= 1 && number < 12)
                Score += number;
            else
            { 
                if (sucess) 
                    Console.WriteLine("Некорректный ввод (Можно вводить числа от 1 до 11), нажмите любую клавишу.");
                else 
                    Console.WriteLine("Некорректный ввод (можно вводить только числа), нажмите любую клавишу.");

                Console.ReadLine();
            }
        }

        public override void Pass() { }
        public override string GetScoreInfo()
        {
            return "Счет игрока: " + Score;
        }
    }
    public class Computer : User
    {
        public override void Pull() { }
        public override void Pass() { }
        public override string GetScoreInfo()
        {
            return "Счет компьютера: " + Score;
        }
    }

    public abstract class User
    {
        public enum StateNames { Win, Lose, Repeat };

        private StateNames _playerState;

        private int _score;
        protected int Score { get => _score; set => _score = value; }
        public StateNames GetState() 
        {
            return _playerState;
        }
        public void SetState(StateNames StateName)
        {
            _playerState = StateName;
        }
        public abstract void Pull();
        public abstract void Pass();
        public abstract string GetScoreInfo();
    }
}
