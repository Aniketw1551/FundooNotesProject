using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        // Instance variables
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly INotesBL notesBL;
        // Constructor of NotesController
        public NotesController(INotesBL notesBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.notesBL = notesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        // Creation of new note API
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
        // Update note API
        [Authorize]
        [HttpPut("Update")]
        public IActionResult NoteUpdate(long notesId, NotesUpdate notesUpdate)
        {
            try
            {
                var result = notesBL.NoteUpdate(notesId, notesUpdate);
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
        // Get notes by user id api
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
        // Get notes by user id api 
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
        // Get all notes by using redis api
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var cacheKey = "customerList";
            string serializedCustomerList;
            var customerList = new List<Notes>();
            var redisCustomerList = await distributedCache.GetAsync(cacheKey);
            if (redisCustomerList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
                customerList = JsonConvert.DeserializeObject<List<Notes>>(serializedCustomerList);
            }
            else
            {
                customerList = (List<Notes>)notesBL.ViewAllNotes();
                serializedCustomerList = JsonConvert.SerializeObject(customerList);
                redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCustomerList, options);
            }
            return Ok(customerList);
        }
        // Checking notes Archived or not
        [Authorize]
        [HttpPut("IsArchive")]
        public IActionResult NoteArchive(long userId, long notesId)
        {
            try
            {
                var result = notesBL.NoteArchive(userId, notesId);
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
        // Checking notes pinned or not
        [Authorize]
        [HttpPut("IsPin")]
        public IActionResult NotePin(long userId, long notesId)
        {
            try
            {
                var result = (notesBL.NotePin(userId, notesId));
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
        // Checking notes trashed or not
        [Authorize]
        [HttpPut("IsTrash")]
        public IActionResult NoteTrash(long userId, long notesId)
        {
            try
            {
                var result = (notesBL.NoteTrash(userId, notesId));
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
        // Changing color of note api
        [Authorize]
        [HttpPut("Color")]
        public IActionResult NoteColor(long userId, long notesId, String color)
        {
            try
            {
                var result = (notesBL.NoteColor(userId, notesId, color));
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
        // Adding image to note api
        [Authorize]
        [HttpPost("Image")]
        public IActionResult ImageUpload(long userId, long notesId, IFormFile image)
        {
            try
            {
                var result = notesBL.ImageUpload(userId, notesId, image);
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
      