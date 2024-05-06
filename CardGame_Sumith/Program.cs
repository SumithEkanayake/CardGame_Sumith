namespace CardGame_Sumith;

internal class Program
{
    static void Main(string[] args)
    {
        List<string> cards = new List<string>
        {
            "2@", "2#", "2^", "2*",
            "3@", "3#", "3^", "3*",
            "4@", "4#", "4^", "4*",
            "5@", "5#", "5^", "5*",
            "6@", "6#", "6^", "6*",
            "7@", "7#", "7^", "7*",
            "8@", "8#", "8^", "8*",
            "9@", "9#", "9^", "9*",
            "10@", "10#", "10^", "10*",
            "J@", "J#", "J^", "J*",
            "Q@", "Q#", "Q^", "Q*",
            "K@", "K#", "K^", "K*",
            "A@", "A#", "A^", "A*"
        };

        //Shuffle the cards
        Shuffle(cards);
        Console.WriteLine("Shuffled Cards:");
        Console.WriteLine(string.Join(" ", cards));

        //Distribute cards to players
        Dictionary<int, List<string>> players = DistributeCards(cards);

        //Evaluate the winner
        int winner = EvaluateWinner(players);

        // Display the distributed cards and winner
        foreach (var player in players)
        {
            Console.WriteLine($"Player {player.Key}: {string.Join(" ", player.Value)}");
        }
        Console.WriteLine($"Winner: Player {winner}");
    }

    static void Shuffle<T>(List<T> list)
    {
        Random rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    static Dictionary<int, List<string>> DistributeCards(List<string> cards)
    {
        Dictionary<int, List<string>> players = new Dictionary<int, List<string>>();
        int numPlayers = 4;
        int cardsPerPlayer = cards.Count / numPlayers;

        for (int i = 0; i < numPlayers; i++)
        {
            List<string> playerCards = cards.GetRange(i * cardsPerPlayer, cardsPerPlayer);
            players.Add(i + 1, playerCards);
        }

        return players;
    }

    static int EvaluateWinner(Dictionary<int, List<string>> players)
    {
        int winner = 1;
        int maxMatchingCards = 0;

        foreach (var player in players)
        {
            Dictionary<string, int> cardCounts = new Dictionary<string, int>();

            foreach (var card in player.Value)
            {
                string key = card.Substring(0, card.Length - 1); // Remove the symbol part
                if (cardCounts.ContainsKey(key))
                    cardCounts[key]++;
                else
                    cardCounts.Add(key, 1);
            }

            int matchingCards = cardCounts.Values.Max();
            if (matchingCards > maxMatchingCards || (matchingCards == maxMatchingCards && GetHighValue(player.Value) > GetHighValue(players[winner])))
            {
                winner = player.Key;
                maxMatchingCards = matchingCards;
            }
        }

        return winner;
    }

    static int GetHighValue(List<string> cards)
    {
        int maxValue = 0;
        foreach (var card in cards)
        {
            string valuePart = card.Substring(0, card.Length - 1);
            if (int.TryParse(valuePart, out int value))
            {
                if (value > maxValue)
                {
                    maxValue = value;
                }
            }
        }
        return maxValue;
    }
}


