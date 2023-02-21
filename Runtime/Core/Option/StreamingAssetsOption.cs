namespace UGeoDB
{
    public class StreamingAssetsOption : IResourceOption
    {
        public DbFile CountryDb { get; set; }
        public DbFile CityDb { get; set; }

        public StreamingAssetsOption(CityDbType cityDbType, string countryDbName = "countryInfo.txt")
        {
            CountryDb = new DbFile(countryDbName, false);

            string cityDbName = "";

            switch (cityDbType)
            {
                case CityDbType.cities500:
                    cityDbName = "cities500.txt";
                    break;

                case CityDbType.cities1000:
                    cityDbName = "cities1000.txt";
                    break;

                case CityDbType.cities5000:
                    cityDbName = "cities5000.txt";
                    break;

                case CityDbType.cities15000:
                    cityDbName = "cities15000.txt";
                    break;
            }
            CityDb = new DbFile(cityDbName, false);
        }
    }
}