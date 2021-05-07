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
    public class PropertyImageController : BaseApiController
    {
        private readonly IPropertyImageService _propertyImageService;

        public PropertyImageController(IPropertyImageService propertyImageService)
        {
            _propertyImageService = propertyImageService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _propertyImageService.Query()
                .Include(x => x.Property)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return NotFound($"propertyimage with id {id} not found");

            var model = new PropertyImageViewModel
            {
                Id = item.Id,
                Enabled = item.Enabled,
                File = item.File,
                PropertyId = item.PropertyId,
                Property = new PropertyViewModel
                {
                    Id = item.Property.Id,
                    Address = item.Property.Address,
                    Name = item.Property.Name,
                    CodeInterval = item.Property.CodeInterval,
                    OwnerId = item.Property.OwnerId,
                    Price = item.Property.Price,
                    Year = item.Property.Year
                }
            };

            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _propertyImageService.GetAllAsync();

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PropertyImageViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = new PropertyImage
            {
                PropertyId = model.PropertyId,
                File = model.File,
                Enabled = model.Enabled
            };

            await _propertyImageService.AddAsync(item);

            return Ok();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PropertyImageViewModel model)
        {
            var item = await _propertyImageService.GetByIdAsync(id);

            if (item == null)
                return NotFound($"propertyimage with id {id} not found");


            item.PropertyId = model.PropertyId;
            item.File = model.File;
            item.Enabled = model.Enabled;

            await _propertyImageService.UpdateAsync(item);

            return Accepted();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _propertyImageService.GetByIdAsync(id);

            if (item == null)
                return NotFound($"propertyimage with id {id} not found");

            await _propertyImageService.DeleteAsync(item);

            return Accepted();
        }
    }
}
