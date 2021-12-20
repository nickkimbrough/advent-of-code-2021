// Assumptions: input always a square
using System.Collections;

IList<int[]> input = new List<int[]>();
File.ReadAllLines("./input.txt")
    .ToList()
    .ForEach(x => input.Add(Array.ConvertAll(x.ToCharArray(), c => (int)char.GetNumericValue(c))));

static ICollection<int> GetLowPoints(IList<int[]> input)
{
    ICollection<int> lowPoints = new List<int>();
    for (int y = 0; y < input.Count; y++)
    {
        for (int x = 0; x < input[y].Length; x++)
        {
            // Get adjacent points

            ICollection<int> adjacentPoints = new List<int>();
            if (x > 0)
            {
                adjacentPoints.Add(input[y][x - 1]);
            }
            if (x + 1 < input[y].Length)
            {
                adjacentPoints.Add(input[y][x + 1]);
            }
            if (y > 0)
            {
                adjacentPoints.Add(input[y - 1][x]);
            }
            if (y + 1 < input.Count)
            {
                adjacentPoints.Add(input[y + 1][x]);
            }

            if (input[y][x] < adjacentPoints.Min())
            {
                lowPoints.Add(input[y][x]);   
            }
        }
    }

    return lowPoints;
}

int GetRiskLevel(ICollection<int> lowPoints)
{
    return lowPoints.Sum() + lowPoints.Count;
}

Console.WriteLine("Part 1 answer:");
Console.WriteLine(GetRiskLevel(GetLowPoints(input)));

static int GetBasins(IList<int[]> input)
{
    ICollection<int[]> lowPoints = new List<int[]>();
    for (int y = 0; y < input.Count; y++)
    {
        for (int x = 0; x < input[y].Length; x++)
        {
            // Get adjacent points
            ICollection<int> adjacentPoints = new List<int>();
            if (x > 0)
            {
                adjacentPoints.Add(input[y][x - 1]);
            }
            if (x + 1 < input[y].Length)
            {
                adjacentPoints.Add(input[y][x + 1]);
            }
            if (y > 0)
            {
                adjacentPoints.Add(input[y - 1][x]);
            }
            if (y + 1 < input.Count)
            {
                adjacentPoints.Add(input[y + 1][x]);
            }

            if (input[y][x] < adjacentPoints.Min())
            {
                lowPoints.Add(new int[] { y, x});
            }
        }
    }

    ICollection<int> basinSizes = new List<int>();
    foreach (int[] lowPoint in lowPoints)
    {
        int basinSize = 0;

        Stack<int[]> basinPoints = new();
        basinPoints.Push(lowPoint);

        ICollection<int[]> alreadyRecordedPoints = new List<int[]>();

        while (basinPoints.Count > 0)
        {
            int[] basinPoint = basinPoints.Pop();

            if (!alreadyRecordedPoints.Where(x => x[0] == basinPoint[0] && x[1] == basinPoint[1]).Any())
            {
                alreadyRecordedPoints.Add(basinPoint);
                basinSize += 1;

                // Get adjacent points that are not 9
                ICollection<int> adjacentPoints = new List<int>();
                if (basinPoint[1] > 0)
                {
                    if (input[basinPoint[0]][basinPoint[1] - 1] != 9)
                    {
                        basinPoints.Push(new int[] { basinPoint[0], basinPoint[1] - 1 });
                    }
                }
                if (basinPoint[1] + 1 < input[basinPoint[0]].Length)
                {
                    if (input[basinPoint[0]][basinPoint[1] + 1] != 9)
                    {
                        basinPoints.Push(new int[] { basinPoint[0], basinPoint[1] + 1 });
                    }
                }
                if (basinPoint[0] > 0)
                {
                    if (input[basinPoint[0] - 1][basinPoint[1]] != 9)
                    {
                        basinPoints.Push(new int[] { basinPoint[0] - 1, basinPoint[1] });
                    }
                }
                if (basinPoint[0] + 1 < input.Count)
                {
                    if (input[basinPoint[0] + 1][basinPoint[1]] != 9)
                    {
                        basinPoints.Push(new int[] { basinPoint[0] + 1, basinPoint[1]});
                    }
                }
            }
        }
        basinSizes.Add(basinSize);
    }

    return basinSizes.OrderByDescending(x => x).Take(3).Aggregate(1, (x,y) => x * y);
}
Console.WriteLine("Part 2 answer:");
Console.WriteLine(GetBasins(input));
