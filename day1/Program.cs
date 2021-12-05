static int GetLargerMeasurements(int[] measurements)
{
    int previousMeasurement = measurements[0];
    int largerMeasurements = 0;

    foreach (int measurement in measurements)
    {
        if (measurement > previousMeasurement)
        {
            largerMeasurements++;
        }
        previousMeasurement = measurement;
    }

    return largerMeasurements;
}

static int GetLargerMeasurementsInWindow(int[] measurements)
{
    int previousMeasurement = measurements[0] + measurements[1] + measurements[2];
    int largerMeasurements = 0;

    for (int i = 0; i < measurements.Length - 3; i++)
    {
        int windowA = measurements[i] + measurements[i + 1] + measurements[i + 2];
        int windowB = measurements[i + 1] + measurements[i + 2] + measurements[i + 3];
        if (windowB > windowA)
        {
            largerMeasurements++;
        }
        previousMeasurement = i;
    }

    return largerMeasurements;
}

int[] input = Array.ConvertAll(File.ReadAllLines("./input.txt"), s => int.Parse(s));

Console.WriteLine(GetLargerMeasurements(input));
Console.WriteLine(GetLargerMeasurementsInWindow(input));
