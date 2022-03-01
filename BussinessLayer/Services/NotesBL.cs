using BussinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL notesRL;
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }
        public bool GenerateNote(UserNotes notes, long UserId)
        {
            try
            {
                return notesRL.GenerateNote(notes, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public UserNotes UpdateNotes(UserNotes notes, long UserId, long NotesId)
        {
            try
            {
                return notesRL.UpdateNotes(notes, UserId, NotesId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Notes> GetAllNotes()
        {
            throw new NotImplementedException();
        }

       
    }
}
