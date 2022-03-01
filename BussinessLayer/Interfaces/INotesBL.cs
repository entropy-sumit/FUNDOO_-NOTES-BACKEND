using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface INotesBL
    {
        public bool GenerateNote(UserNotes notes, long UserId);
        public UserNotes UpdateNotes(UserNotes notes, long UserId, long NotesId);
        public IEnumerable<Notes> GetAllNotes();
    }
}
