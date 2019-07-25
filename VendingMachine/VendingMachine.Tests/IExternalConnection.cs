namespace VendingMachine.Tests
{
    public interface IExternalConnection
    {
        void StockNotificationFor(int productId);
    }
}