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
        [HttpPost("CreateNote")]
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
        [HttpPut("UpdateNote")]
        public IActionResult NoteUpdate(long notesId, NotesUpdate notesUpdate)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var result = notesBL.NoteUpdate(userId, notesId, notesUpdate);
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
        [HttpDelete("DeleteNote")]
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
        [HttpGet("ViewNotesByUserId")]
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
        [HttpGet("ViewAllNotes")]
        public IEnumerable<Notes>ViewAllNotes()
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
    }
}