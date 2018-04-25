using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using refactor_me.data;
using refactor_me.data.Interfaces;
using refactor_me.services.Interfaces;
using refactor_me.services.Models;

namespace refactor_me.services
{
    public class ProductService : IProductService
    {
        private readonly IProductDataService _productDataService;

        public ProductService(IProductDataService productDataService)
        {
            _productDataService = productDataService;
        }


        public IEnumerable<ProductModel> GetAll()
        {
            return _productDataService.GetProducts().Select(ConvertToProductModel);
        }
        
        public IEnumerable<ProductModel> SearchByName(string name)
        {
            return _productDataService.SearchByName(name.ToLower()).Select(ConvertToProductModel);
        }

        public ProductModel GetProduct(Guid id)
        {
            try
            {
                var output = _productDataService.GetProduct(id);
                if (output == null) throw new NullReferenceException("Product not found");
                return ConvertToProductModel(output);
            }
            catch (InvalidOperationException e)
            {
               throw new DataException("Too many Products found");
            }
        }

        public void Create(ProductModel product)
        {
            if (string.IsNullOrEmpty(product.Name)) throw new ArgumentException("Product name cannot be null", 
                nameof(product));

            product.Id = Guid.NewGuid();

            _productDataService.Create(ConvertToProductEntity(product));
        }

        public void Update(Guid id, ProductModel product)
        {
            if (string.IsNullOrEmpty(product.Name)) throw new ArgumentException("Product name cannot be null",
                nameof(product));

            if (_productDataService.GetProduct(id) == null) throw new NullReferenceException("Product not found");

            product.Id = id;
             _productDataService.Update(ConvertToProductEntity(product));
        }

        public void Delete(Guid id)
        {
            try
            {
                _productDataService.Delete(id);
            }
            catch (InvalidOperationException e)
            {
                throw new NullReferenceException("Product not found");
            }
            
        }

        public IEnumerable<ProductOptionModel> GetOptions(Guid productId)
        {
            //make sure product exists
            GetProduct(productId);
            
            return _productDataService.GetOptions(productId).Select(ConvertToProductOptionModel);
          
            
        }

        public ProductOptionModel GetOption(Guid productId, Guid id)
        {
            //make sure product exists
            GetProduct(productId);

            try
            {
                return ConvertToProductOptionModel(
                    _productDataService.GetOption(productId, id));
            }
            catch (InvalidOperationException e)
            {
                throw new NullReferenceException("Product option not found");
            }
        }

        public void CreateOption(Guid productId, ProductOptionModel option)
        {
            //make sure product exists
            GetProduct(productId);

            if (string.IsNullOrEmpty(option.Name)) throw new ArgumentException("Option name cannot be null",
                nameof(option));


            option.Id = Guid.NewGuid();

            option.ProductId = productId;

            _productDataService.CreateOption(productId, ConvertToProductOptionEntity(option));
        }

        public void UpdateOption(Guid id, ProductOptionModel option)
        {
            if (string.IsNullOrEmpty(option.Name)) throw new ArgumentException("Option name cannot be null",
                nameof(option));

            option.Id = id;
            _productDataService.UpdateOption(ConvertToProductOptionEntity(option));
        }

        public void DeleteOption(Guid productId,Guid id)
        {
            //make sure product exists
            GetProduct(productId);

            try
            {
                _productDataService.DeleteOption(productId, id);
            }
            catch (InvalidOperationException e)
            {
                throw new NullReferenceException("Product Option not found");
            }
        }



        private ProductModel ConvertToProductModel(Product product)
        {
            return new ProductModel()
            {
                DeliveryPrice = product.DeliveryPrice,
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

      

        private Product ConvertToProductEntity(ProductModel product)
        {
            return new Product()
            {
                Id = product.Id,
                DeliveryPrice = product.DeliveryPrice,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price
            };
        }

        private ProductOptionModel ConvertToProductOptionModel(ProductOption productOption)
        {
            return new ProductOptionModel()
            {
                Description = productOption.Description,
                Id = productOption.Id,
                Name = productOption.Name,
                ProductId = productOption.ProductId
            };
        }

        private ProductOption ConvertToProductOptionEntity(ProductOptionModel productOption)
        {
            return new ProductOption()
            {
                Description = productOption.Description,
                Id = productOption.Id,
                Name = productOption.Name,
                ProductId = productOption.ProductId
            };
        }


    }
}
