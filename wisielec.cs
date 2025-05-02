using System;
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

    public void WyswietlSlowo()
    {
        Console.WriteLine($"Słowo to: {rnd_word}");
    }
}

class Program
{
    static void Main()
    {
        WordBank wordBank = new WordBank();
        wordBank.WyswietlSlowo();
    }
}
