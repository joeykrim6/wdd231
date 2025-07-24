
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScriptureMemorizer
{
    public class ScriptureReference
    {
        public string Book { get; private set; }
        public int StartChapter { get; private set; }
        public int StartVerse { get; private set; }
        public int? EndChapter { get; private set; }
        public int? EndVerse { get; private set; }

        public ScriptureReference(string book, int chapter, int verse)
        {
            Book = book;
            StartChapter = chapter;
            StartVerse = verse;
            EndChapter = null;
            EndVerse = null;
        }

        public ScriptureReference(string book, int chapter, int startVerse, int endVerse)
        {
            Book = book;
            StartChapter = chapter;
            StartVerse = startVerse;
            EndChapter = chapter;
            EndVerse = endVerse;
        }

        public ScriptureReference(string book, int startChapter, int startVerse, int endChapter, int endVerse)
        {
            Book = book;
            StartChapter = startChapter;
            StartVerse = startVerse;
            EndChapter = endChapter;
            EndVerse = endVerse;
        }

        public override string ToString()
        {
            if (EndChapter == null || EndVerse == null)
                return $"{Book} {StartChapter}:{StartVerse}";
            if (StartChapter == EndChapter)
                return $"{Book} {StartChapter}:{StartVerse}-{EndVerse}";
            return $"{Book} {StartChapter}:{StartVerse}-{EndChapter}:{EndVerse}";
        }
    }

    public class Word
    {
        public string Text { get; private set; }
        public bool IsHidden { get; private set; }

        public Word(string text)
        {
            Text = text;
            IsHidden = false;
        }

        public void Hide()
        {
            IsHidden = true;
        }

        public override string ToString()
        {
            if (!IsHidden)
                return Text;
            return new string('_', Text.Length);
        }
    }

    public class Scripture
    {
        private ScriptureReference _reference;
        private List<Word> _words;
        private Random _random = new Random();

        public Scripture(ScriptureReference reference, string text)
        {
            _reference = reference;
            _words = [.. text.Split(' ').Select(w => new Word(w))];
        }

        public bool AllHidden => _words.All(w => w.IsHidden);

        public void Display()
        {
            Console.WriteLine(_reference);
            Console.WriteLine();
            Console.WriteLine(string.Join(" ", _words.Select(w => w.ToString())));
        }
        public void HideRandomWords(int count)
        {
            var visibleWords = _words.Where(w => !w.IsHidden).ToList();
            if (!visibleWords.Any())
                return;

            for (int i = 0; i < count && visibleWords.Any(); i++)
            {
                int index = _random.Next(visibleWords.Count);
                visibleWords[index].Hide();
                visibleWords.RemoveAt(index);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nScripture Memorizer Menu:");
            Console.WriteLine("1. Give my own scripture reference");
            Console.WriteLine("2. Recieve a random scripture from the library");
            Console.Write("Choose an option (1-2): ");
            var userSelection = Console.ReadLine();
            var scripture = new Scripture(new ScriptureReference("", 0, 0), "");
            if (userSelection == "1")
            {
                Console.WriteLine();
                Console.WriteLine("Please enter a Scripture reference, ex: 2Nephi 2:1, 1 Nephi 22:22-23");
                string scriptureInput = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Please enter the scripture text");
                string textInput = Console.ReadLine();
                var sections = scriptureInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (sections.Length >= 2)
                {
                    var bookPart = string.Join(" ", sections.Take(sections.Length - 1));
                    var nums = sections.Last();
                    var chapterAndVerses = nums.Split(new[] { ':', '-' });
                    ScriptureReference reference = null;

                    if (chapterAndVerses.Length == 2)
                    {
                        var chap = int.Parse(chapterAndVerses[0]);
                        var verse = int.Parse(chapterAndVerses[1]);
                        reference = new ScriptureReference(bookPart, chap, verse);
                    }
                    else if (chapterAndVerses.Length == 3)
                    {
                        var chap = int.Parse(chapterAndVerses[0]);
                        var start = int.Parse(chapterAndVerses[1]);
                        var end = int.Parse(chapterAndVerses[2]);
                        reference = new ScriptureReference(bookPart, chap, start, end);
                    }
                    
                    if (reference != null)
                    {
                        scripture = new Scripture(reference, textInput);
                    }
                    else
                    {
                        Console.WriteLine("Scripture reference must be in the format: <string> <int> ");
                    }
                }
                else
                {
                    Console.WriteLine("Scripture reference must be in the format: <string> <int> ");
                }
            }
            else
            {
                Console.WriteLine("Getting a random Scriptrue from the library");
                var scriptures = LoadScripturesFromFile("scriptures.txt");
                var random = new Random();
                scripture = scriptures[random.Next(scriptures.Count)];
            }   

            while (true)
            {
                Console.Clear();
                scripture.Display();

                if (scripture.AllHidden)
                {
                    Console.WriteLine();
                    Console.WriteLine("All words are hidden. Closing");
                    break;
                }

                Console.WriteLine();
                Console.WriteLine("Press Enter to hide more words or type 'quit' to exit.");
                var input = Console.ReadLine()?.Trim().ToLower();
                if (input == "quit")
                    break;

                scripture.HideRandomWords(3);
            }
        }

        private static List<Scripture> LoadScripturesFromFile(string path)
        {
            var list = new List<Scripture>();
            if (!File.Exists(path))
                return list;

            foreach (var line in File.ReadAllLines(path))
            {
                var parts = line.Split('|');
                if (parts.Length < 2) continue;

                var referenceText = parts[0].Trim();
                var scriptureText = parts[1].Trim();

                var tokens = referenceText.Split(' ');
                var book = string.Join(" ", tokens.Take(tokens.Length - 1));
                var nums = tokens.Last();
                var chapterAndVerses = nums.Split(new[] { ':', '-' });
                ScriptureReference reference;
                try
                {
                    if (chapterAndVerses.Length == 2)
                    {
                        var chap = int.Parse(chapterAndVerses[0]);
                        var verse = int.Parse(chapterAndVerses[1]);
                        reference = new ScriptureReference(book, chap, verse);
                    }
                    else if (chapterAndVerses.Length == 3)
                    {
                        var chap = int.Parse(chapterAndVerses[0]);
                        var start = int.Parse(chapterAndVerses[1]);
                        var end = int.Parse(chapterAndVerses[2]);
                        reference = new ScriptureReference(book, chap, start, end);
                    }
                    else if (chapterAndVerses.Length == 4)
                    {
                        var startChap = int.Parse(chapterAndVerses[0]);
                        var startVerse = int.Parse(chapterAndVerses[1]);
                        var endChap = int.Parse(chapterAndVerses[2]);
                        var endVerse = int.Parse(chapterAndVerses[3]);
                        reference = new ScriptureReference(book, startChap, startVerse, endChap, endVerse);
                    }
                    else
                    {
                        continue;
                    }

                    list.Add(new Scripture(reference, scriptureText));
                }
                catch
                {
                }
            }
            return list;
        }
    }
}

/*
    Creativity:
    - Supports loading a library of scriptures from an external file (scriptures.txt).
    - Hides only non-hidden words each iteration for better learning.
*/
