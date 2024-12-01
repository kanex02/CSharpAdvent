using System.Reflection;

StreamReader streamReader = new("./input.txt");
string? file = streamReader.ReadToEnd();

string[] lines = file.Split("\n", StringSplitOptions.RemoveEmptyEntries);

List<int> list1 = [];
List<int> list2 = [];

foreach (string line in lines)
{
    string[] nums = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    list1.Add(int.Parse(nums[0]));
    list2.Add(int.Parse(nums[1]));
}

System.Console.WriteLine(Similarity(list1, list2));

static int Similarity(List<int> list1, List<int> list2)
{
    int sum = 0;
    foreach (int num in list1) {
        sum += num * list2.Where(i => i == num).Count();
    }
    return sum;
}

static int Distance(List<int> list1, List<int> list2) 
{

    list1.Sort();
    list1.Reverse();

    list2.Sort();
    list2.Reverse();

    int sum = 0;
    int i = 0;
    foreach (int num in list1)
    {
        sum += Math.Abs(num - list2[i]); 
        i++;
    }

    return sum;
}


