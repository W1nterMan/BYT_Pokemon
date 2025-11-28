using System.Reflection;
using TestProject6.BYT_Pokemon.Models;

namespace TestProject6.BYT_Pokemon.Test;

public class BadgeTest
{
    [SetUp]
    public void Setup()
    {
        var field = typeof(Badge).GetField("_extent", BindingFlags.Static | BindingFlags.NonPublic);
        field.SetValue(null, new List<Badge>());
    }

    [Test]
    public void Badge_Attributes()
    {
        var badge = new Badge("BraveBadge", "Orange");
        Assert.That(badge.Name, Is.EqualTo("BraveBadge"));
        Assert.That(badge.Color, Is.EqualTo("Orange"));
    }

    [Test]
    public void Badge_InvalidAttributes()
    {
        var badge = new Badge();

        Assert.Throws<ArgumentException>(() => badge.Name = "");
        Assert.Throws<ArgumentException>(() => badge.Name = null!);
        Assert.Throws<ArgumentException>(() => badge.Color = "");
        Assert.Throws<ArgumentException>(() => badge.Color = null!);
    }

    [Test]
    public void Badge_Extent()
    {
        var braveBadge = new Badge("BraveBadge", "Orange");
        var superFancyBadge = new Badge("SuperFancyBadge", "Rainbow");

        var extent = Badge.GetBadges();
        Assert.That(extent.Count, Is.EqualTo(2));
        Assert.That(extent.Exists(b => b.Name == "BraveBadge"));
        Assert.That(extent.Exists(b => b.Name == "SuperFancyBadge"));
    }

    [Test]
    public void Badge_Encapsulation()
    {
        var badge = new Badge("ForestBadge", "Green");

        var extent = Badge.GetBadges();
        badge.Name = "BurntForest";
        badge.Color = "Black";

        var updatedExtent = Badge.GetBadges();
        Assert.That(updatedExtent.First().Name, Is.EqualTo("BurntForest"));
        Assert.That(updatedExtent.First().Color, Is.EqualTo("Black"));
    }

    [Test]
    public void Badge_Persistence()
    {
        string path = "badges_test.xml";
        if (File.Exists(path)) File.Delete(path);

        var forestBadge = new Badge("ForestBadge", "Green");
        var burntForestBadge = new Badge("BurntForestBadge", "Black");

        Badge.Save(path);

        var field = typeof(Badge).GetField("_extent", BindingFlags.Static | BindingFlags.NonPublic);
        field.SetValue(null, new List<Badge>());

        bool loadSuccess = Badge.Load(path);
        Assert.IsTrue(loadSuccess);

        var loadedExtent = Badge.GetBadges();
        Assert.That(loadedExtent.Count, Is.EqualTo(2));
        Assert.That(loadedExtent.Exists(b => b.Name == "ForestBadge" && b.Color == "Green"));
        Assert.That(loadedExtent.Exists(b => b.Name == "BurntForestBadge" && b.Color == "Black"));
    }
}
