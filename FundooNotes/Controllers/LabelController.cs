//-----------------------------------------------------------------------
// <copyright file="LabelController.cs" company="Aniket">
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
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Entity;

    /// <summary>
    /// Collab API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        ////Instance variables

        /// <summary>The memory cache</summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>The distributed cache</summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>The label BL</summary>
        private readonly ILabelBL labelBL;
        ////Constructor of LabelController
        
        /// <summary>Initializes a new instance of the <see cref="LabelController" /> class.</summary>
        /// <param name="labelBL">The label BL.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public LabelController(ILabelBL labelBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.labelBL = labelBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        /// <summary>Creates the label.</summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <returns>
        /// Create Label API
        /// </returns>
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

        /// <summary>Updates the label.</summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>
        /// Update Label API
        /// </returns>
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

        /// <summary>Removes the label.</summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>
        /// Remove Label API
        /// </returns>
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

        /// <summary>Views the labels by notes identifier.</summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>
        /// Get Labels By Notes Id API
        /// </returns>
        [Authorize]
        [HttpGet("Get")]
        public IEnumerable<Labels> ViewLabelsByNotesId(long notesId)
        {
            try
            {
                var result = labelBL.ViewLabelsByNotesId(notesId);
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

        /// <summary>Views the labels by user identifier.</summary>
        /// <returns>
        /// Get Labels By user Id API
        /// </returns>
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

        /// <summary>Views all labels.</summary>
        /// <returns>
        /// Get All Labels API
        /// </returns>
        [Authorize]
        [HttpGet("GetAll")]
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

        /// <summary>Gets all customers using redis cache.</summary>
        /// <returns>
        /// Redis API
        /// </returns>
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