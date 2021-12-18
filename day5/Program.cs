// Assumptions:
// Vent coordinates will never be below 0, only dealing with quadrant 1
// here in the cartesian coordinate system

static int[,] GetHydroThermalVentField(IEnumerable<int[]> input, bool snapToRightAngles)
{
    // Calculate maximum values for field size
    var xValues = input.SelectMany(x => new[] { x[0], x[2] });
    int maxX = xValues.Max();

    var yValues = input.SelectMany(y => new[] { y[1], y[3] });
    int maxY = yValues.Max();
    int minY = yValues.Min();

    // Initialize field
    int[,] field = new int[maxX + 1, maxY + 1];

    // Add points in between to the field
    foreach (int[] entry in input)
    {
        // get line slope
        decimal mDivisor = entry[2] - (decimal)entry[0];

        if (mDivisor == 0)
        {
            // Line is vertical
            int x = entry[0];
            var yEntryValues = new[] { entry[1], entry[3] };
            int startY = yEntryValues.Min();
            int endY = yEntryValues.Max();

            for (int y = startY; y <= endY; y++)
            {
                field[x, y] += 1;
            }
        }
        else
        {
            // Calculate parts of the y = mx + b equation
            decimal m = ((decimal)entry[3] - (decimal)entry[1]) / mDivisor;

            if (m != 0 && snapToRightAngles)
            {
                continue;
            }

            decimal b = (decimal)entry[1] - (m * (decimal)entry[0]);

            // Get points where X and Y are ints along the line
            var xEntryValues = new[] { entry[0], entry[2] };
            int startX = xEntryValues.Min();
            int endX = xEntryValues.Max();

            for (int x = startX; x <= endX; x++)
            {
                if (int.TryParse(((m * x) + b).ToString(), out int y))
                {
                    y = Math.Abs(y);
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
    .ReadAllLines("./input.txt")
    .Select(s => Array.ConvertAll(s.Replace(" -> ", ",").Split(','), int.Parse));

int[,] field = GetHydroThermalVentField(input, true);

var part1Answer = field.Cast<int>().ToArray().Where(x => x >= 2).Count();
Console.WriteLine($"Danger level is {part1Answer}!");

int[,] field2 = GetHydroThermalVentField(input, false);

// Visualize the field (doesn't work with large fields
//for (int i = 0; i < field2.GetLength(0); i++)
//{
//    for (int j = 0; j < field2.GetLength(1); j++)
//    {
//        Console.Write(string.Format("{0} ", field2[j, i]));
//    }
//    Console.Write(Environment.NewLine + Environment.NewLine);
//}
var part2Answer = field2.Cast<int>().ToArray().Where(x => x >= 2).Count();
Console.WriteLine($"Danger level part 2 is {part2Answer}!");