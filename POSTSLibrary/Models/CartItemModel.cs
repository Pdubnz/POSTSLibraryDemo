namespace POSTSLibrary.Models
{
    public class CartItemModel
    {
        public ProductModel _product;
        public int Count { get; set; } = 1;
        public CartItemModel(ProductModel product)
        {
            this._product = product;
        }

    }
}