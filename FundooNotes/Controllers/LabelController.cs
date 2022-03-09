using BusinessLayer.Interface;
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
    public class LabelController : ControllerBase
    {
        //instance variables
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly ILabelBL labelBL;
        //Constructor of LabelController
        public LabelController(ILabelBL labelBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.labelBL = labelBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        //Create Label API
        [Authorize]
        [HttpPost("Create")]
        public IActionResult CreateLabel(long notesId, string labelName)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var result = labelBL.CreateLabel(userId, notesId, labelName);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Label created successfully", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Error while creating Label" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Update Label API
        [Authorize]
        [HttpPut("Update")]
        public IActionResult UpdateLabel(string labelName, long notesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var result = labelBL.UpdateLabel(labelName, notesId, userId);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Label updated successfully", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Error while updating Label" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Remove Label API
        [Authorize]
        [HttpDelete("Remove")]
        public IActionResult RemoveLabel(long labelId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                if (labelBL.Removelabel(userId, labelId))
                    return this.Ok(new { Success = true, message = "Label removed successfully" });
                else
                    return this.BadRequest(new { Success = false, message = "Failed to remove Label. Please try again" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Get label by notes id api
        [Authorize]
        [HttpGet("Get")]
        public IEnumerable<Labels> ViewLabelsByNotesId(long NotesId)
        {
            try
            {
                var result = labelBL.ViewLabelsByNotesId(NotesId);
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
        //Get label by user id api
        [Authorize]
        [HttpGet("{Id}/Id")]
        public IEnumerable<Labels> ViewLabelsByUserId()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var result = labelBL.ViewLabelsByUserId(userId);
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
        //Get all labels api
        [Authorize]
        [HttpGet("ViewAll")]
        public List<Labels> ViewAllLabels()
        {
            try
            {
                var result = labelBL.ViewAllLabels();
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
        //Get all labels using redis api
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var cacheKey = "customerList";
            string serializedCustomerList;
            var customerList = new List<Labels>();
            var redisCustomerList = await distributedCache.GetAsync(cacheKey);
            if (redisCustomerList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
                customerList = JsonConvert.DeserializeObject<List<Labels>>(serializedCustomerList);
            }
            else
            {
                customerList = (List<Labels>)labelBL.ViewAllLabels();
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

