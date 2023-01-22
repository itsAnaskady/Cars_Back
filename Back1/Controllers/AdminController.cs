using Back1.Models;
using Back1.Services;
using Back1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        private IAdminService _adminService;
        private ApplicationDbContext _context;

        public AdminController(IAdminService adminService, ApplicationDbContext context)
        {
            _adminService = adminService;
            _context = context;
        }

        [HttpGet("ListeFavori")]
        public async Task<IEnumerable<UserGetViewModel>> GetListeFavori()
        {
            var ListeFavori = await _adminService.GetListFavoriAsync();
            return ListeFavori;
        }

        [HttpGet("ListeNoir")]
        public async Task<IEnumerable<UserGetViewModel>> GetListeNoir()
        {
            var ListeNoir = await _adminService.GetListNoirAsync();
            return ListeNoir;
        }
        [HttpGet("ListeMarque")]
        public async Task<IEnumerable<string>> GetAllmarques()
        {
            var ListeMarque = await _adminService.GetAllMarks();
            return ListeMarque;
        }


        [HttpGet("ListeUsers")]
        public async Task<IEnumerable<UserGetViewModel>> GetAllUsers()
        {
            var ListeUsers = await _adminService.GetAllUsers();
            return ListeUsers;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _adminService.GetById(id);
            if (product == null)
                return NotFound();
            await _adminService.Delete(product);
            return Ok();
        }
        [HttpDelete("DeleteVoiture/{id}")]
        public async Task<IActionResult> DeleteVoiture(int id)
        {
            var voiture = _context.Voitures.Where(x => x.Id == id).SingleOrDefault();
            if (voiture == null)
                return NotFound();
            await _adminService.DeleteVoiture(voiture);
            return Ok();
        }


        [HttpGet("ListeVoiture")]
        public async Task<ActionResult<IEnumerable<VoitureViewModel>>> GetAllVoiture()
        {
            var ListeV = await _adminService.GetAllVoitures();
            if(ListeV !=null)
                return Ok(ListeV);
            else
                return BadRequest("Error : Liste vide");
        }

        [HttpPost("AjouterVoiture")]
        public async Task<IActionResult> RegisterAsync([FromBody] PostVoitureViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.PostVoiture(model);
                await _context.SaveChangesAsync();
                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200

                return BadRequest(result);
            }

            return BadRequest("Some proprieties are not valid"); //Status code: 400
        }

        [HttpGet("listeNomProprietaire")]
        public async Task<ActionResult<IEnumerable<string>>> getAllProprietaires()
        {
            var ListeP = await _adminService.GetAllProprietaires();
            if (ListeP != null)
                return Ok(ListeP);
            else
                return BadRequest("Error : Liste vide");
        }
        [HttpGet("NombreVoitures")]
        public async Task<ActionResult<int>> GetNombreVoitures()
        {
            var nb = await _adminService.GetNombreVoitures();
            if (nb != null)
                return Ok(nb);
            else
                return BadRequest("No data found");
        }
        [HttpGet("NombreClient")]
        public async Task<ActionResult<int>> GetNombreClients()
        {
            var nb = await _adminService.GetNombreClients();
            if (nb != null)
                return Ok(nb);
            else
                return BadRequest("No data found");
        }
        [HttpGet("NombreReservation")]
        public async Task<ActionResult<int>> GetNombreReservations()
        {
            var nb = await _adminService.GetNombreReservation();
            if (nb != null)
                return Ok(nb);
            else
                return BadRequest("No data found");
        }
        [HttpGet("NombrePlainte")]
        public async Task<ActionResult<int>> GetNombrePlainte()
        {
            var nb = await _adminService.GetNombrePlainte();
            if (nb != null)
                return Ok(nb);
            else
                return BadRequest("No data found");
        }
        
        [HttpPut("addToFavori/{id}")]
        public async Task<ActionResult<UserManagerResponse>> addToListFavori(string id)
        {
            var model = await _adminService.addToListeFavori(id);
            if (model != null)
                return Ok(model);
            // Return a 204 No Content response
            return NoContent();
        }
        [HttpPut("addToNoir/{id}")]
        public async Task<ActionResult<UserManagerResponse>> addToListNoir(string id)
        {
            var model = await _adminService.addToListeNoir(id);
            if (model != null)
                return Ok(model);
            // Return a 204 No Content response
            return NoContent();
        }
        [HttpPut("RemovefromNoir/{id}")]
        public async Task<ActionResult<UserManagerResponse>> RemoveFromListNoir(string id)
        {
            var model = await _adminService.RemoveFromListeNoir(id);
            if (model != null)
                return Ok(model);
            // Return a 204 No Content response
            return NoContent();
        }
        [HttpPut("RemoveFromFavori/{id}")]
        public async Task<ActionResult<UserManagerResponse>> RemoveFromListFavori(string id)
        {
            var model = await _adminService.RemoveFromListeFavori(id);
            if (model != null)
                return Ok(model);
            // Return a 204 No Content response
            return NoContent();
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserGetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.UpdateUser(model, id);
                if (result)
                    return Ok();
                else
                    return BadRequest();
            }
            return BadRequest();
        }
        [HttpGet("AllCategories")]
        public async Task<ActionResult<IEnumerable<string>>> allcatego()
        {
            var liste = await _adminService.AllCategories();
            if (liste != null)
                return Ok(liste);
            return BadRequest("liste not found");
        }
        [HttpGet("UserIDByEmail/{email}")]
        public async Task<ActionResult<string>> getEmailByID(string email)
        {
            var Id = await _adminService.GetIdbyEmail(email);
            if (Id != null)
                return Ok(Id);
            return BadRequest("liste not found");
        }

    }
}
