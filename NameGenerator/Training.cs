namespace NameGenerator;

public class Training
{
    private static readonly Dictionary<string, List<string>> dict = new();

    public static Dictionary<string, List<string>> TrainAi(string filePath)
    {
        string body;
        using (var sr = new StreamReader(filePath))
        {
            body = sr.ReadToEnd();
        }

        string[] words = body.Split("\r\n");

        foreach (string word in words)
        {
            for (var i = 0; i < word.Length; i++)
            {
                for (var j = 2; j <= 3; j++)
                {
                    string key = GetKey(word, i, j);

                    string value = i + j + 3 <= word.Length ? word.Substring(i + j, 3) : "";
                    AddEntry(key, value);
                    value = i + j + 2 <= word.Length ? word.Substring(i + j, 2) : "";
                    AddEntry(key, value);
                    value = i + j + 1 <= word.Length ? word.Substring(i + j, 1) : "";
                    AddEntry(key, value);
                }
            }
        }

        return dict;
    }

    private static string GetKey(string word, int i, int length)
    {
        List<char> letterArray = word.Skip(i).Take(length).ToList();
        var key = "";
        key = letterArray.Aggregate(key, (current, letter) => current + letter);
        return key;
    }

    private static void AddEntry(string key, string value)
    {
        if (value == "")
        {
            return;
        }

        if (dict.ContainsKey(key))
        {
            dict[key].Add(value);
        }
        else
        {
            dict.Add(key, new List<string> {value});
        }
    }
}