# UGeoDB

Simple library to lookup city/country information based on Lat/Long.

# Usage

## Initialize

To initialize with default option (local / streaming assets) -

```csharp
var option = new StreamingAssetsOption(CityDbType.cities500);
var geodb = new GeoDB(option);
```

In case, we can initialize with custom option. Any class that implements `IResourceOption` can be used as option. For example, if we want to read DB files from remote, firstly create a class that implements `IResourceOption`-

```csharp
public class RemoteOption : IResourceOption
{
    public DbFile CountryDb { get; set; }
    public DbFile CityDb { get; set; }

    public RemoteOption()
    {
        CountryDb = new DbFile(path: "https://example.com/country_db.txt", isRemote: true);
        CityDb = new DbFile(path: "https://example.com/city_db.txt", isRemote: true);
    }
}
```

Now, initialize with that -

```csharp
var remoteOption = new RemoteOption();
var geodb = new GeoDB(remoteOption);
```

## Calculate distance between two Geo Coordinates (Haversine Formula)

```csharp
GeoCoordinate coord1 = new GeoCoordinate(23.8103, 90.4125);
GeoCoordinate coord2 = new GeoCoordinate(51.5072, -0.1276);
double distance = GeoMath.HaversineDistance(coord1, coord2, GeoMath.DistanceUnit.Kilometers);
Debug.Log(distance);
```
