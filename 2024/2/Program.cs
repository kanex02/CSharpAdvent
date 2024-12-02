StreamReader sr = new("./input.txt");

int count = 0;

string? line = sr.ReadLine();

while (line != null) {
    int[] nums = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    int[] diffs = new int[nums.Length - 1];
    for (int i = 1; i < nums.Length; i++) {
        diffs[i-1] = nums[i] - nums[i-1];
    }

    int[] cleaned = RemoveOneError(diffs);

    if (cleaned.Max() < 0 && cleaned.Min() > -4) count++;
    if (cleaned.Min() > 0 && cleaned.Max() < 4) count++;
    line = sr.ReadLine();
}

System.Console.WriteLine(count);

int[] RemoveOneError(int[] diffs) {
    bool positive = diffs.Where(i => i != 0).Select(i => i/Math.Abs(i)).Sum() > 0;
    bool ValidNum(int num) {
        if (positive) return ValidPositive(num);
        return ValidNegative(num);
    }

    for (int i=1; i < diffs.Length-1; i++) {
        if (!ValidNum(diffs[i])) {
            if (ValidNum(diffs[i+1]) && ValidNum(diffs[i] + diffs[i-1])) 
                return [.. diffs[..(i-1)], diffs[i-1] + diffs[i], .. diffs[(i+1)..diffs.Length]];
            return [.. diffs[..i], diffs[i] + diffs[i+1], .. diffs[(i+2)..diffs.Length]];
        }
    }
    if (!ValidNum(diffs[0])) return diffs[1..];
    if (!ValidNum(diffs[^1])) return diffs[..(diffs.Length-1)];
    return diffs;
}

bool ValidPositive(int num) {return num > 0 && num < 4;}
bool ValidNegative(int num) {return num < 0 && num > -4;}