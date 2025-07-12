
using System;
using System.Collections.Generic;

namespace JournalApp
{
    public static class PromptGenerator
    {
        private static readonly List<string> prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What’s one thing I learned today?",
            "What made me smile today?",
            "What challenge did I overcome today?"
        };
        private static readonly Random random = new Random();

        public static string GetRandomPrompt()
        {
            int index = random.Next(prompts.Count);
            return prompts[index];
        }
    }
}
