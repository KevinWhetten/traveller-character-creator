namespace NameGenerator;

public class NameGenerator
{
    private static Dictionary<string, List<string>> dict = new();

    private static string Markov(int outputSize)
    {
        var rand = new Random();
        var output = "";
        string prefix = getPrefix();

        output += prefix;

        while (true) {
            List<string> suffix = GetSuffix(prefix, rand);
            if (suffix.Count == 1) {
                if (suffix[0] == "") {
                    return output;
                }

                output += suffix[0];
            } else {
                int rn = rand.Next(suffix.Count);
                output += suffix[rn];
            }

            if (output.Length >= outputSize) {
                return output;
            }

            prefix = output.TakeLast(rand.Next(2, 3)).Aggregate("", (x, y) => x + y);
        }
    }

    private static List<string> GetSuffix(string prefix, Random rand)
    {
        try {
            List<string> suffix = dict[prefix.TakeLast(rand.Next(2, 3)).Aggregate("", (x, y) => x + y)];
            return suffix;
        }
        catch {
            return new List<string> {""};
        }
    }

    private static string getPrefix()
    {
        var rand = new Random();
        while (true) {
            int rn = rand.Next(dict.Count);
            string prefix = dict.Keys.ElementAtOrDefault(rn) ?? "";
            if (char.IsUpper(prefix.First())) {
                return prefix;
            }
        }
    }

    public static string GeneratePlanetName(string textFile)
    {
        var random = new Random();
        dict = Training.TrainAi(textFile);
        var letters = random.Next(1, 6) + random.Next(1, 6);
        var name = Markov(letters);
        switch (textFile) {
            case "../../../../Data/LanguageFiles/ssitolusss_planets.txt":
                name = SsitolusssSpelling.CheckSsitolusssSpelling(name);
                break;
            case "../../../../Data/LanguageFiles/kaSara_planets.txt":
                name = KaSaraSpelling.CheckKaSaraSpelling(name);
                break;
        }

        if (!char.IsLetter(name.Last())) {
            name = name[..^1];
        }

        return name;
    }
}