using TradeConsole.ExchangeData;
using TradeConsole.ExchangeOps;
using Newtonsoft.Json;
using System.Text;
using TradeConsole.UI;
using Spectre.Console;

BinanceOps _binanceOps = new BinanceOps();
BinanceObjects _binanceObjects = new BinanceObjects();
BinanceMethods _binanceMethods = new BinanceMethods();
TradingContext _context = new TradingContext();
List<BinanceObjects.Balances> balances = new();
ConsoleUI _consoleUI = new ConsoleUI(); 

balances = await _binanceMethods.GetBalances(false);

AnsiConsole.Markup("Here are your current balances:\n");
_consoleUI.AccountBalanceTable(balances);

bool quit = false;

while (quit == false)
{
    Console.WriteLine("Available Commands: 'position'/ 'p' returns information on your current position in an asset  'history'/ 'h' returns trade history for an asset 'b' for all wallet balances  Enter 'e' to exit");
    string command = Console.ReadLine();
    // Status bar for waiting to hear back from both of these calls:....
    if (command == "e")
    {
        quit = true;
    }
    else if (command == "history" || command == "h") 
    {
        AnsiConsole.Write("Enter an Asset in this format: LTCUSD \n");
        string asset = Console.ReadLine();
        string findAssetBal = _binanceMethods.GetSpecificBalance(balances, asset);
        await _consoleUI.GetTradeHistoryTable(asset.ToUpper(), findAssetBal);
    }
    else if (command == "position" || command == "p")
    {
        AnsiConsole.Write("Enter an Asset in this format: LTCUSD \n");
        string asset = Console.ReadLine();
        string findAssetBal = _binanceMethods.GetSpecificBalance(balances, asset.ToUpper());
        await _consoleUI.PositionInfo(asset.ToUpper(), findAssetBal);
    }
    else if(command == "balances" || command=="b")
    {
        _consoleUI.AccountBalanceTable(balances);
    }
    else if(command == "c")
    {
        Console.Clear();
    }
    else if(command == "time")
    {

    }
    else
    {
        AnsiConsole.Write("[red]That's not a command[/]");
    }
    
}

/*
 * 
 * THIS IS ALL BULL SHIT
 * 
async Task GetAssetMenu(string asset)
{
    BinanceMethods binanceMethods = new();
    List<BinanceObjects.TradeHistoryEntry> tradeHistories = await binanceMethods.TradeHistory(asset);


    BinanceObjects.DailyPriceHistory dailyPriceHistory = new();
    dailyPriceHistory = await binanceMethods.GetDailyPrice(asset);
    // create table method for this!!
    _consoleUI.PositionInfo(dailyPriceHistory);

    var lastTrade = (from trade in tradeHistories
                     where trade.isBuyer == true
                     orderby trade.TimeStamp descending
                     select trade)
                    .FirstOrDefault();

    var priceDiff = Convert.ToDecimal(dailyPriceHistory.lastPrice) - Convert.ToDecimal(lastTrade.Price);

    StringBuilder mainMenuOutput = new StringBuilder();
    mainMenuOutput.Append(System.Environment.NewLine);
    mainMenuOutput.Append($"Current Price information for {asset} \n");
    mainMenuOutput.Append($"Last Price: ${dailyPriceHistory.lastPrice} | 24H change: ${dailyPriceHistory.priceChange} ({dailyPriceHistory.priceChangePercent}%) \n");
    mainMenuOutput.Append($"Purchase Price: ${lastTrade.Price}, Quantity: {lastTrade.Quantity} \n");
    mainMenuOutput.Append($"Profit per coin: ${priceDiff} \n");
    mainMenuOutput.Append($"Profet per position: ${priceDiff * Convert.ToDecimal(lastTrade.Quantity)} \n");
    mainMenuOutput.Append($"24 Hour High: {dailyPriceHistory.highPrice}  24 hour Low: {dailyPriceHistory.lowPrice} \n");
    
    Console.Write(mainMenuOutput.ToString());
    //\n 
    // Get 24 hour price data
    // Get Trade History - most recent purchase?
    // Calculate profit / loss

    // future: add buy/sell feature
}
*/