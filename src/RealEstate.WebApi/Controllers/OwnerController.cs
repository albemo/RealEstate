using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstate.Domain.Models;
using RealEstate.Domain.ViewModels;
using RealEstate.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.WebApi.Controllers
{
    public class OwnerController : BaseApiController
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _ownerService.Query()
                .Include(x => x.Properties)
                    //.ThenInclude(x => x.PropertyTraces)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return NotFound($"owner with id {id} not found");

            var model = new OwnerViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Address = item.Address,
                Photo = item.Photo,
                Bithday = item.Bithday,
                Properties = item.Properties.Select(x => new PropertyViewModel
                {
                    Id = x.Id,
                    Address = x.Address,
                    CodeInterval = x.CodeInterval,
                    Name = x.Name,
                    Price = x.Price,
                    Year = x.Year
                }).ToList()
            };

            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _ownerService.GetAllAsync();

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OwnerViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = new Owner
            {
                Address = model.Address,
                Bithday = model.Bithday,
                Name = model.Name,
                Photo = model.Photo
            };

            await _ownerService.AddAsync(item);

            return Ok();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OwnerViewModel model)
        {
            var item = await _ownerService.GetByIdAsync(id);

            if (item == null)
                return NotFound($"owner with id {id} not found");

            item.Address = model.Address;
            item.Bithday = model.Bithday;
            item.Name = model.Name;
            item.Photo = model.Photo;

            await _ownerService.UpdateAsync(item);

            return Accepted();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _ownerService.GetByIdAsync(id);

            if (item == null)
                return NotFound($"owner with id {id} not found");

            await _ownerService.DeleteAsync(item);

            return Accepted();
        }
    }
}
