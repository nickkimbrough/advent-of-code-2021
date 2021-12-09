int WinBingo(Stack<int> drawNumbers, IList<int[][]> bingoCards, int bingoCardSize)
{
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
                    return (bingoCard.SelectMany(item => item).Sum(x => x != -1 ? x : 0)) * drawnNumber;
                }
            }

            // Check Rows
            foreach (int[] row in bingoCard)
            {
                if (row.Sum() == -5)
                {
                    return (bingoCard.SelectMany(item => item).Sum(x => x != -1 ? x : 0)) * drawnNumber;
                }
            }
        }
    }
    throw new Exception("no winner found!");
}

int LoseBingo(Stack<int> drawNumbers, IList<int[][]> bingoCards, int bingoCardSize)
{
    HashSet<int> winningBingoCardIndexes = new();
    int lastWinningIndex = 0;
    int drawnNumber = 0;
    // Play Bingo with a 🦑
    while (drawNumbers.Count > 0 && winningBingoCardIndexes.Count < bingoCards.Count)
    {
        // Draw the number
        drawnNumber = drawNumbers.Pop();

        // Mark the cards
        foreach (int[][] bingoCard in bingoCards)
        {
            if (!winningBingoCardIndexes.Contains(bingoCards.IndexOf(bingoCard)))
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
        }

        // Search for a winner
        foreach (int[][] bingoCard in bingoCards)
        {
            if (!winningBingoCardIndexes.Contains(bingoCards.IndexOf(bingoCard)))
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
                        winningBingoCardIndexes.Add(bingoCards.IndexOf(bingoCard));
                        lastWinningIndex = bingoCards.IndexOf(bingoCard);
                        break;
                    }
                }

                // Check Rows
                foreach (int[] row in bingoCard)
                {
                    if (row.Sum() == -5)
                    {
                        winningBingoCardIndexes.Add(bingoCards.IndexOf(bingoCard));
                        lastWinningIndex = bingoCards.IndexOf(bingoCard);
                        break;
                    }
                }
            }
        }
    }

    return (bingoCards[lastWinningIndex].SelectMany(item => item).Where(x => x != -1).Sum()) * drawnNumber;
}

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

Console.WriteLine(WinBingo(drawNumbers, bingoCards, bingoCardSize));
Console.WriteLine(LoseBingo(drawNumbers, bingoCards, bingoCardSize));
