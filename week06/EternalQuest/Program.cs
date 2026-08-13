using System;
using System.Collections.Generic;
using System.IO;

// Base class
public abstract class Goal
{
    protected string _name;
    protected int _points;
    protected bool _isComplete;

    public Goal(string name, int points)
    {
        _name = name;
        _points = points;
        _isComplete = false;
    }

    public abstract int RecordEvent();
    public abstract string GetStatus();
    public abstract string GetStringRepresentation();
}

// Simple goal
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points) { }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return _points;
        }
        return 0;
    }

    public override string GetStatus() => _isComplete ? "[X]" : "[ ]";

    public override string GetStringRepresentation() => $"SimpleGoal:{_name},{_points},{_isComplete}";
}

// Eternal goal
public class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override int RecordEvent() => _points;

    public override string GetStatus() => "[∞]";

    public override string GetStringRepresentation() => $"EternalGoal:{_name},{_points}";
}

// Checklist goal
public class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonus;

    public ChecklistGoal(string name, int points, int targetCount, int bonus)
        : base(name, points)
    {
        _targetCount = targetCount;
        _bonus = bonus;
        _currentCount = 0;
    }

    public override int RecordEvent()
    {
        _currentCount++;
        if (_currentCount >= _targetCount)
        {
            _isComplete = true;
            return _points + _bonus;
        }
        return _points;
    }

    public override string GetStatus() => $"Completed {_currentCount}/{_targetCount}";

    public override string GetStringRepresentation() =>
        $"ChecklistGoal:{_name},{_points},{_targetCount},{_bonus},{_currentCount},{_isComplete}";
}

// Main program class
public class EternalQuestProgram
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void CreateGoal()
    {
        Console.WriteLine("Choose goal type: 1) Simple  2) Eternal  3) Checklist");
        int choice = int.Parse(Console.ReadLine());

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter points: ");
        int points = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                _goals.Add(new SimpleGoal(name, points));
                break;
            case 2:
                _goals.Add(new EternalGoal(name, points));
                break;
            case 3:
                Console.Write("Enter required times to complete: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus points: ");
                int bonus = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal(name, points, target, bonus));
                break;
        }
    }

    public void RecordEvent()
    {
        ShowGoals();
        Console.Write("Select the number of the goal you accomplished: ");
        int index = int.Parse(Console.ReadLine()) - 1;
        _score += _goals[index].RecordEvent();
        Console.WriteLine($"Event recorded! Total score: {_score}");
    }

    public void ShowGoals()
    {
        Console.WriteLine("\n--- Goals List ---");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetStatus()} {_goals[i].GetStringRepresentation()}");
        }
    }

    public void SaveGoals()
    {
        using (StreamWriter outputFile = new StreamWriter("goals.txt"))
        {
            outputFile.WriteLine(_score);
            foreach (Goal g in _goals)
                outputFile.WriteLine(g.GetStringRepresentation());
        }
    }

    public void LoadGoals()
    {
        string[] lines = File.ReadAllLines("goals.txt");
        _score = int.Parse(lines[0]);
        _goals.Clear();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(':');
            string type = parts[0];
            string[] data = parts[1].Split(',');

            switch (type)
            {
                case "SimpleGoal":
                    _goals.Add(new SimpleGoal(data[0], int.Parse(data[1])));
                    break;
                case "EternalGoal":
                    _goals.Add(new EternalGoal(data[0], int.Parse(data[1])));
                    break;
                case "ChecklistGoal":
                    _goals.Add(new ChecklistGoal(data[0], int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3])));
                    break;
            }
        }
    }

    public void DisplayScore() => Console.WriteLine($"Current score: {_score}");
}

// Entry point
class Program
{
    static void Main()
    {
        EternalQuestProgram eq = new EternalQuestProgram();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n1. Create goal\n2. Record event\n3. Show goals\n4. Save goals\n5. Load goals\n6. Display score\n7. Exit");
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1: eq.CreateGoal(); break;
                case 2: eq.RecordEvent(); break;
                case 3: eq.ShowGoals(); break;
                case 4: eq.SaveGoals(); break;
                case 5: eq.LoadGoals(); break;
                case 6: eq.DisplayScore(); break;
                case 7: running = false; break;
            }
        }
    }
}
