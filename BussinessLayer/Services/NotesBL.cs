using BussinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class NotesBL : INotesBL
    {
        INotesRL NoteRL;
        public NotesBL(INotesRL notesRL)
        {
            this.NoteRL = notesRL;
        }
        public bool GenerateNote(UserNotes notes, long UserId)
        {
            try
            {
                return this.NoteRL.GenerateNote(notes, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
