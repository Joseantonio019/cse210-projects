using System;
using System.Collections.Generic;



class Program
{
    static void Main(string[] args)
    {

        Video video1 = new Video();
        video1._title = "Understanding Data Analytics";
        video1._author = "Michael Wolf";
        video1._length = 420;

        video1._comments.Add(new Comment { _name = "Alice", _text = "Great explanation!" });
        video1._comments.Add(new Comment { _name = "Bob", _text = "Very helpful, thanks!" });
        video1._comments.Add(new Comment { _name = "Charlie", _text = "Loved the visuals." });

        
        Video video2 = new Video();
        video2._title = "C# Classes and Abstraction";
        video2._author = "Michael Wolf";
        video2._length = 360;

        video2._comments.Add(new Comment { _name = "Diana", _text = "Clear and concise!" });
        video2._comments.Add(new Comment { _name = "Ethan", _text = "I finally understand abstraction!" });

        
        Video video3 = new Video();
        video3._title = "Data Visualization Basics";
        video3._author = "Michael Wolf";
        video3._length = 510;

        video3._comments.Add(new Comment { _name = "Fiona", _text = "Excellent tutorial!" });
        video3._comments.Add(new Comment { _name = "George", _text = "Can you make one about Power BI?" });
        video3._comments.Add(new Comment { _name = "Hannah", _text = "Very informative." });

        
        List<Video> videos = new List<Video> { video1, video2, video3 };

       
        foreach (Video video in videos)
        {
            video.Display();
        }
    }
}
