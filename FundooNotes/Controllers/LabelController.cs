using BussinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
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
        private readonly ILabelBL labelBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;
        public LabelController(ILabelBL labelBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.labelBL = labelBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
        }
        [HttpPost("Create")]
        public IActionResult CreateLabel(LabelModel labelModel)
        {
            try
            {
                
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);

                if (this.labelBL.CreateLabel(labelModel))
                {

                    return this.Ok(new { status = true, isSuccess = true, Message = "Label created successfully!"});
                }
                else
                {
                    return this.BadRequest(new { status = false, isSuccess = false, Message = "Label not created" });
                }
                
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }
        }
        [HttpGet("Detail")]
        public IActionResult GetlabelByNotesId(long NotesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var notesid = this.labelBL.GetlabelByNotesId(NotesId);
                if (notesid != null)
                {
                    return this.Ok(new { Status = true, Message = "Label Found successfully", data = notesid });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Label  not Found " });
                }
            }
            catch(Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }
        }
        [HttpDelete("Delete")]
        public IActionResult DeleteLabel(long labelId)
        {
            try

            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.labelBL.DeleteLabel(labelId))
                {
                    return this.Ok(new { Success = true, message = "label Deleted successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "No Such label Found" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }
        }
        [HttpPut("Update")]
        public IActionResult UpdateLabel(LabelModel labelModel, long labelId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.labelBL.UpdateLabel(labelModel, labelId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " updated succes", Updated = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "No Such Registration Found" });
                }

            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }
        }
        [HttpGet("Redis")]
        public async Task<IActionResult> GetAllLabelUsingRedisCache()
        {
            var cacheKey = "LabelList";
            string serializedLabelList;
            var labelList = new List<Label>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                labelList = JsonConvert.DeserializeObject<List<Label>>(serializedLabelList);
            }
            else
            {
                labelList = await fundooContext.LabelTable.ToListAsync();
                serializedLabelList = JsonConvert.SerializeObject(labelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(labelList);
        }


    }
}
