using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRTutorial.Hubs
{
    /*
        STOCK MANAGER
    */
    public class StockManager
    {
        private readonly static Lazy<StockManager> _instance = new Lazy<StockManager>(() => new StockManager(GlobalHost.ConnectionManager.GetHubContext<StockHub>().Clients));
        private IHubConnectionContext<dynamic> _clients { get; set; }
        public static StockManager Instance { get { return _instance.Value; } }

        private StockManager(IHubConnectionContext<dynamic> clients)
        {
            _clients = clients;
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            throw new NotImplementedException();
        }

        private void BroadcastStock(Stock stock)
        {
            _clients.All.updateStock(stock);
        }
    }

    /* 
        STOCK MODEL 
    */
    public class Stock
    {
        public int High { get; set; }
        public int Low { get; set; }
    }

    /*
        STOCK HUB
    */
    public class StockHub : Hub
    {
        private readonly StockManager _manager;

        public StockHub() : this(StockManager.Instance) { }

        public StockHub(StockManager manager)
        {
            _manager = manager;
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            return _manager.GetAllStocks();
        }
    }
}