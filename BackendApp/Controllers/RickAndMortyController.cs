using System.Collections.Generic;
using System.IO;
using BackendApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Linq;

namespace BackendApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RickAndMortyController : ControllerBase
    { 
        private readonly ILogger<RickAndMortyController> _logger;
        private readonly List<Character> _charactersList;
        public RickAndMortyController(ILogger<RickAndMortyController> logger)
        {
            _logger = logger;

            //This is just to get data from file, 
            using StreamReader r = new StreamReader("Database/DataFile.json");
            string json = r.ReadToEnd();
            var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
            CharactersCollection item = JsonSerializer.Deserialize<CharactersCollection>(json, options);
            _charactersList = item.CharactersList;
        }

        /// <summary>
        /// Gets characters name based on search keyword
        /// </summary>
        /// <param name="keyWord">serach keyword</param>
        /// <returns>Collection of sugestions</returns>
        [HttpGet("Suggestions")]
        public IEnumerable<string> Suggestions([FromQuery]string keyWord)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                return null;
            }
            return _charactersList
                .Where(x => x.Name.Contains(keyWord, System.StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(y => y.Id)
                .Select(z => z.Name)
                .Take(5)
                .Distinct();
            
        }

        /// <summary>
        /// Gets list of all characters based on name
        /// </summary>
        /// <param name="name">Character name</param>
        /// <returns>Collection of characters</returns>
        [HttpGet("GetCharacters/{name}")]
        public IEnumerable<Character> GetCharacters(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            return _charactersList
                .Where(x => x.Name.Contains(name, System.StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(y => y.Id);
        }
    }
}
