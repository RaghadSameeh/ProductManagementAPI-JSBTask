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
    public class orderController : ControllerBase
    {

        private readonly IProduct _product;
        private readonly IOrder _order;

        public orderController(IProduct product, IOrder order)
        {
            this._product = product;
            this._order = order;
        }

        [HttpGet("GetAllOrders")]
        public ActionResult<resultDTO> GetAllOrders()
        {
            List<order> orders = _order.GetAll();
            List<orderDTO> ordersDTO = new List<orderDTO>();

            foreach (order order in orders)
            {
                List<productDTO> productsDTO = new List<productDTO>();

                foreach (product product in order.Products)
                {
                    productsDTO.Add(new productDTO
                    {
                        ProductId = product.ProductId,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Stock = product.Stock,
                        OrderId = order.OrderId
                    });
                }

                ordersDTO.Add(new orderDTO
                {
                    OrderId = order.OrderId,
                    CustomerId = order.CustomerId,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    Products = productsDTO

                });
            }

            resultDTO result = new resultDTO();
            result.IsPass = ordersDTO.Count != 0 ? true : false;
            result.Data = ordersDTO;
            return result;
        }

        [HttpGet("orderById/{id}")]
        public ActionResult<resultDTO> orderById(string id)
        {

            order order = _order.GetById(id);
            resultDTO result = new resultDTO();
            List<productDTO> productDTOs = new List<productDTO>();


            if (order != null)
            {
                if (order.Products != null)
                {
                    foreach (product product in order.Products)
                    {
                        productDTOs.Add(new productDTO()
                        {
                            ProductId = product.ProductId,
                            Name = product.Name,
                            Description = product.Description,
                            Price = product.Price,
                            Stock = product.Stock,
                            OrderId = order.OrderId

                        });
                    }

                }
                orderDTO orderDTO = new orderDTO()
                {
                   OrderId= order.OrderId,
                   CustomerId = order.CustomerId,
                   OrderDate = order.OrderDate,
                   TotalAmount = order.TotalAmount,
                   Products = productDTOs

                };


                result.IsPass = orderDTO != null ? true : false;
                result.Data = orderDTO;
            }
            else
            {
                result.IsPass = false;
                result.Data = "Not Found";
            }

            return result;
        }

        [HttpPost("AddOrder")]
        public ActionResult<resultDTO> AddOrder(orderDTO orderDTO)
        {
            resultDTO result = new resultDTO();
            if (ModelState.IsValid)
            {
                try
                {
                    List<product> products = new List<product>();
                    if (orderDTO != null)
                    {
                        if (orderDTO.Products != null)
                        {
                            foreach(productDTO productDTO in orderDTO.Products)
                            {
                                product existingProduct = _product.GetById(productDTO.ProductId);

                                if (existingProduct != null)
                                {
                                    products.Add(existingProduct);
                                }
                            } 
                            
                        }
                    }
                    string orderid = Guid.NewGuid().ToString();
                    order order = new order()
                    {
                       OrderId = orderid,
                       CustomerId = orderDTO.CustomerId,
                       OrderDate = orderDTO.OrderDate,
                       TotalAmount = orderDTO.TotalAmount,
                       Products = products

                    };
                    _order.insert(order);
                    _order.save();

                    //update foriegnkey in products
                    foreach (product product in products)
                    {
                        product.OrderId = orderid;
                        _product.update(product);
                        _product.save();
                    }

                    result.IsPass = true;
                    result.Data = $"Created successfully";
                }
                catch (Exception ex)
                {
                    result.IsPass = false;
                    result.Data = "An error occurred while creating the order.";
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

        [HttpPut("EditOrder")]
        public ActionResult<resultDTO> EditOrder(orderDTO orderDTO)
        {
            resultDTO result = new resultDTO();
            if (ModelState.IsValid)
            {
                try
                {
                    order order = _order.GetById(orderDTO.OrderId);
                    if (order != null)
                    {
                        //productsInOrder
                        List<product> products = new List<product>();
                        if (orderDTO != null)
                        {
                            if (orderDTO.Products != null)
                            {
                                foreach (productDTO productDTO in orderDTO.Products)
                                {
                                    products.Add(new product()
                                    {
                                        ProductId = productDTO.ProductId,
                                        Name = productDTO.Name,
                                        Description = productDTO.Description,
                                        Price = productDTO.Price,
                                        Stock = productDTO.Stock,
                                        OrderId = orderDTO.OrderId,
                                    });
                                }

                            }
                        }
                        //end
                        order.CustomerId = orderDTO.CustomerId;
                        order.OrderDate = orderDTO.OrderDate;
                        order.TotalAmount = orderDTO.TotalAmount;
                        order.Products = products;

                        _order.update(order);
                        _order.save();
                        result.IsPass = true;
                        result.Data = "Updated Successfully";
                    }
                    else
                    {
                        result.IsPass = false;
                        result.Data = "order not found";
                    }
                }
                catch (Exception ex)
                {
                    result.IsPass = false;
                    result.Data = "An error occurred during update order data";
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

        [HttpDelete("DeleteOrder/{id}")]
        public ActionResult<resultDTO> DeleteProduct(string id)
        {
            resultDTO result = new resultDTO();
            order order = _order.GetById(id);

            if (order == null)
            {
                result.IsPass = false;
                result.Data = "order not Found";
            }
            else
            {
                _order.Delete(id);
                _order.save();
                result.IsPass = true;
                result.Data = "order Has been deleted successfully";
            }
            return result;

        }

    }
}
