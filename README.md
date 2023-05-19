# WeatherAPI

The Weather API is a RESTful web service that provides weather information based on the IP address of the request originator. It leverages non-commercial, third-party services to perform geolocation search and retrieve current weather conditions for the detected location.

## Features

* **Weather Information:** The API allows you to retrieve the current weather information based on the IP address of the request originator.
* **Geolocation Search:** The service performs a geolocation search using a non-commercial, third-party IP to location provider.
* **Third-Party Integration:** It integrates with non-commercial, third-party services to fetch weather data based on the detected location.
* **Resilience:** The implementation is designed to handle cases where the third-party services are unavailable, ensuring robustness and reliability.
* **Data Storage:** The API stores data from the third-party providers in a database, allowing historical analysis of queries and weather conditions.
* **Caching:** An in-memory cache is used as the first layer in data retrieval to optimize performance and reduce the load on the third-party services.
* **Test Coverage:** The project aims for a test coverage of at least 80% to ensure the reliability and correctness of the implemented functionality.
* **Database Schema Versioning:** The database schema is versioned, enabling seamless updates and maintenance of the application.
