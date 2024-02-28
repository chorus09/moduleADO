
namespace moduleADO.Models.Observer; 
public class ProductUpdater : IProductObserver {
    private Action<Product> _updateDataProduct;
    private object _updatedValue;
    public ProductUpdater(Action<Product> updateDataProduct) {
        _updateDataProduct = updateDataProduct;
    }
    public void Update(Product product, object updatedValue) {
        _updateDataProduct?.Invoke(product);
    }
    public object GetValue() => _updatedValue;
}
