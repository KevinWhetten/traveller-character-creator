namespace NameGenerator;

public static class KaSaraSpelling
{
    public static string CheckKaSaraSpelling(string name)
    {
        var newName = name.Replace("'", ""); 
        newName = newName.ToLower();
        newName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(newName);

        if (name[0] == 'Q') {
            if (newName.Length <= 3) {
                newName += "to";
            }
            if (IsVowel(name[2])) {
                newName = newName.Insert(3, "'");
                if (!char.IsUpper(newName[4])) {
                    var charArray = newName.ToCharArray();
                    charArray[4] = char.ToUpper(charArray[4]);
                    newName = new string(charArray);
                }
            } else {
                newName = newName.Insert(2, "a'");
                if (!char.IsUpper(newName[4])) {
                    var charArray = newName.ToCharArray();
                    charArray[4] = char.ToUpper(charArray[4]);
                    newName = new string(charArray);
                }
            }
        } else {
            if (newName.Length <= 2) {
                newName += "to";
            }
            if (IsVowel(name[1])) {
                newName = newName.Insert(2, "'");
                if (!char.IsUpper(newName[3])) {
                    var charArray = newName.ToCharArray();
                    charArray[3] = char.ToUpper(charArray[3]);
                    newName = new string(charArray);
                }
            } else {
                newName = newName.Insert(1, "a'");
                if (!char.IsUpper(newName[3])) {
                    var charArray = newName.ToCharArray();
                    charArray[3] = char.ToUpper(charArray[3]);
                    newName = new string(charArray);
                }
            }
        }

        return newName;
    }

    private static bool IsVowel(char c)
    {
        return "aeiouAEIOU".Contains(c);
    }
}