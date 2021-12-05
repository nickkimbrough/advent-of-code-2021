static int[] GetPowerRates(string[] input)
{
    string gammaRate = string.Empty;
    string epsilonRate = string.Empty;

    for (int i = 0; i < input[0].Length; i++)
    {
        int oneCount = 0;

        foreach (string value in input)
        {
            if (value[i] == '1')
            {
                oneCount += 1;
            }
        }

        if (oneCount > input.Length / 2)
        {
            gammaRate += "1";
            epsilonRate += "0";
        }
        else
        {
            gammaRate += "0";
            epsilonRate += "1";
        }
    }
    int[] rates = new int[2] { Convert.ToInt32(gammaRate, 2), Convert.ToInt32(epsilonRate, 2) };

    return rates;
}

int GetOxygenRate(string[] input)
{
    IList<string> oxygen = new List<string>(input);

    for (int i = 0; i < input[0].Length; i++)
    {
        var ones = oxygen.Where(x => x[i] == '1');
        var onesCount = ones.Count();

        var zeroes = oxygen.Where(x => x[i] == '0');
        var zeroesCount = zeroes.Count();

        if (onesCount >= zeroesCount)
        {
            oxygen = ones.ToList();
        }
        else
        {
            oxygen = zeroes.ToList();
        }

        if (oxygen.Count == 1)
        {
            return Convert.ToInt32(oxygen[0], 2);
        }
    }

    throw new Exception("Could not find Oxygen Rate!");
}

int GetCo2Rate(IList<string> input)
{
    for (int i = 0; i < input[0].Length; i++)
    {
        var ones = input.Where(x => x[i] == '1');
        var onesCount = ones.Count();

        var zeroes = input.Where(x => x[i] == '0');
        var zeroesCount = zeroes.Count();

        if (zeroesCount <= onesCount)
        {
            input = zeroes.ToList();
        }
        else
        {
            input = ones.ToList();
        }

        if (input.Count == 1)
        {
            return Convert.ToInt32(input[0], 2);
        }
    }

    throw new Exception("Could not find Co2 Rate!");
}

string[] input = File.ReadAllLines("./input.txt");

// I am quite certain that the bitwise complement operator here could
// be used on the gamma rate to get the upsilon rate, but I was not able
// to get C# 7 binary literals to play nice with the operator here.
// so instead I calculated the epsilonRate as well.
//https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#bitwise-complement-operator-
int[] rates = GetPowerRates(input);
Console.WriteLine(rates[0] * rates[1]);

int oxygenRate = GetOxygenRate(input);
int co2Rate = GetCo2Rate(input.ToList<string>());
Console.WriteLine(oxygenRate * co2Rate);