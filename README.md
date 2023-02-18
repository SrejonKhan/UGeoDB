# UGeoDB

Simple library to lookup city/country information based on Lat/Long.

# Examples

## Calculate distance between two Geo Coordinates (Haversine Formula)

```csharp
GeoCoordinate coord1 = new GeoCoordinate(23.8103, 90.4125);
GeoCoordinate coord2 = new GeoCoordinate(51.5072, -0.1276);
double distance = GeoMath.HaversineDistance(coord1, coord2, GeoMath.DistanceUnit.Kilometers);
Debug.Log(distance);
```
