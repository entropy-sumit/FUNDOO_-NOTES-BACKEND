﻿using CommonLayer.Models;
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
        public IEnumerable<Label> GetlabelByNotesId(long NotesId)
        {
            try
            {
                var response= this.fundoocontext.LabelTable.Where(x => x.NotesId == NotesId).ToList();
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool DeleteLabel(long labelId)
        {
            try
            {
                var check = this.fundoocontext.LabelTable.Where(x => x.LabelId == labelId).FirstOrDefault();
                this.fundoocontext.LabelTable.Remove(check);
                int result = this.fundoocontext.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public LabelModel UpdateLabel(LabelModel labelModel, long labelId)
        {
            try
            {
                var update = this.fundoocontext.LabelTable.Where(x => x.LabelId == labelId).FirstOrDefault();
                if (update != null)
                {
                    update.LabelName = labelModel.LabelName;
                    update.NotesId = labelModel.NotesId;

                }
                var result = this.fundoocontext.SaveChanges();
                if (result > 0)
                {
                    return labelModel;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
