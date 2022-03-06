using BussinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
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


    }
}
