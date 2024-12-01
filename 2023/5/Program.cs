uint[] seeds = [
    929142010,
    467769747,
    2497466808,
    210166838,
    3768123711,
    33216796,
    1609270159,
    86969850,
    199555506,
    378609832,
    1840685500,
    314009711,
    1740069852,
    36868255,
    2161129344,
    170490105,
    2869967743,
    265455365,
    3984276455,
    31190888
];

StreamReader sr = new("./input.txt");

List<Almanac> almanacs = [];

Almanac seedToSoil = new();
ReadToAlmanac(sr, seedToSoil);
almanacs.Add(seedToSoil);

Almanac soilToFertilizer = new();
ReadToAlmanac(sr, soilToFertilizer);
almanacs.Add(soilToFertilizer);

Almanac fertilizerToWater = new();
ReadToAlmanac(sr, fertilizerToWater);
almanacs.Add(fertilizerToWater);

Almanac waterToLight = new();
ReadToAlmanac(sr, waterToLight);
almanacs.Add(waterToLight);

Almanac lightToTemperature = new();
ReadToAlmanac(sr, lightToTemperature);
almanacs.Add(lightToTemperature);

Almanac temperatureToHumidity = new();
ReadToAlmanac(sr, temperatureToHumidity);
almanacs.Add(temperatureToHumidity);

Almanac humidityToLocation = new();
ReadToAlmanac(sr, humidityToLocation);
almanacs.Add(humidityToLocation);

almanacs.Reverse();
uint i = 0;
uint mappedSeed = 0;
while (!IsSeed(mappedSeed)) {
    mappedSeed = i;
    foreach (Almanac almanac in almanacs) {
        mappedSeed = almanac.BackMap(mappedSeed);
    }
    i++;
}

System.Console.WriteLine(i);

bool IsSeed(uint seed)
{
    for (int i = 0; i < seeds.Length; i += 2) {
        if (seed >= seeds[i] && seed < seeds[i] + seeds[i+1]) return true;
    }
    return false;
}

static void ReadToAlmanac(StreamReader sr, Almanac seedToSoil)
{
    sr.ReadLine();
    string? line = sr.ReadLine();
    while (line != "" && line != null)
    {
        seedToSoil.Parse(line);
        line = sr.ReadLine();
    }
}

class Almanac {
    readonly List<uint> destination_start = [];
    readonly List<uint> source_start = [];
    readonly List<uint> range = [];

    public void Parse(string line) {
        string[] nums = line.Split();
        destination_start.Add(uint.Parse(nums[0]));
        source_start.Add(uint.Parse(nums[1]));
        range.Add(uint.Parse(nums[2]));
    }

    public uint Map(uint input) {
        int index = 0;
        foreach (uint start in source_start)
        {
            uint diff = input - start;
            if (diff >= 0 && diff < range[index]) return destination_start[index] + diff;
            index++;
        }
        return input;
    }

    public uint BackMap(uint input) {
        int index = 0;
        foreach (uint destination in destination_start)
        {
            uint diff = input - destination;
            if (diff >= 0 && diff < range[index]) return source_start[index] + diff;
            index++;
        }
        return input;
    }
}