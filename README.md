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

### Original problem statement

We need Web API number ordering solution. This solution should have 2 endpoints:

1. We can pass line of numbers from 1 - 10 (few can be skipped) and these numbers should be ordered and saved to file (for ex. result.txt). For ex. we pass 5 2 8 10 1, this file should be saved with following content: 1 2 5 8 10

2. We should be able to load content of latest saved file


Requirements:

1. Latest .NET project

2. Business service(s) with unit tests

3. Sorting algorithm should be written yourself for ex. bubble sort (it would be nice if this algorithm would be able to handle any size of numbers not only 1 to 10). You can use AI tools if you want as help for this part, but sorting code should be in this project (not some library).

4. Put source code in GIT

5. Use best software engineering practices


Bonus points:

1. Multiple sorting algorithms are used, and time performance is measured between them.

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
