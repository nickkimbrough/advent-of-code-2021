using System.Numerics;

static BigInteger GetLanternFishPopulation(int[] initialPopulation, int days)
{
    // Dictionary of fish internal timer values and a count
    // 6 -> 5 -> 4 -> 3 -> 2 -> 1 -> 0 -> 6 + new fish with 8
    Dictionary<int, BigInteger> fishPopulation = new();

    // Populate keys
    for (int i = 0; i <= 8; i++)
    {
        fishPopulation.Add(i, 0);
    }

    // Initialize the fish population given the input.
    foreach (int fish in initialPopulation)
    {
        fishPopulation[fish] += 1;
    }

    for (int i = 0; i < days; i++)
    {
        Dictionary<int, BigInteger> yesterdaysPopulation = new(fishPopulation);

        // Simulate population growth
        fishPopulation[8] = yesterdaysPopulation[0];
        fishPopulation[7] = yesterdaysPopulation[8];
        fishPopulation[6] = BigInteger.Add(yesterdaysPopulation[0], yesterdaysPopulation[7]);
        fishPopulation[5] = yesterdaysPopulation[6];
        fishPopulation[4] = yesterdaysPopulation[5];
        fishPopulation[3] = yesterdaysPopulation[4];
        fishPopulation[2] = yesterdaysPopulation[3];
        fishPopulation[1] = yesterdaysPopulation[2];
        fishPopulation[0] = yesterdaysPopulation[1];
    }

    return fishPopulation.Values.Aggregate(BigInteger.Add);
}

// Gather input
var input = Array.ConvertAll(File.ReadAllText("./input.txt").Split(','), int.Parse);
Console.WriteLine(GetLanternFishPopulation(input, 80));
Console.WriteLine(GetLanternFishPopulation(input, 256));