using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
        public bool CreateLabel(LabelModel labelModel);
        public IEnumerable<Label> GetlabelByNotesId(long NotesId);
        public bool DeleteLabel(long labelId);
    }
}
