using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Inventory;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(IInventoryService inventoryService, ILogger<InventoryController> logger)
        {
            _inventoryService = inventoryService;
            _logger = logger;
        }

        [HttpGet("/api/inventory")]
        public ActionResult GetCurrentInventory()
        {
            _logger.LogInformation("Getting all inventory...");

            var inventory = _inventoryService.GetCurrentInventory()
                .Select(x => new ProductInventoryViewModel
                {
                    Id = x.Id,
                    Product = ProductMapper.SerializeProductViewModel(x.Product),
                    IdealQuantity = x.IdealQuantity,
                    QuantityOnHand = x.QuantityOnHand
                })
                .OrderBy(x => x.Product.Name)
                .ToList();

            return Ok(inventory);
        }
        
        [HttpPatch("/api/inventory")]
        public ActionResult UpdateInventory([FromBody] ShipmentViewModel shipmentViewModel)
        {
            _logger.LogInformation($"Updating inventory for {shipmentViewModel.ProductId} - Adjustment: {shipmentViewModel.Adjustment}.");

            var id = shipmentViewModel.ProductId;
            var adjustment = shipmentViewModel.Adjustment;

            var inventory = _inventoryService.UpdateUnitsAvailable(id, adjustment);
            return Ok(inventory);
        }

    }
}