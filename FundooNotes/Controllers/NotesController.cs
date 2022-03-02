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
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
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
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }

        }

        [HttpGet("AllNotes")]
        public IActionResult GetAllNotes()
        {
            try
            {
                var noteResult = this.notesBL.GetAllNotes();
                if (noteResult == null)
                {
                    return this.BadRequest(new { Success = false, message = " Notes records not found" });
                }
                else
                {
                    return this.Ok(new { Success = true, message = "Notes records found", notesdata = noteResult });
                }
                
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }

    }
}
