using BussinessLayer.Interfaces;
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
    public class NotesController : ControllerBase
    {
        
        
       
        private readonly INotesBL notesBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;
        public NotesController(INotesBL notesBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.notesBL = notesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
            
            
        }
        
        [HttpPost("Create")]
        public IActionResult GenerateNote(UserNotes notes)
        {
           
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                //var data = this.notesBL.GenerateNote(notes, userId);
                if (this.notesBL.GenerateNote(notes,userId))
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
                return this.BadRequest(new { Status = false, Message = ex.InnerException.Message });
            }
        }
        [HttpPut("Update")]

        public IActionResult UpdatesNotes(UserNotes notes,long NotesId)
        {
            
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);

                UserNotes response = notesBL.UpdateNotes(notes, NotesId);
                if (response != null)
                {
                    return this.Ok(new { Success = true, message = " updated succes", Updated = response });
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

        [HttpGet("AllDetail")]
        public IActionResult GetAllNotes()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var noteResult = this.notesBL.GetAllNotes(userId);
                if (noteResult == null)
                {
                    return this.BadRequest(new { Success = false, message = " Notes records not found of the user" });
                }
                else
                {
                    return this.Ok(new { Success = true, message = "Notes records found of the user", notesdata = noteResult });
                }
                
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }
        }
        [HttpDelete("Delete")]
        public IActionResult DeleteNotesOfUser(long NotesId)
        {
            try

            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.notesBL.DeleteNotesOfUser(NotesId))
                {
                    return this.Ok(new { Success = true, message = "Deleted successfully" });
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
        [HttpPut("IsArchieve")]
        public IActionResult Archieve(long NotesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.Archieve(NotesId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = result });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }
        }
        [HttpPut("IsPinned")]
        public IActionResult Pinned(long NotesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.Pinned(NotesId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = result });
                }
                
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }
        }
        [HttpPut("IsTrash")]
        public IActionResult TrashedNotes(long NotesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.TrashedNotes(NotesId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = result });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }
        }
        [HttpPut("Color")]
        public IActionResult AddColor(long NotesId, string color)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.AddColor(NotesId, color);
                if (result!=color)
                {
                    return this.Ok(new { Status = true, message = result  });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = result });
                }

            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }
        }
        [HttpPut("BGImage")]
        public IActionResult BGImage(long NotesId, IFormFile image)
        {
            {
                try
                {
                    long userId = Convert.ToInt32(User.Claims.FirstOrDefault(X => X.Type == "Id").Value);
                    var result = this.notesBL.BGImage(NotesId,image);
                    if (result == true)
                    {
                        return this.Ok(new { isSuccess = true, message = "BGImage Added Successfully!", data = result });
                    }
                    else
                        return this.BadRequest(new { isSuccess = false, message = " BGImage not Added!" });
                }
                catch (Exception ex)
                {
                    return this.BadRequest(new { Status = false, isSuccess = false, message = ex.InnerException.Message });
                }
            }


        }
        [HttpGet("AllNotes")]
        public IActionResult GetAll()
        {
            try
            {
                
                var noteResult = this.notesBL.GetAll();
                if (noteResult == null)
                {
                    return this.BadRequest(new { Success = false, message = " Notes records not found in DB" });
                }
                else
                {
                    return this.Ok(new { Success = true, message = "Notes records found of the user", notesdata = noteResult });
                }

            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }
        }
       

    }
}
