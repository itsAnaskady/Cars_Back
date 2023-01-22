using Back1.Models;
using Back1.Services;
using Back1.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Back1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocataireController : ControllerBase
    {
        private ILocataireService _LocataireService;
        private ApplicationDbContext _context;

        public LocataireController(ILocataireService LocataireService, ApplicationDbContext context)
        {
            _LocataireService = LocataireService;
            _context = context;
        }

        [HttpGet("GetAllVoitureDispo")]
        public async Task<ActionResult<IEnumerable<VoitureViewModel>>> GetAllVoitureDispo()
        {
            var ListeDispo = await _LocataireService.GetAllVoituresDispo();
            if(!ListeDispo.IsNullOrEmpty())
                return Ok(ListeDispo);
            return BadRequest("No data Found");
        }
        [HttpGet("GetAllVoitureNonDispo")]
        public async Task<ActionResult<IEnumerable<VoitureViewModel>>> GetAllVoitureNonDispo()
        {
            var ListenDispo = await _LocataireService.GetAllVoituresNonDispo();
            if (!ListenDispo.IsNullOrEmpty())
                return Ok(ListenDispo);
            return BadRequest("No data Found");
        }
        
        [HttpGet("GetAllModePaiement")]
        public async Task<ActionResult<IEnumerable<ModePaiement>>> GetAllModePaiement()
        {
            var Liste = await _LocataireService.GetAllModePaiement();
            if (!Liste.IsNullOrEmpty())
                return Ok(Liste);
            return BadRequest("No data Found");
        }
  
        [HttpPost("AjouterReservation")]
        public async Task<IActionResult> AjouterReservationAsync([FromBody]ReservationForm model)
        {
            if (ModelState.IsValid)
            {
                var result = await _LocataireService.AjouterReservationAsync(model);
                if (result != null)
                    return Ok(result); // Status Code: 200

                return BadRequest(result);
            }
            return BadRequest("Some proprieties are not valid");
        }

        [HttpGet("AllReservations")]
        public async Task<IActionResult> allReservation()
        {
            var Liste = await _LocataireService.GetAllReservations();
            if (!Liste.IsNullOrEmpty())
                return Ok(Liste);
            return BadRequest("No data Found");
        }
        
        [HttpGet("AllReservationsByIdPro/{id}")]
        public async Task<IActionResult> allReservationByIdProp(string id)
        {
            var Liste = await _LocataireService.GetAllReservationsByIdProp(id);
            if (!Liste.IsNullOrEmpty())
                return Ok(Liste);
            return BadRequest("No data Found");
        }
        [HttpGet("AllReservationsByIdLocataire/{id}")]
        public async Task<IActionResult> allReservationByIdLoca(string id)
        {
            var Liste = await _LocataireService.GetAllReservationsByIdLocataire(id);
            if (!Liste.IsNullOrEmpty())
                return Ok(Liste);
            return BadRequest("No data Found");
        }
    }
}
