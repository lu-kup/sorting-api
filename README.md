# SortingApi

### Introduction

This project implements a simple number ordering API. The input numbers are passed as a single string in query parameters. The ordering result is then saved to a text file. Only positive integer values are supported.

Multiple sorting algorithms are implemented:
- Bubble sort
- Insertion sort
- Selection sort
- Quick sort
- Merge sort

The user is able to select one of the algorithms when calling the `POST /api/sorting` endpoint by providing an enum value, which is specified by its name (e.g., `"QuickSort"`, `"MergeSort"`, `"BubbleSort"`).

Performance time is displayed along with the sorting result, and the performance can be compared between all implemented algorithms by calling the endpoint `POST /api/sorting/all-algorithms`.

The sorting endpoints save the sorted result to a file (e.g., `Result_08-31_08:51:33.txt`). The files are saved to the directory `Infrastructure/Data`. The latest saved result can be loaded by calling the endpoint `GET /api/sorting`.

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

In order to test the endpoints, use Swagger interface at `https://localhost:7212/swagger/index.html`.

The API will be available at `https://localhost:7212` by default. 

If the URL does not open due to a missing HTTPS certificate, use the following command to generate a self-signed certificate to enable HTTPS use in local development.

```bash
dotnet dev-certs https --trust
```

### Unit tests

In order to run unit tests, use the following command while in the directory `sorting-api`.
```bash
dotnet test
```
