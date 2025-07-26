using System;
using System.Collections.Generic;

class Comment
{
    public string User { get; set; }
    public string Text { get; set; }

    public Comment(string user, string text)
    {
        User = user;
        Text = text;
    }
}

class Video
{
    public string Title { get; set; }
    public string User { get; set; }
    public int LengthSeconds { get; set; }
    private List<Comment> comments = new List<Comment>();

    public Video(string title, string user, int lengthSeconds)
    {
        Title = title;
        User = user;
        LengthSeconds = lengthSeconds;
    }

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return comments.Count;
    }

    public IEnumerable<Comment> GetComments()
    {
        return comments;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var videos = new List<Video>();

        var video1 = new Video("Monkeys in Space", "John Doe", 300);
        video1.AddComment(new Comment("Darth Vader", "I would slay those apes"));
        video1.AddComment(new Comment("Einstein", "I would like to travel to space with monkeys"));
        video1.AddComment(new Comment("Issac Newton", "GRAVITY!!"));
        videos.Add(video1);

        var video2 = new Video("How to Program", "Jane Smith", 600);
        video2.AddComment(new Comment("Bill Gates", "I love this series."));
        video2.AddComment(new Comment("Steve Jobs", "Please make more videos."));
        video2.AddComment(new Comment("HackerMan", "Now I will get into everything!"));
        videos.Add(video2);

        var video3 = new Video("The Perfect Fried Egg", "Gordan Ramsay", 900);
        video3.AddComment(new Comment("ExpertChef9000", "I could do this better."));
        video3.AddComment(new Comment("Guy Fieri", "I'm surpised you didn't swear"));
        video3.AddComment(new Comment("Bobby Flay", "Where's the butter?"));
        video3.AddComment(new Comment("OldMan", "I remember eating these daily."));
        videos.Add(video3);

        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"User: {video.User}");
            Console.WriteLine($"Length (seconds): {video.LengthSeconds}");
            Console.WriteLine($"Comments ({video.GetCommentCount()}):");
            foreach (var comment in video.GetComments())
            {
                Console.WriteLine($"\t{comment.User}: {comment.Text}");
            }
            Console.WriteLine(new string('-', 40));
        }
    }
}