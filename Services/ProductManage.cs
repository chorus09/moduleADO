using moduleADO.Models;
namespace moduleADO.Services; 
public class ProductManage {
    public static List<Product> GetProductsFromList(List<List<object>>? data) {
        if (data == null) return [];
        var products = new List<Product>();

        foreach (var item in data) {
            if (item.Count >= 4 &&
                item[0] is int id &&
                item[1] is string code &&
                item[2] is string email &&
                item[3] is string name) {
                products.Add(new Product(id, email, name, code));
            } else return [];
        }
        return products;
    }
}
