﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APi_version.Models;

namespace APi_version.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserModelsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ApplicationUserModelsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/ApplicationUserModels
        [HttpGet]
        public IEnumerable<ApplicationUserModel> GetApplicationUserModel()
        {
            return _context.ApplicationUserModel;
        }

        // GET: api/ApplicationUserModels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationUserModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationUserModel = await _context.ApplicationUserModel.FindAsync(id);

            if (applicationUserModel == null)
            {
                return NotFound();
            }

            return Ok(applicationUserModel);
        }

        // PUT: api/ApplicationUserModels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationUserModel([FromRoute] int id, [FromBody] ApplicationUserModel applicationUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationUserModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(applicationUserModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ApplicationUserModels
        [HttpPost]
        public async Task<IActionResult> PostApplicationUserModel([FromBody] ApplicationUserModel applicationUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ApplicationUserModel.Add(applicationUserModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicationUserModel", new { id = applicationUserModel.Id }, applicationUserModel);
        }

        // DELETE: api/ApplicationUserModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationUserModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationUserModel = await _context.ApplicationUserModel.FindAsync(id);
            if (applicationUserModel == null)
            {
                return NotFound();
            }

            _context.ApplicationUserModel.Remove(applicationUserModel);
            await _context.SaveChangesAsync();

            return Ok(applicationUserModel);
        }

        private bool ApplicationUserModelExists(int id)
        {
            return _context.ApplicationUserModel.Any(e => e.Id == id);
        }
    }
}