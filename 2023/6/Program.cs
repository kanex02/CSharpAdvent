(long, long)[] inputs = [
    (48989083, 390110311121360),
];

long prod = 1;

foreach ((long t, long d) in inputs) {
    long range = WaysToWin(t, d);
    System.Console.WriteLine(range);
    prod *= range;
}

System.Console.WriteLine(prod);

static long WaysToWin(long time, long recordDistance)
{
    double disc = Math.Sqrt(time * time - 4 * recordDistance);

    double max = (time + disc) / 2.0;
    double min = (time - disc) / 2.0;

    long range = (long)Math.Floor(max) - (long)Math.Floor(min);
    return range;
}