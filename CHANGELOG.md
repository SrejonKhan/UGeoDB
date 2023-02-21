## [0.1.1] - 21-02-2023

Patch fix, that fixed critical IOException issue.

### Fixed

- Fixed (#1), IOException when copying Static files from Package folder to Project's StreamingAssets folder.

## [0.1.0] - 21-02-2023

This is the first release of the library, which includes the ability to find city and country information. Users can search for a city from a coordinate and get information such as the city's name, population, latitude and longitude, timezone and more. Users can also get country information from a city info.

This release marks an important milestone for the library, as it provides a strong foundation for future development. In upcoming releases, we plan to add more features, including the ability to search city and country from input string. And provide more methods to get organized information or do meaningful operation on informations. We also plan to improve the performance and accuracy of the library to make it more useful for a wide range of applications.

### Added

- Reading DB files from local (streaming assets) and remote.
- Editor Tools for copying DB Files from Package folder to Project's StreamingAssets folder.
- Lookup methods (`FindNearestCity()`, `FindNearestCities()`) for City.
- Lookup methods (`GetCountry()`) for Country.
- Calculating distance between two coordinate (using Haversine Method)
