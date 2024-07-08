using BusinessLayer.DTO;
using DataAccessLayer.Models;
using DataAccessLayer.Reposatries.OrderReposatry;
using DataAccessLayer.Reposatries.ProductReposatry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace productManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {
        private readonly IProduct _product;
        private readonly IOrder _order;

        public productController(IProduct product)
        {
            this._product = product;
        }

        [HttpGet("GetAllProducts")]
        public ActionResult<resultDTO> GetAllProducts()
        {
            List<product> products = _product.GetAll();
            List<productDTO> productsDTO = new List<productDTO>();

            foreach (product product in products)
            {
                productsDTO.Add(new productDTO
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    OrderId = product.OrderId
                });
            }

            resultDTO result = new resultDTO();
            result.IsPass = productsDTO.Count != 0 ? true : false;
            result.Data = productsDTO;
            return result;
        }

        [HttpGet("ProductById/{id}")]
        public ActionResult<resultDTO> productById(int id ) { 

            product product = _product.GetById( id );
            resultDTO result = new resultDTO();


            if (product != null)
            {
                productDTO productDto = new productDTO()
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    OrderId = product.OrderId
                };


                result.IsPass = productDto != null ? true : false;
                result.Data = productDto;
            }
            else
            {
                result.IsPass = false;
                result.Data = "Not Found";
            }
           
            return result;
        }

        [HttpPost("AddProduct")]
        public ActionResult<resultDTO> AddProduct(productDTO productDTO)
        {
            resultDTO result = new resultDTO();
            if (ModelState.IsValid)
            {
                try
                {
                    product product = new product()
                    {
                        Name = productDTO.Name,
                        Description = productDTO.Description,
                        Price = productDTO.Price,
                        Stock = productDTO.Stock
                    };
                    _product.insert(product);
                    _product.save();
                    result.IsPass = true;
                    result.Data = $"Created successfully";
                }
                catch (Exception ex)
                {
                    result.IsPass = false;
                    result.Data = "An error occurred while creating the product.";
                }
            }
            else
            {
                result.IsPass = false;
                result.Data = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage).ToList();
            }
            return result;
        }

        [HttpPut("EditProduct")]
        public ActionResult<resultDTO> EditProduct(productDTO productDTO)
        {
            resultDTO result = new resultDTO();
            if (ModelState.IsValid)
            {
                try
                {
                    product product  = _product.GetById(productDTO.ProductId);
                    if (product != null)
                    {
                        product.Name = productDTO.Name;
                        product.Description = productDTO.Description;
                        product.Price = productDTO.Price;
                        product.Stock = productDTO.Stock;
                        _product.update(product);
                        _product.save();
                        result.IsPass = true;
                        result.Data = "Updated Successfully";
                    }
                    else
                    {
                        result.IsPass = false;
                        result.Data = "product not found";
                    }
                }
                catch (Exception ex)
                {
                    result.IsPass = false;
                    result.Data = "An error occurred during update product data";
                }
            }
            else
            {
                result.IsPass = false;
                result.Data = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage).ToList();
            }
            return result;
        }

        [HttpDelete("DeleteProduct/{id}")]
        public ActionResult<resultDTO> DeleteProduct(int id)
        {
            resultDTO result = new resultDTO();
            product product = _product.GetById(id);

            if (product == null)
            {
                result.IsPass = false;
                result.Data = "product not Found";
            }
            else
            {
                _product.Delete(id);
                _product.save();
                result.IsPass = true;
                result.Data = "product Has been deleted successfully";
            }
            return result;

        }

    }
}
