using System.Text.RegularExpressions;

StreamReader sr = new("./input.txt");
string input = sr.ReadToEnd();
string[] lines = input.Split("\n");

Regex digitsRegex = new("(\\d+)");

Dictionary<Tuple<int, int>, List<int>> gearMapping = [];

int total = 0;
int lineNum = 0;
foreach (string line in lines) 
{
    foreach (Match match in digitsRegex.Matches(line))
    {
        int startIndex = match.Captures[0].Index;
        int length = match.Captures[0].Length;
        int value = Int16.Parse(match.Captures[0].Value);

        if (lineNum > 0)
        {
            if (startIndex > 0) 
                addNumToDictIfGear(lineNum-1, startIndex-1, value);
            for (int i = startIndex; i < startIndex + length; i++)
                addNumToDictIfGear(lineNum-1, i, value);
            if (startIndex + length < 139)
                addNumToDictIfGear(lineNum-1, startIndex + length, value);
        }
        if (startIndex > 0) 
            addNumToDictIfGear(lineNum, startIndex-1, value);
        if (startIndex + length < 139)
            addNumToDictIfGear(lineNum, startIndex + length, value);
        if (lineNum < 139)
        {
            if (startIndex > 0) 
                addNumToDictIfGear(lineNum+1, startIndex-1, value);
            for (int i = startIndex; i < startIndex + length; i++)
                addNumToDictIfGear(lineNum+1, i, value);
            if (startIndex + length < 139)
                addNumToDictIfGear(lineNum+1, startIndex + length, value);
        }
    }
    lineNum += 1;
}

foreach ((Tuple<int, int> _, List<int> values) in gearMapping)
{
    if (values.Count() == 2) total += values[0] * values[1];
}

void addNumToDictIfGear(int i, int j, int num)
{
    if (lines[i][j] == '*')
    {
        try
        {
            gearMapping[new Tuple<int, int>(i, j)].Add(num);
        } catch
        {
            List<int> list = [num];
            gearMapping[new Tuple<int, int>(i,j)] = list;
        }
    }
}

System.Console.WriteLine(total);
