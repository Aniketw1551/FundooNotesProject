//-----------------------------------------------------------------------
// <copyright file="NotesController.cs" company="Aniket">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooNotes.Controllers
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    /// <summary>
    /// Notes API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        ////Instance variables

        /// <summary>The memory cache</summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>The distributed cache</summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>The notes BL</summary>
        private readonly INotesBL notesBL;
        ////Constructor of NotesController

        /// <summary>Initializes a new instance of the <see cref="NotesController" /> class.</summary>
        /// <param name="notesBL">The notes BL.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public NotesController(INotesBL notesBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.notesBL = notesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        /// <summary>Notes the creation.</summary>
        /// <param name="notesCreation">The notes creation.</param>
        /// <returns>
        /// Create Note API
        /// </returns>
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


        /// <summary>Notes the update.</summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="notesUpdate">The notes update.</param>
        /// <returns>
        ///  Notes Update API
        /// </returns>
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


        /// <summary>Deletes the note.</summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>
        /// Delete Notes APi
        /// </returns>
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

        /// <summary>Views the notes by user identifier.</summary>
        /// <returns>
        /// Get Notes By user id API
        /// </returns>
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

        /// <summary>Views all notes.</summary>
        /// <returns>
        /// Get All notes API
        /// </returns>
        [Authorize]
        [HttpGet("GetAll")]
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

        /// <summary>Gets all customers using redis cache.</summary>
        /// <returns>
        ///  Get Notes By redis API
        /// </returns>
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

        /// <summary>Notes the archive.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>
        ///  Checking Notes Archived API
        /// </returns>
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

        /// <summary>Notes the pin.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>
        ///   Checking Notes Pinned API
        /// </returns>
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

        /// <summary>Notes the trash.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>
        ///   Checking Notes Trashed API
        /// </returns>
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

        /// <summary>Notes the color.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>
        ///   Changing Notes Color API
        /// </returns>
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

        /// <summary>Images the upload.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>
        /// Add Image to Note API
        /// </returns>
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
      