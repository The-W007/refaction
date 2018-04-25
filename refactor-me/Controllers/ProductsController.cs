using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using refactor_me.Models;
using refactor_me.services;
using refactor_me.services.Models;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private readonly ProductService _productsService;
        public ProductsController(ProductService productsService)
        {
            _productsService = productsService;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        [Route]
        [HttpGet]
        public CollectionsDto<ProductDto> GetAll()
        {
            try
            {
                return ConvertToProductsDto(_productsService.GetAll());
            }
            catch (Exception)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to get products"));
            }
        }

        /// <summary>
        /// Search products by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route]
        [HttpGet]
        public CollectionsDto<ProductDto> SearchByName(string name)
        {
            try
            {
                return ConvertToProductsDto(_productsService.SearchByName(name));
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to return products"));
            }

        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        public ProductDto GetProduct(Guid id)
        {
            try
            {
                return ConvertToProductDto(_productsService.GetProduct(id));
            }
            catch (NullReferenceException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message));
                
            }
            catch (DataException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict, e.Message));

            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to get product"));
                
            }
          
        }

        /// <summary>
        /// create new product
        /// </summary>
        /// <param name="productDto"></param>
        [Route]
        [HttpPost]
        public void Create(ProductDto productDto)
        {
            try
            {
                _productsService.Create(ConvertToProductModel(productDto));
            }
            catch (ArgumentException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message));

            }
            catch (Exception)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to create product"));

            }


        }

        /// <summary>
        /// Update product by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productDto"></param>
        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, ProductDto productDto)
        {
            try
            {
                _productsService.Update(id, ConvertToProductModel(productDto));

            }
            catch (ArgumentException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message));

            }
            catch (NullReferenceException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message));

            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to update product"));

            }

        }

        /// <summary>
        /// delete product by id
        /// </summary>
        /// <param name="id"></param>
        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            try
            {
                _productsService.Delete(id);
            }
            catch (NullReferenceException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message));

            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to delete product"));

            }
           
        }

        /// <summary>
        /// get options by product Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Route("{productId}/options")]
        [HttpGet]
        public CollectionsDto<ProductOptionDto> GetOptions(Guid productId)
        {
            try
            {
                return ConvertToProductOptionssDto(_productsService.GetOptions(productId));
            }
            catch (NullReferenceException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message));

            }
            catch (DataException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict, e.Message));

            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "failed to get options"));

            }

        }

        /// <summary>
        /// Get product option by id
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOptionDto GetOption(Guid productId, Guid id)
        {
            try
            {
                return ConvertoProductOptionDto(_productsService.GetOption(productId, id));
            }
            catch (NullReferenceException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message));

            }
            catch (DataException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict, e.Message));

            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "failed to get option"));

            }

        }

        /// <summary>
        /// create new product option 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="option"></param>
        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOptionDto option)
        {
            try
            {
                _productsService.CreateOption(productId, ConvertoProductOptionModel(option));
            }
            catch (ArgumentException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message));

            }
            catch (NullReferenceException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message));

            }
            catch (DataException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict, e.Message));

            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "failed to create option"));

            }
         
        }

        /// <summary>
        /// Update new options by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="option"></param>
        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid id, ProductOptionDto option)
        {
            try
            {
                _productsService.UpdateOption(id, ConvertoProductOptionModel(option));
            }
            catch (ArgumentException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message));

            }
            catch (NullReferenceException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message));

            }
            catch (DataException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict, e.Message));

            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "failed to update option"));

            }

        }

        /// <summary>
        /// delete options by id
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="id"></param>
        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid productId, Guid id)
        {
            try
            {
                _productsService.DeleteOption(productId, id);
            }
            catch (NullReferenceException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message));

            }
            catch (DataException e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict, e.Message));

            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "failed to delete option"));
            }

        }

        private CollectionsDto<ProductDto> ConvertToProductsDto(IEnumerable<ProductModel> products)
        {
            var productList = products.Select(ConvertToProductDto);

           return new CollectionsDto<ProductDto>()
           {
              Items = productList.ToList()
           };
        }

        private ProductDto ConvertToProductDto(ProductModel product)
        {
            return new ProductDto()
            {
                Id = product.Id,
                DeliveryPrice = product.DeliveryPrice,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price
            };
        }

        private ProductModel ConvertToProductModel(ProductDto product)
        {
            return new ProductModel()
            {
                Description = product.Description,
                DeliveryPrice = product.DeliveryPrice,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price

            };

        }

        private CollectionsDto<ProductOptionDto> ConvertToProductOptionssDto(IEnumerable<ProductOptionModel> options)
        {
            var optionList = options.Select(ConvertoProductOptionDto);

            return new CollectionsDto<ProductOptionDto>
            {
                Items = optionList.ToList()
            };
        }

        private ProductOptionDto ConvertoProductOptionDto(ProductOptionModel option)
        {
            return new ProductOptionDto()
            {
                Description = option.Description,
                Id = option.Id,
                Name = option.Name,
                ProductId = option.ProductId
            };

        }

        private ProductOptionModel ConvertoProductOptionModel(ProductOptionDto option)
        {
            return new ProductOptionModel()
            {
                Description = option.Description,
                Id = option.Id,
                Name = option.Name,
                ProductId = option.ProductId
            };

        }

    }
}
