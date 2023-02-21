namespace UGeoDB
{
    public interface IResourceOption
    {
        DbFile CountryDb { get; set; }
        DbFile CityDb { get; set; }
    }
}