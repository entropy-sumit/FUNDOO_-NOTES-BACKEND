using CommonLayer.Models;
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
    public class NotesRL :INotesRL
    {
        private readonly FundooContext fundoocontext;
        IConfiguration _configure;
        public NotesRL(FundooContext fundoocontext, IConfiguration configure)
        {
            this.fundoocontext = fundoocontext;
            _configure = configure;
        }
        public bool GenerateNote(UserNotes notes, long UserId)
        {
            try
            {
                Notes newNotes = new Notes();
                newNotes.NotesId = notes.NotesId;
                newNotes.Title = notes.Title;
                newNotes.Body = notes.Body;
                newNotes.Reminder = notes.Reminder;
                newNotes.Color = notes.Color;
                newNotes.BgImage = notes.BgImage;
                newNotes.Archieve = notes.Archieve;
                newNotes.IsPinned = notes.IsPinned;
                newNotes.NotesId = UserId;
                newNotes.CreatedTime = notes.CreatedTime;

                //Adding the data to database
                this.fundoocontext.Notes.Add(newNotes);

                //Save the changes in database
                int result = this.fundoocontext.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
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
                var UpdateNote = this.fundoocontext.Notes.Where(Y => Y.NotesId == NotesId).FirstOrDefault();
                if (UpdateNote != null && UpdateNote.NotesId == UserId)
                {
                    UpdateNote.Title = notes.Title;
                    
                    UpdateNote.Reminder = notes.Reminder;
                    UpdateNote.Color = notes.Color;
                    UpdateNote.BgImage = notes.BgImage;
                    UpdateNote.ModifiedTime= notes.ModifiedTime;
                }
                var result = this.fundoocontext.SaveChanges();
                if (result > 0)
                {
                    return notes;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<Notes> GetAllNotes()
        {
            return fundoocontext.Notes.ToList();
        }

        public string UpdatesNotes(UserNotes updateusernotes)
        {
            throw new NotImplementedException();
        }
    }
}
