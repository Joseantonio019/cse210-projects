using System;
using System.Collections.Generic;
using System.Linq;

class Reference
{
    public string Book { get; }
    public int Chapter { get; }
    public string Verses { get; }

    public Reference(string book, int chapter, string verses)
    {
        Book = book;
        Chapter = chapter;
        Verses = verses;
    }

    public override string ToString() => $"{Book} {Chapter}:{Verses}";
}

class Word
{
    public string Text { get; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide() => IsHidden = true;

    public string Display() => IsHidden ? new string('_', Text.Length) : Text;
}

class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _rand = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public void HideRandomWords(int count)
    {
        var visibleWords = _words.Where(w => !w.IsHidden).ToList();
        foreach (var word in visibleWords.OrderBy(x => _rand.Next()).Take(count))
            word.Hide();
    }

    public bool AllHidden() => _words.All(w => w.IsHidden);

    public void Display()
    {
        Console.WriteLine(_reference);
        Console.WriteLine(string.Join(" ", _words.Select(w => w.Display())));
    }
}

class Program
{
    static void Main()
    {
        var reference = new Reference("John", 3, "16");
        var scripture = new Scripture(reference, "For God so loved the world that he gave his only begotten Son");

        while (true)
        {
            Console.Clear();
            scripture.Display();
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit:");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit") break;
            scripture.HideRandomWords(3);

            if (scripture.AllHidden())
            {
                Console.Clear();
                scripture.Display();
                Console.WriteLine("\nAll words hidden. Program ended.");
                break;
            }
        }
    }
}
