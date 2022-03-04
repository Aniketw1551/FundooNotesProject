using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        //instance variables
        private readonly ICollabBL collabBL;
        //Constructor of NotesController
        public CollabController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
        }
        [Authorize]
        [HttpPost("Create")]
        public IActionResult CreateCollab(long notesId, string email)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var result = collabBL.CreateCollab(userId,notesId,email);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Collab created successfully", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Error while creating collad" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet("{Id}/Get")]
        public IEnumerable<Collaborator> ViewCollabByNotesId(long NotesId)
        {
            try
            {
                var result = collabBL.ViewCollabByNotesId(NotesId);
                if (result != null)
                    return result;
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpDelete("Remove")]
        public IActionResult RemoveCollab(long CollabId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var result = collabBL.RemoveCollab(userId, CollabId);
                if(result!=null)
                    return this.Ok(new { Success = true, message = "Collab removed successfully" });
                else
                    return this.BadRequest(new { Success = false, message = "Failed to remove collab. Please try again" });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}