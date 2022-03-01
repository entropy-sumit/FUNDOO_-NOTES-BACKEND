using BussinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer.Context;
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
        private readonly IMemoryCache memoryCache;
        public readonly FundooContext fundoocontext;
        private readonly IDistributedCache distributedCache;
        private readonly INotesBL notesBL;
        public NotesController(INotesBL notesBL, IMemoryCache memoryCache, FundooContext fundoocontext, IDistributedCache distributedCache)
        {
            this.notesBL = notesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundoocontext = fundoocontext;
        }
        [Authorize]
        [HttpPost]
        public IActionResult GenerateNote(UserNotes notes)
        {
            long UserId = Convert.ToInt64(User.FindFirst("UserId").Value);
            try
            {
                if (this.notesBL.GenerateNote(notes, UserId))
                {
                    return this.Ok(new { Success = true, message = "New Note created successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "New Node creation unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }

    }
}
