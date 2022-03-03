# TradeConsole
Console Application that reads Binance wallet information. 

This is a simple console app. It uses a class library "Bianance Integration". 

This class library outputs strings of JSON for each binance endpoint. 

The Console App, TradeCOnsoleUI uses a Nuget Package called "SpectreConsole" to render text based tables and information from Binance.



## Installation instructions!

This app was written in .NET6 using C#.

You *must* create two Environment Variables "BiananceAPISecret" and "BinanceAPIKey" in the User Directory.
secret = Environment.GetEnvironmentVariable("BinanceAPISecret", EnvironmentVariableTarget.User)
