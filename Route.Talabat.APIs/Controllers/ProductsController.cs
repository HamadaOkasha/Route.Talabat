﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.DTOs;
using Route.Talabat.APIs.Errors;
using Route.Talabat.APIs.Helper;
using Route.Talabat.Core.Entities.Product;
using Route.Talabat.Core.IRepositories;
using Route.Talabat.Core.Specifications;

namespace Route.Talabat.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IGenericRepository<ProductCategory> _categoriesRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductBrand> brandsRepo,
            IGenericRepository<ProductCategory> categoriesRepo,
            IMapper mapper)
        {
            _productsRepo = productsRepo;
            _brandsRepo = brandsRepo;
            _categoriesRepo = categoriesRepo;
            _mapper = mapper;
        }

        //[AllowAnonymous]//default any one can use it
        //[Authorize] //must be loginning
        //[Authorize(AuthenticationSchemes ="Application.Identity")]
        //[Authorize(AuthenticationSchemes ="Bearer,Bearer02",Roles ="Admin",Policy ="Amazon")]
        //[Authorize(AuthenticationSchemes ="Bearer")]// ->add schema at program.cs
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//add default at configration at program.cs
        
        [Authorize]
        [HttpGet]
        //Or [HttpPost] without [FromQuery]
        public async Task<ActionResult<IReadOnlyList<Pagination<ProductToReturnDto>>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            //  var products = await _productsRepo.GetAllAsync();

            // var spec = new BaseSpecification<Product>();

            //Specification for Data
            var spec = new ProductWithBrandAndCategorySpecifications(specParams);
            var products = await _productsRepo.GetAllWithSpecAsync(spec);

            // JsonResult result = new JsonResult(products);
            // result.StatusCode=200;
            // return result;

            // OkObjectResult result = new OkObjectResult(products);
            // return result;

            //  return Ok(products);
            // return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));//200

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            var CountSpec = new ProductsWithFilterationForCountSpecifications(specParams);
            var count = await _productsRepo.GetCountAsync(CountSpec/*spec*/);

            return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, count, data));//200

        }
        // [ProducesResponseType(typeof(ProductToReturnDto),200)]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int? id)
        {
            if (!id.HasValue)
                return BadRequest();//400

            //var product = await _productsRepo.GetAsync(id.Value);
            var spec = new ProductWithBrandAndCategorySpecifications(id.Value);
            var product = await _productsRepo.GetWithSpecAsync(spec);

            if (product is null)
                return NotFound(new ApiResponse(404));
            // return NotFound(new {Message="Not Found" ,StatusCode= 404});//404
            // return NotFound();//404

            // return Ok(product);//200
            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));//200
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandsRepo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetCategories()
        {
            var categories = await _categoriesRepo.GetAllAsync();
            return Ok(categories);
        }
    }
}
