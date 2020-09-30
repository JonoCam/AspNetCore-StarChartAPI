using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        //[HttpGet("{id:int}","GetById")]
        public IActionResult GeyById(int id)
        {
            CelestialObject celestialObject =  _context.CelestialObjects.FirstOrDefault(e => e.Id == id);
            if (celestialObject == null)
                return NotFound();

            foreach (var obj in _context.CelestialObjects)
            {
                if (obj.OrbitedObjectId == id)
                    obj.Satellites.Add(celestialObject);
            }

            return Ok(celestialObject);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            CelestialObject celestialObject = _context.CelestialObjects.FirstOrDefault(e => e.Name == name);
            if (celestialObject == null)
                return NotFound();

            foreach (var obj in _context.CelestialObjects)
            {
                if (obj.OrbitedObjectId == celestialObject.Id)
                    obj.Satellites.Add(celestialObject);
            }

            return Ok(celestialObject);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.CelestialObjects);
        }
    }
}
