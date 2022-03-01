using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INotesRL
    {

        public bool GenerateNote(UserNotes notes, long UserId);
        public UserNotes UpdateNotes(UserNotes notes, long UserId, long NotesId);
        IEnumerable<Notes> GetAllNotes();
    }
}
