// Assumptions:
// Vent coordinates will never be below 0, only dealing with quadrant 1
// here in the cartesian coordinate system

static int[,] GetHydroThermalVentField(IEnumerable<int[]> input, bool snapToRightAngles)
{
    // Calculate maximum values for field size
    var xValues = input.SelectMany(x => new[] { x[0], x[2] });
    int maxX = xValues.Max();
    int minX = xValues.Min();

    var yValues = input.SelectMany(y => new[] { y[0], y[2] });
    int maxY = yValues.Max();
    int minY = yValues.Min();

    // Initialize field
    int arrayX = maxX - minX + 1;
    int arrayY = maxY - minY + 1;
    int[,] field = new int[arrayX, arrayY];

    // Add points already existing to the field
    foreach (int[] entry in input)
    {
        field[entry[0], entry[1]] += 1;
        field[entry[2], entry[3]] += 1;
    }

    // Add points in between to the field
    foreach (int[] entry in input)
    {
        // get line slope
        decimal mDivisor = ((decimal)entry[2] - (decimal)entry[0]);

        if (mDivisor == 0)
        {
            // Line is vertical
            int x = entry[0];
            var yEntryValues = new[] { entry[1], entry[3] };
            int startY = yEntryValues.Min();
            int endY = yEntryValues.Max();

            for (int y = startY + 1; y < endY; y++)
            {
                field[x, y] += 1;
            }
        }
        else
        {
            // Calculate parts of the y = mx + b equation
            decimal m = ((decimal)entry[1] - (decimal)entry[3]) / mDivisor;

            if (m != 0 && snapToRightAngles)
            {
                continue;
            }

            decimal b = (decimal)entry[1] - (m * (decimal)entry[0]);

            // Get points where X and Y are ints along the line
            var xEntryValues = new[] { entry[0], entry[2] };
            int startX = xEntryValues.Min();
            int endX = xEntryValues.Max();

            for (int x = startX + 1; x < endX; x++)
            {
                if (int.TryParse(((m * x) + b).ToString(), out int y))
                {
                    if (minY <= y && y <= maxY)
                    {
                        field[x, y] += 1;
                    }
                }
            }
        }
    }

    return field;
}

// Gather input
IEnumerable<int[]>? input = File
    .ReadAllLines("./sampleinput.txt")
    .Select(s => Array.ConvertAll(s.Replace(" -> ", ",").Split(','), int.Parse));

int[,] field = GetHydroThermalVentField(input, true);

// Visualize the field (this is rotated but still accurate)
for (int i = 0; i < field.GetLength(0); i++)
{
    for (int j = 0; j < field.GetLength(1); j++)
    {
        Console.Write(string.Format("{0} ", field[i, j]));
    }
    Console.Write(Environment.NewLine + Environment.NewLine);
}

var part1Answer = field.Cast<int>().ToArray().Where(x => x >= 2).Count();
Console.WriteLine($"Danger level is {part1Answer}!");