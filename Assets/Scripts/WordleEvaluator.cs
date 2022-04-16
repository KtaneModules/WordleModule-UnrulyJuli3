public static class WordleEvaluator
{
    private static T[] Fill<T>(T value)
    {
        T[] array = new T[WordleConstants.TileCount];
        for (int i = 0; i < array.Length; i++)
            array[i] = value;

        return array;
    }

    public static TileState[] Evaluate(string solution, string input)
    {
        TileState[] s = Fill(TileState.Absent);
        bool[] t = Fill(true);
        bool[] n = Fill(true);

        for (int o = 0; o < WordleConstants.TileCount; o++)
            if (input[o] == solution[o] && n[o])
            {
                s[o] = TileState.Correct;
                t[o] = false;
                n[o] = false;
            }

        for (int r = 0; r < WordleConstants.TileCount; r++)
        {
            char i = input[r];
            if (t[r])
                for (int l = 0; l < WordleConstants.TileCount; l++)
                {
                    char d = solution[l];
                    if (n[l] && i == d)
                    {
                        s[r] = TileState.Present;
                        n[l] = false;
                        break;
                    }
                }
        }

        return s;
    }
}
