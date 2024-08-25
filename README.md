# SortingApi

### Introduction

This project implements a simple number ordering API.

### Clone the repository

```bash
git clone https://github.com/lu-kup/sorting-api.git
cd sorting-api
```

### Running the application

In order to run the application, use the following command while in the `sorting-api` directory:

```bash
dotnet run --project SortingApi
```

In order to test the endpoints, use Swagger interface at `https://localhost:7212/swagger/index.html`

The API will be available at `https://localhost:7212` by default. 

If the URL does not open due to a missing HTTPS certificate, use the following command to generate a self-signed certificate to enable HTTPS use in local development.

```bash
dotnet dev-certs https --trust
```

### Unit tests

In order to run unit tests, use the following command while in directory `sorting-api`.
```bash
dotnet test
```
