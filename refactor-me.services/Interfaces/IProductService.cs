using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using refactor_me.services.Models;

namespace refactor_me.services.Interfaces
{
     public interface IProductService
    {
        IEnumerable<ProductModel> GetAll();

        IEnumerable<ProductModel> SearchByName(string name);

        ProductModel GetProduct(Guid id);

        void Create(ProductModel product);

        void Update(Guid id, ProductModel product);

        void Delete(Guid id);

        IEnumerable<ProductOptionModel> GetOptions(Guid productId);

        ProductOptionModel GetOption(Guid productId, Guid id);

        void CreateOption(Guid productId, ProductOptionModel option);

        void UpdateOption(Guid id, ProductOptionModel option);

        void DeleteOption(Guid productId, Guid id);

    }
}
