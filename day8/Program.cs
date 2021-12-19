// Part 1

// 1 is only one that uses 2 segments
// 4 is only one that uses 4 segments
// 7 is only one that uses 3 segments
// 8 is only one that uses 7 segments

var input = File.ReadAllLines("./input.txt");

int matchingOutputValues = 0;
foreach (var line in input)
{
    var signals = line.Split('|')[0].Trim().Split(' ');
    var outputs = line.Split('|')[1].Trim().Split(' ');

    matchingOutputValues += outputs.Where(x => x.Length == 2 || x.Length == 4 || x.Length == 3 || x.Length == 7).Count();
}

Console.Write("Part 1 answer: ");
Console.WriteLine(matchingOutputValues);

// Part 2

// 0 - 6 segments, not equivalent to 6 or 9
// 1 is only one that uses 2 segments
// 2 - 5 segments: 1 - 6 = top portion of 1, which 5 doesnt contain but 2 does, 3 is already solved
// 3 - 5 segments, contains all of 1's segments
// 4 is only one that uses 4 segments
// 5 - 5 segments, not equal to 2 or 3
// 6 - 6 segments, doesn't contain all of 1's segments
// 7 is only one that uses 3 segments
// 8 is only one that uses 7 segments
// 9 - is equivalent to 3 + 4

int outputSum = 0;
foreach (var line in input)
{
    var signals = line.Split('|')[0].Trim().Split(' ');

    HashSet<char> one = signals
        .Where(x => x.Length == 2)
        .First()
        .ToCharArray()
        .ToHashSet();
    HashSet<char> three = signals
        .Where(x => x.Length == 5 && x.ToCharArray().ToHashSet().IsSupersetOf(one))
        .First()
        .ToCharArray()
        .ToHashSet();
    HashSet<char> four = signals
        .Where(x => x.Length == 4)
        .First()
        .ToCharArray()
        .ToHashSet();
    HashSet<char> six = signals
        .Where(x => x.Length == 6 && !x.ToCharArray().ToHashSet().IsSupersetOf(one))
        .First()
        .ToCharArray()
        .ToHashSet();
    HashSet<char> seven = signals
        .Where(x => x.Length == 3)
        .First()
        .ToCharArray()
        .ToHashSet();
    HashSet<char> eight = signals
        .Where(x => x.Length == 7)
        .First()
        .ToCharArray()
        .ToHashSet();
    IEnumerable<char> nine = new HashSet<char>(three).Union(four);
    HashSet<char> zero = signals
        .Where(x => x.Length == 6 && !x.ToCharArray().ToHashSet().SetEquals(six) && !x.ToCharArray().ToHashSet().SetEquals(nine))
        .First()
        .ToCharArray()
        .ToHashSet();

    HashSet<char> oneTopSegment = new(one);
    oneTopSegment.ExceptWith(six);

    HashSet<char> two = signals
        .Where(x => x.Length == 5)
        .Where(x => x.ToCharArray().ToHashSet().Contains(oneTopSegment.First()))
        .Where(x => !x.ToCharArray().ToHashSet().SetEquals(three))
        .First()
        .ToCharArray()
        .ToHashSet();

    HashSet<char> five = signals
        .Where(x => x.Length == 5)
        .Where(x => !x.ToCharArray().ToHashSet().SetEquals(two))
        .Where(x => !x.ToCharArray().ToHashSet().SetEquals(three))
        .First()
        .ToCharArray()
        .ToHashSet();


    IList<HashSet<char>> outputNumbers = new List<HashSet<char>>();
    line.Split('|')[1].Trim().Split(' ').ToList().ForEach(x => outputNumbers.Add(x.ToCharArray().ToHashSet()));


    string outputTranslated = "";
    foreach (HashSet<char> digit in outputNumbers)
    {
        // This if tree disgusts me but it's almost midnight and I couldn't figure out how
        // to get this to work in something nicer like a switch expression
        if (digit.SetEquals(zero))
        {
            outputTranslated += "0";
        }
        else if (digit.SetEquals(one))
        {
            outputTranslated += "1";
        }
        else if (digit.SetEquals(two))
        {
            outputTranslated += "2";
        }
        else if (digit.SetEquals(three))
        {
            outputTranslated += "3";
        }
        else if (digit.SetEquals(four))
        {
            outputTranslated += "4";
        }
        else if (digit.SetEquals(five))
        {
            outputTranslated += "5";
        }
        else if (digit.SetEquals(six))
        {
            outputTranslated += "6";
        }
        else if (digit.SetEquals(seven))
        {
            outputTranslated += "7";
        }
        else if (digit.SetEquals(eight))
        {
            outputTranslated += "8";
        }
        else if (digit.SetEquals(nine))
        {
            outputTranslated += "9";
        }
    }

    outputSum += int.Parse(outputTranslated);
}

Console.Write("Part 2 answer: ");
Console.WriteLine(outputSum);