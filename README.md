# Algora Coding task

## How to Run

run the script called *RunDemo.ps1*

```powershell
./RunDemo.ps1
```

## Requirement
as
The task is to create a WPF front-end, in C#, that displays simulated stock prices ticking (prices for each stock is provided, below):
* Stock 1, random stock prices between 240 and 270 and
* Stock 2, random stock prices between 180 and 210.

Prices should tick every second. Price moves up should have a green background, price moves down should have a red background and no price change should have a white background.
Double-clicking a stock should display the price change history by time.

### Sample screen layout for the history screen

In a list view (tabular format) with the following columns, example layout:
Date/Time	Price
1 Oct 2015 13:05:21	240.00
1 Oct 2015 13:05:22	245.90
…	…

The columns should be formatted, as above.
### Project Design
1. The project should be separated into Client module and Service module (Assemblies).  Client module should be WPF
2. Client should only subscribe with a ticker i.e. Stock1, and the service will keep publishing prices for that ticker for as long as it is subscribed.
3. Service module will act as a provider and publish prices based on the price logic to the client.
4. Design the project in such a way that the client module can use different implementations of Service module without changing the client code (loading during runtime).

