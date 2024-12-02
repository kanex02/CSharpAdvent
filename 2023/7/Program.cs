using System.Collections;
using System.Text.RegularExpressions;
using _7;



SortedSet<Hand> hands = new(new _7.HandComparer());
StreamReader sr = new("./input.txt");
string? line = sr.ReadLine();

while (line != null) {
    string[] split = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    string cards = split[0];
    int bid = int.Parse(split[1]);
    int type = Type(cards);
    Hand hand = new(type, cards, bid);
    hands.Add(hand);
    line = sr.ReadLine();
}

int i = 1;
int sum = 0;
foreach (Hand hand in hands)
{
    sum += i * hand.Bid;
    i++;
}

System.Console.WriteLine(sum);

static int Type(string hand)
{
    List<int> counts = [];
    int jokers = Regex.Matches(hand, "J").Count;
    foreach (char card in hand.Distinct()) {
        int count = Regex.Matches(hand, card.ToString()).Count;
        if (card != 'J') count += jokers;
        counts.Add(count);
    }
    if (counts.Max() == 5) return 7;
    if (counts.Max() == 4) return 6;
    if (counts.Contains(3) && counts.Contains(2) && jokers == 0) return 5;
    if (counts.Count(i => i == 3) == 2 && jokers == 1) return 5;
    if (counts.Max() == 3) return 4;
    if (counts.Count(i => i == 2) == 2 && jokers == 0) return 3;
    if (counts.Count(i => i == 2) == 2 && jokers == 1) return 2;
    if (counts.Max() == 2) return 2;
    return 1;
}

namespace _7 {
public readonly struct Hand(int type, string cards, int bid)
{
    public readonly int Type = type;
    public readonly string Cards = cards;
    public readonly int Bid = bid;
}


public class HandComparer : IComparer<Hand>
{
    readonly Dictionary<char, int> CardNum = new() {
        {'2', 2},
        {'3', 3},
        {'4', 4},
        {'5', 5},
        {'6', 6},
        {'7', 7},
        {'8', 8},
        {'9', 9},
        {'T', 10},
        {'J', 1},
        {'Q', 12},
        {'K', 13},
        {'A', 14},
    };
    public int Compare(Hand x, Hand y)
    {
        if (x.Type > y.Type) return 1;
        if (x.Type < y.Type) return -1;
        int i = 0;
        foreach (char card in x.Cards) {
            if (CardNum[card] < CardNum[y.Cards[i]]) return -1;
            if (CardNum[card] > CardNum[y.Cards[i]]) return 1;
            i++;
        }
        return 0;
    }
}
}
