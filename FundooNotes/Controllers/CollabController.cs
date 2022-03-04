using BussinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        public CollabController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;


        }
        [HttpPost("Add")]
        public IActionResult CollaborationMethod(CollabModel collab)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.collabBL.CollaborationMethod(collab,userId))
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
        public IActionResult  GetCollabsByNoteId(long NotesId)
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
                return this.BadRequest(new { Status = false, message =  ex.InnerException.Message });
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

    }
}
