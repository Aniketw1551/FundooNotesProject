using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Entity;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        ////Instance variables
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly ICollabBL collabBL;
        ////Constructor of CollabController
        public CollabController(ICollabBL collabBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.collabBL = collabBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        // Create collab api
        
        [Authorize]
        [HttpPost("Create")]
        public IActionResult CreateCollab(long notesId, string email)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var result = collabBL.CreateCollab(userId, notesId, email);
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
        // Remove collab api

        [Authorize]
        [HttpDelete("Remove")]
        public IActionResult RemoveCollab(long collabId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var result = collabBL.RemoveCollab(userId, collabId);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Collab removed successfully", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Failed to remove collab. Please try again" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        // Get collabs by notes id api

        [Authorize]
        [HttpGet("{Id}/Get")]
        public IEnumerable<Collaborator> ViewCollabByNotesId(long notesId)
        {
            try
            {
                var result = collabBL.ViewCollabByNotesId(notesId);
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
        // Get all Collabs api

        [Authorize]
        [HttpGet("ViewAll")]
        public List<Collaborator> ViewAllCollaborators()
        {
            try
            {
                var result = collabBL.ViewAllCollaborators();
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
        // Get all collab by redis api

        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var cacheKey = "customerList";
            string serializedCustomerList;
            var customerList = new List<Collaborator>();
            var redisCustomerList = await distributedCache.GetAsync(cacheKey);
            if (redisCustomerList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
                customerList = JsonConvert.DeserializeObject<List<Collaborator>>(serializedCustomerList);
            }
            else
            {
                customerList = (List<Collaborator>)collabBL.ViewAllCollaborators();
                serializedCustomerList = JsonConvert.SerializeObject(customerList);
                redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCustomerList, options);
            }
            return Ok(customerList);
        }
    }
}