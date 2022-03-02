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
        public bool GenerateNote(UserNotes notes, long userId)
        {
            try
            {
                return notesRL.GenerateNote(notes,userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public UserNotes UpdateNotes(UserNotes notes,long NotesId)
        {
            try
            {
                return notesRL.UpdateNotes(notes, NotesId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<Notes> GetAllNotes(long UserId)
        {
            try
            {
                return notesRL.GetAllNotes(UserId);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public bool DeleteNotesOfUser(long NotesId)
        {
            try
            {
                return this.notesRL.DeleteNotesOfUser(NotesId);
            }
            catch(Exception)
            {
                throw;
            }
        }



    }
}
