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
    [Authorize]
    public class NotesController : ControllerBase
    {
        
        
       
        private readonly INotesBL notesBL;
        public NotesController(INotesBL notesBL)
        {
            this.notesBL = notesBL;
            
            
        }
        
        [HttpPost("CreateNotes")]
        public IActionResult GenerateNote(UserNotes notes)
        {
           
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
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
        [HttpPut("UpdateNotes")]

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

        [HttpGet("AllNotes")]
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
        [HttpDelete("DeleteNotes")]
        public IActionResult DeleteNotesOfUser(long NotesId)
        {
            try

            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.notesBL.DeleteNotesOfUser(NotesId))
                {
                    return this.Ok(new { Success = true, message = "Deleted successfully.." });
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
        [HttpPut("Archieve")]
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
                return this.BadRequest(new { Status = false, message = result });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }
        }

    }
}
