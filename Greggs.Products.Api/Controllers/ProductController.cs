using System;
using System.Linq;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IDataAccess<Product> _dataAccess;
    private readonly ILogger<ProductController> _logger;

    public ProductController(
        IDataAccess<Product> dataAccess,
        ILogger<ProductController> logger)
    {
        _dataAccess = dataAccess;
        _logger = logger;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Get(int pageStart = 0, int pageSize = 5)
    {
        try
        {
            var products = _dataAccess.List(pageStart, pageSize);

            if (products.Any())
            {
                return Ok(products);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Products Get method failed.");
        }

        return BadRequest("Error: No products found for query params");
    }
}