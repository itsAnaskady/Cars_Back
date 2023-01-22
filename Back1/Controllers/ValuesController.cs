
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Globalization;
using System.Resources;
using Microsoft.AspNetCore.Mvc;
using Back1.ViewModels;

[Route("api/[controller]")]
[ApiController]
public class PhraseController : ControllerBase
{
    
    [HttpGet("Language_test")]
    public IActionResult GetTranslations(string languageCode)
    {
        var resourceManager = new ResourceManager("Back1.Resources.Resource", typeof(PhraseController).Assembly);
        var cultureInfo = new CultureInfo(languageCode);
        var stringPhrases = new List<Phrase>();
        foreach (var resourceName in resourceManager.GetResourceSet(cultureInfo, true, true).Cast<DictionaryEntry>())
        {
            stringPhrases.Add(new Phrase(){
                value = (string)resourceName.Value,
                 key = (string)resourceName.Key
                });
        }
        return Ok(stringPhrases);
    }
}