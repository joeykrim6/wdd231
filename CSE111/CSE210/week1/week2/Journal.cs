
using System;
using System.Collections.Generic;
using System.IO;

namespace JournalApp
{
    public class Journal
    {
        private List<Entry> entries = new List<Entry>();

        public void AddEntry(Entry entry)
        {
            entries.Add(entry);
        }

        public void Display()
        {
            foreach (var entry in entries)
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine(entry);
            }
            Console.WriteLine("------------------------------");
        }

        public void Save(string filename)
        {
            using (var writer = new StreamWriter(filename))
            {
                foreach (var entry in entries)
                {
                    writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
                }
            }
            Console.WriteLine($"Journal saved to {filename}");
        }

        public void Load(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"File {filename} not found.");
                return;
            }

            entries.Clear();
            var lines = File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length >= 3)
                {
                    var date = parts[0];
                    var prompt = parts[1];
                    var response = parts[2];
                    entries.Add(new Entry(date, prompt, response));
                }
            }

            Console.WriteLine($"Journal loaded from {filename}");
        }
    }
}
