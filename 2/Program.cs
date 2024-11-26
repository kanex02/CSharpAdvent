using System.Text.RegularExpressions;

Regex idRegex = new("^Game (\\d+):");
Regex blueRegex = new("(\\d+) blue");
Regex redRegex = new("(\\d+) red");
Regex greenRegex = new("(\\d+) green");

int total = 0;
StreamReader sr = new("./input.txt");
string? line = sr.ReadLine();
while (line != null)
{
    int id = Int16.Parse(idRegex.Match(line).Groups[1].Captures[0].Value);
    int maxBlue = 0;
    int maxRed = 0;
    int maxGreen = 0;
    foreach (string set in line.Split(";"))
    {
        int blue = (blueRegex.Match(set).Length > 0) ? Int16.Parse(blueRegex.Match(set).Groups[1].Captures[0].Value) : 0;
        int red = (redRegex.Match(set).Length > 0) ? Int16.Parse(redRegex.Match(set).Groups[1].Captures[0].Value) : 0;
        int green = (greenRegex.Match(set).Length > 0) ? Int16.Parse(greenRegex.Match(set).Groups[1].Captures[0].Value) : 0;
        if (blue > maxBlue) maxBlue = blue;
        if (red > maxRed) maxRed = red;
        if (green > maxGreen) maxGreen = green;
    }
    System.Console.WriteLine(maxBlue * maxGreen * maxRed);
    total += maxBlue * maxGreen * maxRed;
    line = sr.ReadLine();
}
System.Console.WriteLine(total);
