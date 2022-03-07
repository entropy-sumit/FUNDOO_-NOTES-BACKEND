using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INotesRL
    {

        public bool GenerateNote(UserNotes notes, long userId);
        public UserNotes UpdateNotes(UserNotes notes, long NotesId);
        public IEnumerable<Notes> GetAllNotes(long UserId);
        public bool DeleteNotesOfUser(long NotesId);
        public string Archieve(long NotesId);
        public string Pinned(long NotesId);
        public string TrashedNotes(long NotesId);
        public string AddColor(long NotesId, string color);
        public bool BGImage(long NotesId, IFormFile image);
        public IEnumerable<Notes> GetAll();
    }
}
