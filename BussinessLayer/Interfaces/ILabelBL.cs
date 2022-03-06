using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface ILabelBL
    {
        public bool CreateLabel(LabelModel labelModel);
        public IEnumerable<Label> GetlabelByNotesId(long NotesId);
        public bool DeleteLabel(long labelId);
        public LabelModel UpdateLabel(LabelModel labelModel, long labelId);
    }
}
