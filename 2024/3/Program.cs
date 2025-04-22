using System.Text.RegularExpressions;

StreamReader sr = new("../../../input.txt");
string pattern = "mul\\(([0-9]{1,3}),([0-9]{1,3})\\)";
Regex reg = new(pattern);

string enable = "do\\(\\)";
Regex en = new(enable);

string disable = "don't\\(\\)";
Regex dis = new(disable);

int total = 0;
bool enabled = true;

string? line = sr.ReadLine();

while (line != null) {
    if (enabled)
    {
        Match disableMatch = dis.Match(line);
        if (disableMatch.Success) 
        {
            System.Console.WriteLine(1);
            System.Console.WriteLine(line);
            DoMultiplications(line[..disableMatch.Index]);
            line = line[(disableMatch.Index + disableMatch.Length)..];
            enabled = false;
            continue;
        } 
        else
        {
            System.Console.WriteLine(2);
            DoMultiplications(line);
        }
    }
    else
    {
        Match enableMatch = en.Match(line);
        if (enableMatch.Success)
        {
            System.Console.WriteLine(3);
            line = line[(enableMatch.Index + enableMatch.Length)..];
            enabled = true;
            continue;
        }
    }
    
    System.Console.WriteLine(4);
    line = sr.ReadLine();
}

void DoMultiplications(string instructions)
{
    MatchCollection matches = reg.Matches(instructions);
    foreach (Match match in matches)
    {
        total += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
    }
}

Console.WriteLine(total);
