namespace MarkovChain;

public interface ILargosianSpelling
{
    string CheckLargosianSpelling(string name);
    bool IsVowel(char c);
    string GetRandomVowel();
    string ReplaceInvalidVowelPairs(string name);
    string ReplaceTripleVowels(string name);
    string ReplaceEndingY(string name);
    string RemoveQuadSs(string name);
    string RemoveDoubleLetters(string name);
    string CheckMiddleSs(string name);
    string CheckBeginningSs(string newName);
    string CheckLastSs(string name);
    string ReplaceEndingConsonants(string name);
}

public class LargosianSpelling : ILargosianSpelling
{
    public string CheckLargosianSpelling(string name)
    {
        string newName = ReplaceInvalidVowelPairs(name);
        newName = ReplaceTripleVowels(newName);
        newName = ReplaceEndingY(newName);

        newName = CheckBeginningSs(newName);
        newName = CheckLastSs(newName);
        newName = CheckMiddleSs(newName);
        newName = RemoveQuadSs(newName);

        newName = RemoveDoubleLetters(newName);
        newName = ReplaceEndingConsonants(newName);
        return newName;
    }

    public bool IsVowel(char c)
    {
        return char.ToLower(c) is 'a' or 'e' or 'i' or 'o' or 'u';
    }

    public string GetRandomVowel()
    {
        var rand = new Random();
        var validVowels = new List<string> {"a", "e", "i", "o", "u", "ae", "ai", "ao", "ie", "ou"};

        return validVowels[rand.Next(0, validVowels.Count - 1)];
    }

    public string ReplaceInvalidVowelPairs(string name)
    {
        var invalidVowelPairs = new List<string> {
            "aa", "au", "ea", "ee", "ei", "eo", "eu", "ia", "ii", "io", "iu", "oa", "oe", "oi", "oo", "ua", "ue", "ui",
            "uo", "uu"
        };
        string newName = name;

        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (string invalidVowelPair in invalidVowelPairs) {
            int? index = newName?.IndexOf(invalidVowelPair, StringComparison.Ordinal);
            if (index is >= 0) {
                newName = newName?.Replace(invalidVowelPair, GetRandomVowel())!;
            }
        }

        return newName ?? string.Empty;
    }

    public string ReplaceTripleVowels(string name)
    {
        var vowelNum = 0;
        string newName = name.ToLower();

        for (var i = 0; i < newName.Length; i++) {
            if (IsVowel(newName[i])) {
                vowelNum++;
            } else {
                vowelNum = 0;
            }

            if (vowelNum >= 3) {
                string randomVowel = GetRandomVowel();
                newName = newName.Replace(newName.Substring(i - vowelNum + 1, vowelNum), randomVowel);
                i = -1;
                vowelNum = 0;
            }
        }

        if (newName.Length <= 1) {
            newName += "sss";
        }

        return char.ToUpper(newName[0]) + newName[1..];
    }

    public string ReplaceEndingY(string name)
    {
        string newName = name;

        if (newName.TakeLast(1).FirstOrDefault() == 'y') {
            newName = newName.Remove(newName.Length - 1);
            newName += GetRandomVowel();
        }

        return newName;
    }

    public string RemoveQuadSs(string name)
    {
        string newName = name.ToLower();
        for (var i = 3; i < newName.Length; i++) {
            if (newName[i] == 's' && newName[i - 1] == 's' && newName[i - 2] == 's' && newName[i - 3] == 's') {
                newName = newName.Remove(i, 1);
                i--;
            }
        }

        return char.ToUpper(newName[0]) + newName[1..];
    }

    public string RemoveDoubleLetters(string name)
    {
        string newName = name.ToLower();
        for (var i = 1; i < newName.Length; i++) {
            if (newName[i] == newName[i - 1] && newName[i] != 's') {
                newName = newName.Remove(i, 1);
                i--;
            }
        }

        return char.ToUpper(newName[0]) + newName[1..];
    }

    public string CheckMiddleSs(string name)
    {
        string newName = name;

        for (var i = 1; i < newName.Length - 2; i++) {
            if (newName[i] == 's' && newName[i - 1] != 's' && newName[i - 1] != 'S') {
                if (newName[i + 1] != 's') {
                    newName = newName.Insert(i,
                        IsVowel(newName[i + 1]) && IsVowel(newName[i - 1])
                            ? "ss"
                            : "s");
                } else if (IsVowel(newName[i - 1]) && IsVowel(newName[i + 2])) {
                    newName = newName.Insert(i, "s");
                }
            }
        }

        return newName;
    }

    public string CheckBeginningSs(string newName)
    {
        if (newName[0] == 'S' && newName[1] != 's') {
            newName = newName.Insert(1, "s");
        }

        if (newName[0] == 'S' && newName[1] == 's' && newName[2] == 's') {
            newName = newName.Remove(1, 1);
        }

        return newName;
    }

    public string CheckLastSs(string name)
    {
        List<char> lastLetters = name.TakeLast(3).ToList();
        var needsSs = 2;
        if (lastLetters[^1] == 's') {
            for (var j = 1; j >= 0; j--) {
                if (lastLetters[j] == 's') {
                    needsSs--;
                } else {
                    break;
                }
            }

            for (var j = 0; j < needsSs; j++) {
                name += 's';
            }
        }

        return name;
    }

    public string ReplaceEndingConsonants(string name)
    {
        string newName = name;
        char lastLetter = name[^1];
        char secondLastLetter = name[^2];

        if (lastLetter is 'r' or 'l'
            && !IsVowel(secondLastLetter)) {
            newName += GetRandomVowel();
        }

        return newName;
    }
}