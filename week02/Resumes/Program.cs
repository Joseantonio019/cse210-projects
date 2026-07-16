using System;



class Program
{
    static void Main(string[] args)
    {
        // First job
        Job job1 = new Job();
        job1._company = "Microsoft";
        job1._jobTitle = "Data Analyst";
        job1._startYear = 2020;
        job1._endYear = 2023;

        // Second job
        Job job2 = new Job();
        job2._company = "Google";
        job2._jobTitle = "Data Analyst";
        job2._startYear = 2023;
        job2._endYear = 2024;

        // Resume
        Resume myResume = new Resume();
        myResume._name = "Michael Wolf";
        myResume._jobs.Add(job1);
        myResume._jobs.Add(job2);

        // Display resume
        myResume.Display();
    }
}
