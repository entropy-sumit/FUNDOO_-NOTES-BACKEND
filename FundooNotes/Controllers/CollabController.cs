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
        [HttpPut("User")]
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

    }
}
