﻿using BussinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class LabelBL:ILabelBL
    {
        private readonly ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public bool CreateLabel(LabelModel labelModel)
        {
            try
            {
                return labelRL.CreateLabel(labelModel);
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
                return labelRL.GetlabelByNotesId(NotesId);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public bool DeleteLabel(long labelId)
        {
            try
            {
                return labelRL.DeleteLabel(labelId);
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
                return labelRL.UpdateLabel(labelModel, labelId);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
