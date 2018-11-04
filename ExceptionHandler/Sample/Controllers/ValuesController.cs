using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            switch (id)
            {
                case int value when value > 0 && value < 100:
                    throw new InvalidAsynchronousStateException("Delegate exception");
                case int value when value > 100 && value < 200:
                    throw new FileNotFoundException("File exception");
                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }
}
