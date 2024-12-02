StreamReader sr = new("./test.txt");

string? line = sr.ReadLine();
List<int> newNums = [];
while (line != null) {
    List<List<int>> differences = [];
    differences.Add(
        line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToList());

    while (differences.Last().Distinct().Count() > 1) {
        List<int> newDiffs = [];
        for (int i = 1; i < differences.Last().Count; i++) {
            newDiffs.Add(differences.Last()[i] - differences.Last()[i-1]);
        }
        differences.Add(newDiffs);
    }

    differences.Add([0]);

    for (int i = differences.Count - 2; i >= 0; i--) {
        // Super hacky solution to extrapolate forwards
        differences[i].Add(differences[i][0] - differences[i+1].Last());
    }

    newNums.Add(differences[0].Last());

    line = sr.ReadLine();
}

System.Console.WriteLine(newNums.Sum());
