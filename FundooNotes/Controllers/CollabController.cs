﻿using BussinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;
        public CollabController(ICollabBL collabBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.collabBL = collabBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;


        }
        [HttpPost("Add")]
        public IActionResult CollaborationMethod(CollabModel collab)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.collabBL.CollaborationMethod(collab, userId))
                {
                    return this.Ok(new { Status = true, Message = "Note Shared successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "You Do not have permission" });

                }

            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
        [HttpGet("Detail")]
        public IActionResult GetCollabsByNoteId(long NotesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var notesid = this.collabBL.GetCollabsByNoteId(NotesId);
                if (notesid != null)
                {
                    return this.Ok(new { Status = true, Message = "Collaboration Found", data = notesid });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Collaboration not Found " });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }
        }
        [HttpDelete("Delete")]
        public IActionResult DeleteCollab(long NotesId)
        {
            try

            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.collabBL.DeleteCollab(NotesId))
                {
                    return this.Ok(new { Success = true, message = "Collab Deleted successfully" });
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
            var cacheKey = "CollabList";
            string serializedCollabList;
            var collabList = new List<Collaborator>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                collabList = JsonConvert.DeserializeObject<List<Collaborator>>(serializedCollabList);
            }
            else
            {
                collabList = await fundooContext.CollabTable.ToListAsync();
                serializedCollabList = JsonConvert.SerializeObject(collabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(collabList);
        }
    } 

    
}
