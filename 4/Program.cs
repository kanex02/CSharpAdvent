using System.ComponentModel.DataAnnotations;

StreamReader sr = new("./input.txt");
string? line = sr.ReadLine();
List<int> scores = [];

while (line != null)
{
    string nums = line.Split(": ")[1];
    List<int> winning = nums.Split(" | ")[0]
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToList();
    List<int> card = nums.Split(" | ")[1]
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToList();
    
    int score = 0;

    foreach (int num in card)
    {
        if (winning.IndexOf(num) != -1) score += 1;
    }

    scores.Add(score);

    line = sr.ReadLine();
}

List<int> cards = Enumerable.Repeat(1, scores.Count).ToList();

for (int card = 0; card < cards.Count; card += 1) {
    int score = scores[card];
    for (int increment = 1; increment < score+1; increment++) {
        cards[card + increment] += cards[card];
    }
}

System.Console.WriteLine(cards);
System.Console.WriteLine(cards.Sum());