StreamReader sr = new("./test.txt");

string file = sr.ReadToEnd() ?? throw new Exception();

string[] lines = file.Split("\n", StringSplitOptions.RemoveEmptyEntries);

HashSet<(int, int)> pipeCoords = [];

HashSet<(int, int)> pipeRight = [];

(int pi, int pj) = (4, 12);
(int ci, int cj) = (5, 12);
int steps = 1;

while (lines[ci][cj] != 'S') {
    char pipe = lines[ci][cj];
    pipeCoords.Add((ci, cj));
    int di = ci - pi;
    int dj = cj - pj;

    pi = ci;
    pj = cj;

    if (di == -1) {
        // North
        switch (pipe)
        {
            case '|':
                ci -= 1;
                pipeRight.Add((ci, cj+1));
                break;
            case '7':
                cj -= 1;
                pipeRight.Add((ci, cj+1));
                pipeRight.Add((ci-1, cj));
                break;
            case 'F':
                cj += 1;
                break;
        }
    } else if (di == 1) {
        // South
        switch (pipe)
        {
            case '|':
                ci += 1;
                pipeRight.Add((ci, cj-1));
                break;
            case 'L':
                cj += 1;
                pipeRight.Add((ci, cj-1));
                pipeRight.Add((ci+1, cj));
                break;
            case 'J':
                cj -= 1;
                break;
        }
    } else if (dj == -1) {
        // West
        switch (pipe)
        {
            case '-':
                cj -= 1;
                pipeRight.Add((ci-1, cj));
                break;
            case 'L':
                ci -= 1;
                pipeRight.Add((ci-1, cj));
                pipeRight.Add((ci, cj-1));
                break;
            case 'F':
                ci += 1;
                break;
        }
    } else {
        // East
        switch (pipe)
        {
            case '-':
                cj += 1;
                pipeRight.Add((ci+1, cj));
                break;
            case 'J':
                ci -= 1;
                pipeRight.Add((ci+1, cj));
                pipeRight.Add((ci, cj+1));
                break;
            case '7':
                ci += 1;
                break;
        }
    }
    steps++;
}

pipeRight = pipeRight.Except(pipeCoords).Where(coord => coord.Item1 >= 0
            && coord.Item1 < 10
            && coord.Item2 >= 0
            && coord.Item2 < 20).ToHashSet();

bool changed = true;
while (changed) {
    changed = false;
    (int, int)[] toAdd = [(-1, -1), (-1, -1), (-1, -1), (-1, -1)];
    toAdd[0] = (ci-1, cj);
    toAdd[1] = (ci+1, cj);
    toAdd[2] = (ci, cj-1);
    toAdd[3] = (ci, cj+1);

    foreach ((int, int) coord in toAdd) {
        if (coord.Item1 >= 0
            && coord.Item1 < 10
            && coord.Item2 >= 0
            && coord.Item2 < 20
            && !pipeRight.Contains(coord) 
            && !pipeCoords.Contains(coord)
        ) {
            pipeRight.Add(coord);
            changed = true;
        }
    }
}

int count = 0;

var copy = lines.Select(str => str.ToCharArray()).ToList();

pipeRight = pipeRight.Except(pipeCoords).Where(coord => coord.Item1 >= 0
            && coord.Item1 < 10
            && coord.Item2 >= 0
            && coord.Item2 < 20).ToHashSet();

foreach ((int i, int j) in pipeRight) {
    if (lines[i][j] == '.') {
        count++;
        copy[i][j] = 'I';
    }
}

foreach ((int i, int j) in pipeCoords) {
    copy[i][j] = '*';
}

StreamWriter sw = new("./new.txt");
foreach (var line in copy)
{
    sw.WriteLine(line);
}
sw.Close();

System.Console.WriteLine(count);