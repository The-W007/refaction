using System;
using System.Collections.Generic;

namespace refactor_me.data.Interfaces
{
    public interface IProductDataService
    {
        IEnumerable<Product> GetProducts();

        IEnumerable<Product> SearchByName(string name);

        Product GetProduct(Guid id);

        void Create(Product product);

        void Update(Product product);

        void Delete(Guid id);

        IEnumerable<ProductOption> GetOptions(Guid productId);

        ProductOption GetOption(Guid productId, Guid id);

        void CreateOption(Guid productId, ProductOption option);

        void UpdateOption(ProductOption option);

        void DeleteOption(Guid productId, Guid id);
    }
}
