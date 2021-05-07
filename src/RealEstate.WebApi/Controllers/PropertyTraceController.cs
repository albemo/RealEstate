using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Models;
using RealEstate.Domain.ViewModels;
using RealEstate.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.WebApi.Controllers
{
    public class PropertyTraceController : BaseApiController
    {
        private readonly IPropertyTraceService _propertyTraceService;

        public PropertyTraceController(IPropertyTraceService propertyTraceService)
        {
            _propertyTraceService = propertyTraceService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _propertyTraceService.Query()
                .Include(x => x.Property)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return NotFound($"propertytrace with id {id} not found");

            var model = new PropertyTraceViewModel
            {
                Id = item.Id,
                Name = item.Name,
                DateSale = item.DateSale,
                PropertyId = item.PropertyId,
                Property = new PropertyViewModel
                {
                    Id = item.Property.Id,
                    Address = item.Property.Address,
                    CodeInterval = item.Property.CodeInterval,
                    Name = item.Property.Name,
                    OwnerId = item.Property.OwnerId,
                    Price = item.Property.Price,
                    Year = item.Property.Year
                },
                Tax = item.Tax,
                Value = item.Value
            };

            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _propertyTraceService.GetAllAsync();

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PropertyTraceViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = new PropertyTrace
            {
                Name = model.Name,
                PropertyId = model.PropertyId,
                Tax = model.Tax,
                DateSale = model.DateSale,
                Value = model.Value
            };

            await _propertyTraceService.AddAsync(item);

            return Ok();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PropertyTraceViewModel model)
        {
            var item = await _propertyTraceService.GetByIdAsync(id);

            if (item == null)
                return NotFound($"propertytrace with id {id} not found");


            item.Name = model.Name;
            item.PropertyId = model.PropertyId;
            item.Tax = model.Tax;
            item.DateSale = model.DateSale;
            item.Value = model.Value;

            await _propertyTraceService.UpdateAsync(item);

            return Accepted();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _propertyTraceService.GetByIdAsync(id);

            if (item == null)
                return NotFound($"propertytrace with id {id} not found");

            await _propertyTraceService.DeleteAsync(item);

            return Accepted();
        }
    }
}
