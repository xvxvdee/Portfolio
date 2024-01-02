using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;

namespace inputToMongoDB.Serivce;
public class InputToMongoDBService
{

    private readonly Regex alphanumericPattern = new Regex("^[a-zA-Z0-9]*$");
    public bool CheckIdRange(int id, long size)
    { //Ensure int Id exists in colection
        if (id > size || id < 1)
        {
            return false;
        }
        return true;
    }

    // Serialize inputs with fragmentidentifiers (Ex. # -> %23)
    public string SerializeFragmentIdentifiers(string input)
    {
        StringBuilder res = new StringBuilder();
        List<string> inputCheck = input.Split("").ToList();
        foreach (string letter in inputCheck)
        {
            if (this.alphanumericPattern.IsMatch(input))
            {
                res.Append(letter);
            }
            else
            {
                var encodedChar = WebUtility.UrlEncode(letter.ToString());
                res.Append(encodedChar);
            }
        }
        return res.ToString();
    }

    public bool CheckInputExistsProperty(List<string> list, string input)
    { // If string input exists in collection properties
        if (list.Any(item => item.Contains(input, StringComparison.CurrentCultureIgnoreCase)))
        {
            return true;
        }
        return false;
    }
}