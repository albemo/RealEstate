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
    public class PropertyController : BaseApiController
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _propertyService.Query()
                .Include(x => x.Owner)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return NotFound($"property with id {id} not found");

            var model = new PropertyViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Address = item.Address,
                CodeInterval = item.CodeInterval,
                OwnerId = item.OwnerId,
                Price = item.Price,
                Year = item.Year,
                Owner = new OwnerViewModel
                {
                    Id = item.Owner.Id,
                    Photo = item.Owner.Photo,
                    Address = item.Owner.Address,
                    Bithday = item.Owner.Bithday,
                    Name = item.Owner.Name
                }
            };

            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _propertyService.GetAllAsync();

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PropertyViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = new Property
            {
                Address = model.Address,
                Name = model.Name,
                CodeInterval = model.CodeInterval,
                Year = model.Year,
                Price = model.Price,
                OwnerId = model.OwnerId
            };

            await _propertyService.AddAsync(item);

            return Ok();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PropertyViewModel model)
        {
            var item = await _propertyService.GetByIdAsync(id);

            if (item == null)
                return NotFound($"property with id {id} not found");

            item.Address = model.Address;
            item.Name = model.Name;
            item.OwnerId = model.OwnerId;
            item.Price = model.Price;
            item.Year = model.Year;
            item.CodeInterval = model.CodeInterval;

            await _propertyService.UpdateAsync(item);

            return Accepted();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _propertyService.GetByIdAsync(id);

            if (item == null)
                return NotFound($"property with id {id} not found");

            await _propertyService.DeleteAsync(item);

            return Accepted();
        }
    }
}
