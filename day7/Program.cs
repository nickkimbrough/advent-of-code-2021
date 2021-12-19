using MathNet.Numerics.Statistics;

double[] crabPositions = Array.ConvertAll(File.ReadAllText("./input.txt").Split(','), double.Parse);

// Part 1
// Grab the median to get the target position
double median = crabPositions.Median();

int fuelUsed = 0;
// Get the value for the move
foreach (var crabPosition in crabPositions)
{
    fuelUsed += (int)Math.Abs(crabPosition - median);
}

Console.WriteLine(fuelUsed);

// Part 2
// Grab the average to get the target position
double average = Math.Round(crabPositions.Average());

// The exact optimal position isn't always going to be the average,
// but it will be close. Do a brute force within 5% on either side
var bestFuelUsed = int.MaxValue;
var variance = Math.Round((crabPositions.Max() - crabPositions.Min()) * 0.05, 0, MidpointRounding.ToPositiveInfinity);

for (int i = (int)(average - variance); i <= (int)(average + variance); i++)
{
    fuelUsed = 0;

    foreach (var crabPosition in crabPositions)
    {
        int difference = (int)Math.Abs(crabPosition - i);
        // It looks like previous work on Project Euler with triangle numbers is going to help here:
        // (n^2 +n) /2 can get this number
        fuelUsed += (int)((Math.Pow(difference, 2) + difference) / 2);
    }
    if(fuelUsed < bestFuelUsed)
        {
        bestFuelUsed = fuelUsed;
        }
}

Console.WriteLine(bestFuelUsed);
