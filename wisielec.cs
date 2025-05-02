using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class WordBank
{
    private string rnd_word;

    public WordBank()
    {
        Random rnd = new Random();
        var words = File.ReadAllLines("WordBank.txt");
        int word_id = rnd.Next(words.Length);
        rnd_word = words[word_id];
    }
    public string GetRandomWord()
    {
        return rnd_word;
    }
}
    public class Player
    {
        public string Word { get; private set; }
        public HashSet<char> GuessedLetters { get; private set; } = new HashSet<char>();
        public int Errors { get; private set; } = 0;
        public int MaxErrors { get; private set; } = 6;

        public Player(string word)
        {
            Word = word;
        }

        public bool Guess(char letter)
        {
            letter = char.ToLower(letter);
            if (GuessedLetters.Contains(letter))
            {
                return false;
            }

            GuessedLetters.Add(letter);

            if (!Word.Contains(letter))
            {
                Errors++;
                return false;
            }

            return true;
        }

        public string DisplayWord()
        {
            return string.Join(" ", Word.Select(c => GuessedLetters.Contains(c) ? c : '_'));
        }

        public bool HasWon()
        {
            return Word.All(c => GuessedLetters.Contains(c));
        }

        public bool HasLost()
        {
            return Errors >= MaxErrors;
        }
    }

    public class Game
    {
        private string randomWord;
        private Player player;

        public Game()
        {
            WordBank wordBank = new WordBank();
            randomWord = wordBank.GetRandomWord();
            player = new Player(randomWord);
        }

        public void DrawHangman()
        {
            string[] stages = new string[]
            {
            "",
            "\n\n\n\n\n_|_",

            "\n |\n |\n |\n |\n_|_",

            " |===|\n |\n |\n |\n_|_",

            " |===|\n |   0\n |\n |\n_|_",

            " |===|\n |   0\n |  /|\\\n |\n_|_",

            " |===|\n |   0\n |  /|\\\n |   /\\\n_|_"

            };

            Console.WriteLine(stages[player.Errors]);
        }

        public void Play()
        {
            Console.WriteLine("Witaj w grze Wisielec!\n");

            while (!player.HasWon() && !player.HasLost())
            {
                DrawHangman();
                Console.WriteLine("Słowo: " + player.DisplayWord());
                Console.WriteLine($"Błędy: {player.Errors}/{player.MaxErrors}");
                Console.Write("Podaj literę: ");
                string input = Console.ReadLine().ToLower();

                if (string.IsNullOrWhiteSpace(input) || input.Length != 1 || !char.IsLetter(input[0]))
                {
                    Console.WriteLine("Wprowadź pojedynczą literę!");
                    continue;
                }

                char letter = input[0];

                if (player.Guess(letter))
                {
                    Console.WriteLine("Dobrze!\n");
                }
                else
                {
                    Console.WriteLine("Źle!\n");
                }
            }

            DrawHangman();
            if (player.HasWon())
            {
                Console.WriteLine($"\nBrawo! Odgadłeś słowo: {randomWord}");
            }
            else
            {
                Console.WriteLine($"\nPrzegrałeś! Słowo to: {randomWord}");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Game game = new Game();
            game.Play();
        }
    }