using Models;

namespace TestProject6.BYT_Pokemon.Models;

[Serializable]
public class Badge
{
    private static List<Badge> _extent = new List<Badge>();
    private string _name = string.Empty;
    private string _color = string.Empty;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Invalid badge name.");
            _name = value;
        }
    }

    public string Color
    {
        get => _color;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Invalid badge color.");
            _color = value;
        }
    }

    public Badge()
    {
        AddBadge(this);
    }

    public Badge(string name, string color)
    {
        Name = name;
        Color = color;
        AddBadge(this);
    }
    
    private static void AddBadge(Badge badge)
    {
        if (badge == null)
            throw new ArgumentException("Badge cannot be null.");
        _extent.Add(badge);
    }
    
    public static List<Badge> GetBadges()
    {
        return _extent;
    }

    public static void Save(string path = "badges.xml")
    {
        Serializer.Save(path, _extent);
    }

    public static bool Load(string path = "badges.xml")
    {
        return Serializer.Load(path, _extent);
    }
    
}