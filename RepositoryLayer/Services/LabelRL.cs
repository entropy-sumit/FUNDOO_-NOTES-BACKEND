using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
   public class LabelRL:ILabelRL
    {
        private readonly FundooContext fundoocontext;
        
        public LabelRL(FundooContext fundoocontext)
        {
            this.fundoocontext = fundoocontext;
            
        }
        public bool CreateLabel(LabelModel labelModel)
        {
            try
            {
                var note = this.fundoocontext.Notes.Where(x => x.NotesId == labelModel.NotesId).FirstOrDefault();
                if (note != null)
                {
                    Label label = new Label();
                    label.LabelName = labelModel.LabelName;
                    label.NotesId = note.NotesId;
                    label.UserId = note.UserId;

                    this.fundoocontext.LabelTable.Add(label);
                    int result = this.fundoocontext.SaveChanges();
                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
