using System.Text.RegularExpressions;

string? line;

Dictionary<string, string> mapping = [];
mapping.Add("one", "1");
mapping.Add("two", "2");
mapping.Add("three", "3");
mapping.Add("four", "4");
mapping.Add("five", "5");
mapping.Add("six", "6");
mapping.Add("seven", "7");
mapping.Add("eight", "8");
mapping.Add("nine", "9");
mapping.Add("1", "1");
mapping.Add("2", "2");
mapping.Add("3", "3");
mapping.Add("4", "4");
mapping.Add("5", "5");
mapping.Add("6", "6");
mapping.Add("7", "7");
mapping.Add("8", "8");
mapping.Add("9", "9");

string chars = "(?=(\\d|one|two|three|four|five|six|seven|eight|nine))";


Regex start = new(chars);
Regex end = new($"{chars}(?!.*{chars})");

int total = 0;

try
{
    StreamReader sr = new StreamReader("./input.txt") ?? throw new Exception();
    line = sr.ReadLine();
    while (line != null) 
    {
        string first = start.Match(line).Groups[1].Captures[0].Value;
        string last = start.Matches(line)[^1].Groups[^1].Captures[0].Value;
        int num = Int16.Parse(mapping[first] + mapping[last]);
        total += num;
        System.Console.WriteLine(num);
        line = sr.ReadLine();
    }
    System.Console.WriteLine(total);
}
catch (System.Exception)
{
    
    throw;
}