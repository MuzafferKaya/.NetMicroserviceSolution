using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mongo.Services.ProductAPI.Data;
using Mongo.Services.ProductAPI.Models;
using Mongo.Services.ProductAPI.Models.Dto;

namespace Mongo.Services.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    [Authorize]
    public class ProductAPIController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private ResponseDto _response;

        public ProductAPIController(
            AppDbContext context,
            IMapper mapper
            )
        {
            this._context = context;
            this._mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet]
        [AllowAnonymous]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Product> objList = _context.Products.ToList();
                _response.Results = _mapper.Map<IEnumerable<ProductDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public ResponseDto Get(int id)
        {
            try
            {
                Product obj = _context.Products.First(u => u.ProductId == id);
                _response.Results = _response.Results = _mapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] ProductDto productDto)
        {
            try
            {
                Product obj = _mapper.Map<Product>(productDto);
                _context.Products.Add(obj);
                _context.SaveChanges();
                _response.Results = _response.Results = _mapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] ProductDto productDto)
        {
            try
            {
                Product obj = _mapper.Map<Product>(productDto);
                _context.Products.Update(obj);
                _context.SaveChanges();
                _response.Results = _response.Results = _mapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        public ResponseDto Delete(int id)
        {
            try
            {
                Product obj = _context.Products.First(u => u.ProductId == id);
                _context.Products.Remove(obj);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
