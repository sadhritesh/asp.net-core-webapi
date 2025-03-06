using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitsAPIController : ControllerBase
    {

        public List<string> fruits = new List<string>()
            {
                "Banana",
                "Kiwi",
                "Dragon-fruit",
                "papaya",
                "grapes"

            };

        [HttpGet]
         public List<string> GetFruits()
        {
            return fruits;
        }

        [HttpGet("{id}")]
        public string GetFruitsByIndex(int id)
        {
            return fruits.ElementAt(id);
        }

    }
}
