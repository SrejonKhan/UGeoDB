# UGeoDB

Simple library to lookup city/country information based on Lat/Long.

# Installation

1. Open Package Manager in Unity and Click on Plus Icon -> Add package from git URL, paste following link

   ```console
   https://github.com/SrejonKhan/UGeoDB.git
   ```

   and click Add. Or, download latest `.unitypackage` and import it manually.

2. Open CopyStaticFiles window (`Window/UGeoDB/CopyStaticFiles`).
3. Select files and copy that are going to be used -

   ![ugeodb-cpystaticfiles-editor.png](https://i.ibb.co/P9vvzVN/ugeodb-cpystaticfiles-editor.png)

### Static File Details

| Name            | Details                          |
| --------------- | -------------------------------- |
| cities500.txt   | Cities with a population > 500   |
| cities1000.txt  | Cities with a population > 1000  |
| cities5000.txt  | Cities with a population > 5000  |
| cities15000.txt | Cities with a population > 15000 |
| countryInfo.txt | All Countries                    |

The following files are from [GeoNames](http://www.geonames.org/), under Creative Commons Attributions [License](http://download.geonames.org/export/#:~:text=cc%2Dby%20licence,usage%20is%20allowed).

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

## OnLoad

Initialization takes time, as it read db files. So, if anything is called just after initialization, it may not work. It's better wait till it's loaded.

```csharp
geoDb.OnLoad += () =>
{
    // ...
};
```

or, you can check if everything is loaded -

```csharp
if(geoDb.IsLoaded)
{
    // ...
}
```

With Coroutine -

```csharp
yield return new WaitUntil(() => geoDb.IsLoaded);
```

## FindNearestCity

Given a GeoCoordinate, nearest city to the coordinate can be found easily -

```csharp
var coord = new GeoCoordinate(23.4607, 91.1809);
var city = geoDb.FindNearestCity(coord);
Debug.Log(city.Name);
```

## FindNearestCities

Given a GeoCoordinate, nearest cities in range (km/mi) to the coordinate can be found easily -

```csharp
var coord = new GeoCoordinate(23.4607, 91.1809);
var cities = geoDb.FindNearestCities(coord, 50, GeoMath.DistanceUnit.Kilometers);
foreach(var city in cities)
{
    Debug.Log(city.Name);
}
```

## GetCountry

To get the Country of a City (`CityInfo`) -

```csharp
var coord = new GeoCoordinate(23.4607, 91.1809);
var city = geoDb.FindNearestCity(coord);
var country = geoDb.GetCountry(city);
Debug.Log(country.CountryName);
```

## Calculate distance between two Geo Coordinates (Haversine Formula)

```csharp
GeoCoordinate coord1 = new GeoCoordinate(23.8103, 90.4125);
GeoCoordinate coord2 = new GeoCoordinate(51.5072, -0.1276);
double distance = GeoMath.HaversineDistance(coord1, coord2, GeoMath.DistanceUnit.Kilometers);
Debug.Log(distance);
```
