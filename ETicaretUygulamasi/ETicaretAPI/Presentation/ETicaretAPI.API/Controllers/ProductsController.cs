using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Persistence.Repositories;
using ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;

        readonly private IOrderWriteRepository _orderWriteRepository;
        readonly private ICustomerWriteRepository _customerWriteRepository;

        public ProductsController(
            IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository,
            IOrderWriteRepository orderWriteRepository,
            ICustomerWriteRepository customerWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
        }
        [HttpGet]
        public async Task Get()
        {
            var customerId=Guid.NewGuid();
            await _customerWriteRepository.AddAsync(new() { Id = customerId, Name="Betül" });
            await _orderWriteRepository.AddAsync(new() { Description = "bla bla", Adress = "Ankara, Çankaya",CustomerId =customerId });
            await _orderWriteRepository.AddAsync(new() { Description = "bla bla 2", Adress = "Ankara, Pursaklar",  CustomerId = customerId
            });
            await _orderWriteRepository.SaveAsync();
        }
    }
}
