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

    }

}




Console.WriteLine("b'ah");





//answer = sum of board * last number called