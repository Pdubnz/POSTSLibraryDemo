using POSTSLibrary.Models;

namespace POSTSLibrary.Services
{
    public interface IPointOfSaleTerminal
    {
        ShoppingCartModel Cart { get; set; }

        double CalculateCartTotal();
        void EmptyCart();
        void RemoveItem(char productId);
        void ScanItem(char productId);
    }
}