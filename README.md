# WildFire App 🔥

This is a simple web application that consumes data from https://openmaps.gov.bc.ca/
via HTTP and displays response based on parameters passed.

Front end of the application is built with Blazor Pages in C#.

### Requirements

- Visual Studio 2022
- .Net 8.0
- Docker Desktop: Installed and configured to run on WSL2, with a working Distribution (Debian Preferred)

### Dependencies

- CsvHelper (31.0.2)
- Newtonsoft.Json (13.0.3)

### Build Instructions

Download WildFireApp Solution at a location where
write permissions are available

### Building the Image

```shell
docker build -t wildfireapp:latest -f ./WildFireApp.Web/Dockerfile .
```

### Running / Deploying the Docker Image

```shell
docker run -p 8080:8080 -p 8081:8081 --rm wildfireapp:latest --name wildfireapp
```

Normally you would build and publish the Docker image to
a registry, and you would pull it before running in the Server
Environment.
