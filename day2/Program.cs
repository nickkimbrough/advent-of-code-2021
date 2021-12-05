static int GetPosition(string[] input)
{
    int horizontalPosition = 0;
    int depth = 0;
    foreach (var line in input)
    {
        var command = line.Split(' ');

        switch (command[0])
        {
            case "forward":
                horizontalPosition += int.Parse(command[1]);
                break;
            case "up":
                depth -= int.Parse(command[1]);
                break;
            case "down":
                depth += int.Parse(command[1]);
                break;
        }
    }

    return horizontalPosition * depth;
}

static int GetPositionWithAim(string[] input)
{
    int horizontalPosition = 0;
    int depth = 0;
    int aim = 0;
    foreach (var line in input)
    {
        var command = line.Split(' ');

        switch (command[0])
        {
            case "forward":
                horizontalPosition += int.Parse(command[1]);
                depth += aim * int.Parse(command[1]);
                break;
            case "up":
                aim -= int.Parse(command[1]);
                break;
            case "down":
                aim += int.Parse(command[1]);
                break;
        }
    }

    return horizontalPosition * depth;
}

string[] input = File.ReadAllLines("./input.txt");

Console.WriteLine(GetPosition(input));
Console.WriteLine(GetPositionWithAim(input));