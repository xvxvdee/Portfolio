using System;
namespace inputToMongoDB.Serivce;
public class InputToMongoDBService
{
    public bool CheckIdRange(int id, long size)
    { //Ensure int Id exists in colection
        if (id > size || id < 1)
        {
            return false;
        }
        return true;
    }

    // Serialize inputs with fragmentidentifiers
    //http://localhost:5500/deandra_spike-madden/resume/experience/skills/C%23
    public bool CheckInputExistsProperty(List<string> list, string input)
    { // If string input exists in collection properties
        if (list.Any(item => item.Contains(input, StringComparison.CurrentCultureIgnoreCase)))
        {
            return true;
        }
        return false;
    }
}