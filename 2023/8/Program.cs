using System.Text.RegularExpressions;

StreamReader streamReader = new("./input.txt");

string instructions = streamReader.ReadLine() ?? throw new Exception();

streamReader.ReadLine();

string? line = streamReader.ReadLine();

Dictionary<string, string> leftMap = [];
Dictionary<string, string> rightMap = [];

List<string> paths = [];

while (line != null) {
    MatchCollection matches = Regex.Matches(line, "[0-9A-Z]{3}");
    string start = matches[0].Value;
    string left = matches[1].Value;
    string right = matches[2].Value;

    leftMap[start] = left;
    rightMap[start] = right;

    if (start[^1] == 'A') paths.Add(start);

    line = streamReader.ReadLine();
}

System.Console.WriteLine(Navigate(instructions, leftMap, rightMap, paths));


long Navigate(
    string instructions, 
    Dictionary<string, string> leftMap, 
    Dictionary<string, string> rightMap,
    List<string> currSteps
    ) {
        int steps = 0;
        List<long> loopLength = [];

        while (true) 
        {
            foreach (char step in instructions)
            {
                steps += 1;
                int i = 0;
                foreach (string currStep in new List<string>(currSteps)) {
                    if (step == 'L') currSteps[i] = leftMap[currStep];
                    else if (step == 'R') currSteps[i] = rightMap[currStep];
                    i++;
                }

                List<string> toRemove = [];
                foreach (string currStep in currSteps) {
                    if (currStep[^1] == 'Z') {
                        loopLength.Add(steps);
                        toRemove.Add(currStep);
                    }
                }

                currSteps = currSteps.Where(step => !toRemove.Contains(step)).ToList();

                if (currSteps.Count == 0)
                    return LCM([.. loopLength]);
            }
        }
    }


// Used the answer from https://stackoverflow.com/a/29717490 because couldn't be bothered
static long LCM(long[] numbers)
{
    return numbers.Aggregate(lcm);
}

static long lcm(long a, long b)
{
    return Math.Abs(a * b) / GCD(a, b);
}

static long GCD(long a, long b)
{
    return b == 0 ? a : GCD(b, a % b);
}
