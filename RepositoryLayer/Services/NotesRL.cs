using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
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
        public bool GenerateNote(UserNotes notes,long userId)
        {
            try
            {
                Notes newNotes = new Notes();

                newNotes.UserId = userId;
                newNotes.Title = notes.Title;
                newNotes.Body = notes.Body;
                newNotes.Reminder = notes.Reminder;
                newNotes.Color = notes.Color;
                newNotes.BgImage = notes.BgImage;
                newNotes.Archieve = notes.Archieve;
                newNotes.IsPinned = notes.IsPinned;
                
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
        public UserNotes UpdateNotes(UserNotes notes, long NotesId)
        {
            try
            {
                var UpdateNote = this.fundoocontext.Notes.Where(Y => Y.NotesId == NotesId).FirstOrDefault();
                if (UpdateNote != null && UpdateNote.NotesId == NotesId)
                {
                    UpdateNote.Title = notes.Title;
                    UpdateNote.Color = notes.Color;
                    UpdateNote.Body = notes.Body;
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
        public IEnumerable<Notes> GetAllNotes(long UserId)
        {
            return fundoocontext.Notes.Where(x => x.UserId==UserId).ToList();
        }
        public bool DeleteNotesOfUser(long NotesId)
        {
            try
            {
                var notecheck = this.fundoocontext.Notes.Where(x => x.NotesId == NotesId).FirstOrDefault();
                this.fundoocontext.Notes.Remove(notecheck);
                int result = this.fundoocontext.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch(Exception)
            {
                throw;
            }
        }
        public string Archieve(long NotesId)
        {
            try
            {
                var archive = this.fundoocontext.Notes.Where(s => s.NotesId == NotesId).FirstOrDefault();
                if (archive.Archieve == true)
                {
                    archive.Archieve = false;
                    this.fundoocontext.SaveChanges();
                    return "note archieved";
                }


                else
                {
                    archive.Archieve = true;
                    this.fundoocontext.SaveChanges();
                    return "note is now unarchieved";
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
        public string Pinned(long NotesId)
        {
            try
            {
                var pinned = this.fundoocontext.Notes.Where(s => s.NotesId == NotesId).FirstOrDefault();
                if (pinned.IsPinned == true)
                {
                    pinned.IsPinned = false;
                    this.fundoocontext.SaveChanges();
                    return "note pinned";
                }


                else
                {
                    pinned.IsPinned = true;
                    this.fundoocontext.SaveChanges();
                    return "note is now unpinned";
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public string TrashedNotes(long NotesId)
        {
            try
            {
                var trashed = this.fundoocontext.Notes.Where(s => s.NotesId == NotesId).FirstOrDefault();
                if (trashed.Delete == true)
                {
                    trashed.Delete = false;
                    this.fundoocontext.SaveChanges();
                    return "notes recoverd";
                }


                else
                {
                    trashed.Delete = true;
                    this.fundoocontext.SaveChanges();
                    return "note is in trashed";
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public string AddColor(long NotesId,string color)
        {
            try
            {
                var addcolor = this.fundoocontext.Notes.Where(x => x.NotesId == NotesId).FirstOrDefault();
                if(addcolor.Color==null)
                {
                    addcolor.Color = color;
                    this.fundoocontext.SaveChanges();
                    return "color is added";
                }
                else
                {
                    return "color is already added";
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        public bool BGImage(long NotesId, IFormFile image)
        {
            try
            {
                var notes = this.fundoocontext.Notes.Where(x => x.NotesId == NotesId).FirstOrDefault();
                if (notes != null)
                {
                    Account account = new Account
                    (
                    _configure["CloudinaryAccount:cloud_name"],
                    _configure["CloudinaryAccount:api_key"],
                    _configure["CloudinaryAccount:api_secret"]
                    );
                    var path = image.OpenReadStream();
                    Cloudinary cloudinary = new Cloudinary(account);
                    ImageUploadParams uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, path)
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    fundoocontext.Notes.Attach(notes);
                    notes.BgImage = uploadResult.Url.ToString();
                    fundoocontext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
                    
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
