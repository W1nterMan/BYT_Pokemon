using System.Xml;
using System.Xml.Serialization;

namespace Models;

public class Serializer
{
    public static void Save<T>(string path, List<T> extent)
    {
        // Pokemon <- bin <- debug <- net8.0 (3 .. needed to go up for solution)
        string solutionDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        string dataFolder = Path.Combine(solutionDir, "Data");
        
        Directory.CreateDirectory(dataFolder);
        
        string filePath = Path.Combine(dataFolder, path);
        
        using var file = File.CreateText(filePath);
        var serializer = new XmlSerializer(typeof(List<T>));

        using var writer = new XmlTextWriter(file)
        {
            Formatting = Formatting.Indented //so it saves everything not in one line of .xml
        };
        
        serializer.Serialize(writer,extent);
    }

    public static bool Load<T>(string path,  List<T> extent)
    {
        // Pokemon <- bin <- debug <- net8.0 (3 .. needed to go up for solution)
        string solutionDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        string dataFolder = Path.Combine(solutionDir, "Data");
        string filePath = Path.Combine(dataFolder, path);
        
        extent = new List<T>();

        if (!File.Exists(filePath))
        {
            return false;
        }

        try
        {
            using var file = File.OpenText(filePath);
            var serializer = new XmlSerializer(typeof(List<T>));

            using var reader = new XmlTextReader(file);

            extent = (List<T>)serializer.Deserialize(reader);
            return true;
        }
        catch (Exception e)
        {
            extent.Clear();
            return false;
        }
    }
}