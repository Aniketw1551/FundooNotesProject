using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Model;
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
    public class NotesController : ControllerBase
    {
        private readonly INotesBL notesBL;
        //Constructor of NotesController
        public NotesController(INotesBL notesBL)
        {
            this.notesBL = notesBL;
        }
        //Creation of new note API
        [Authorize]
        [HttpPost("Create")]
        public IActionResult NoteCreation(NotesCreation notesCreation)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var result = notesBL.NoteCreation(userId, notesCreation);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Notes Created successfully", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Notes Creation Unsuccessful" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Update note API
        [Authorize]
        [HttpPut("Update")]
        public IActionResult NoteUpdate(long NotesId, NotesUpdate notesUpdate)
        {
            try
            {
                var result = notesBL.NoteUpdate(NotesId, notesUpdate);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Notes updated successfully", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Error no note found" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Delete Note API
        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult DeleteNote(long notesId)
        {
            try
            {
                if (notesBL.DeleteNote(notesId))
                    return this.Ok(new { Success = true, message = "Notes Deleted successfully" });
                else
                    return this.BadRequest(new { Success = false, message = "Failed to delete note" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet("{Id}/Get")]
        public IEnumerable<Notes> ViewNotesByUserId()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var result = notesBL.ViewNotesByUserId(userId);
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
        [HttpGet("ViewAll")]
        public List<Notes> ViewAllNotes()
        {
            try
            {
                var result = notesBL.ViewAllNotes();
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
        [HttpPut("IsArchive")]
        public IActionResult NoteArchive(long userId, long NotesId)
        {
            try
            {
                var result = notesBL.NoteArchive(userId, NotesId);
                if (result != null)
                    return this.Ok(new { Success = true, message = "checking note archived or not", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Error" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("IsPin")]
        public IActionResult NotePin(long userId, long NotesId)
        {
            try
            {
                var result = (notesBL.NotePin(userId, NotesId));
                if (result != null)
                    return this.Ok(new { Success = true, message = "Cheking note pinned or not", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Error" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("IsTrash")]
        public IActionResult NoteTrash(long userId, long NotesId)
        {
            try
            {
                var result = (notesBL.NoteTrash(userId, NotesId));
                if (result != null)
                    return this.Ok(new { Success = true, message = "Cheking note trashed or not", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Error" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("Color")]
        public IActionResult NoteColor(long userId, long NotesId, String Color)
        {
            try
            {
                var result = (notesBL.NoteColor(userId, NotesId, Color));
                if (result != null)
                    return this.Ok(new { Success = true, message = "Notes color changed successfully", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Failed to change the color of the note" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPost("Image")]
        public IActionResult ImageUpload(long userId, long NotesId, IFormFile Image)
        {
            try
            {
                var result = notesBL.ImageUpload(userId, NotesId, Image);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Image uploaded successfully", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Error while uploading image. Please try again" });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
      