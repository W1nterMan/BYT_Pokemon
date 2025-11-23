using Models;

namespace Test;

public class LocationTest
{
    [Test]
    public void Location_Persistence_And_ComplexAttribute_Test()
    {
        string testPath = "test_locations.xml";
        if (File.Exists(testPath)) File.Delete(testPath);
        
        var loc1 = new Location("Town 1", 10, 20, LocationType.Town);
        var loc2 = new Location("Village 1", 50, 50, LocationType.Village);
        
        Assert.IsTrue(Location.GetExtent().Count >= 2);
        
        Location.save(testPath);
        
        bool loaded = Location.load(testPath);
        
        Assert.IsTrue(loaded);
        
        var loadedExtent = Location.GetExtent();
        var loadedPallet = loadedExtent.FirstOrDefault(l => l.Name == "Town 1");

        Assert.IsNotNull(loadedPallet);
        
        Assert.IsNotNull(loadedPallet.Coords);
        Assert.AreEqual(10, loadedPallet.Coords.X);
        Assert.AreEqual(20, loadedPallet.Coords.Y);
        
        if (File.Exists(testPath)) File.Delete(testPath);
    }
}