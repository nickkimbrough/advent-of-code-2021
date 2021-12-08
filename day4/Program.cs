// Gather Data
int bingoCardSize = 5;
Stack<string> input = new(File.ReadAllLines("./input.txt").Reverse());

Stack<int> drawNumbers = new(Array.ConvertAll(input.Pop().Split(',').ToArray(), s => int.Parse(s)).Reverse());

IList<int[][]> bingoCards = new List<int[][]>();

while (input.Count > 0)
{
    string currentLine = input.Pop();
    if (string.IsNullOrWhiteSpace(currentLine))
    {
        int[][] bingoCard = new int[bingoCardSize][];
        for (int i = 0; i < bingoCardSize; i++)
        {
            bingoCard[i] = Array.ConvertAll(input.Pop().Trim().Replace("  ", " ").Split(' '), s => int.Parse(s));
        }
        bingoCards.Add(bingoCard);
    }
}

// Play Bingo with a 🦑
while (drawNumbers.Count > 0)
{
    // Draw the number
    int drawnNumber = drawNumbers.Pop();

    // Mark the cards
    foreach (int[][] bingoCard in bingoCards)
    {
        for (int i = 0; i < bingoCardSize; i++)
        {
            for (int j = 0; j < bingoCardSize; j++)
            {
                if (bingoCard[i][j] == drawnNumber)
                {
                    bingoCard[i][j] = -1;
                }
            }
        }
    }

    // Search for a winner
    foreach (int[][] bingoCard in bingoCards)
    {
        // Check Columns
        for (int i = 0; i < bingoCardSize; i++)
        {
            int columnSum = 0;
            for (int j = 0; j < bingoCardSize; j++)
            {
                columnSum += bingoCard[j][i];
            }

            if (columnSum == -5)
            {
                Console.WriteLine((bingoCard.SelectMany(item => item).Sum(x => x != -1 ? x : 0)) * drawnNumber);
                Environment.Exit(0);
            }
        }

        // Check Rows
        foreach (int[] row in bingoCard)
        {
            if (row.Sum() == -5)
            {
                Console.WriteLine((bingoCard.SelectMany(item => item).Where(x => x != -1).Sum()) * drawnNumber);
                Environment.Exit(0);
            }
        }
    }
}
