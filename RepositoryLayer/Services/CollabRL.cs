﻿using CommonLayer.Models;
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
    public class CollabRL : ICollabRL
    {
        private readonly FundooContext fundoocontext;
        IConfiguration _configure;
        public CollabRL(FundooContext fundoocontext, IConfiguration configure)
        {
            this.fundoocontext = fundoocontext;
            _configure = configure;
        }
        public bool CollaborationMethod(CollabModel collab, long UserId)
        {
            try
            {
                var noteid = this.fundoocontext.Notes.Where(x => x.NotesId == collab.NotesId && x.UserId == UserId).FirstOrDefault();
                var collabemail = this.fundoocontext.UserTables.Where(x => x.Email == collab.CollabEmail).FirstOrDefault();
                Collaborator collaborator = new Collaborator();
                if(noteid!=null && collabemail.Email!=null)
                {
                    collaborator.CollabEmail = collab.CollabEmail;
                    collaborator.NotesId = collab.NotesId;
                    collaborator.UserId = UserId;
                    this.fundoocontext.CollabTable.Add(collaborator);
                }
                int result = this.fundoocontext.SaveChanges();
                if(result>0)
                {
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
        public IEnumerable<Collaborator> GetCollabsByNoteId(long NotesId)
        {
            try
            {
                var response = this.fundoocontext.CollabTable.Where(x => x.NotesId == NotesId).ToList();
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool DeleteCollab(long NotesId)
        {
            try
            {
                var check = this.fundoocontext.CollabTable.Where(x => x.NotesId == NotesId).FirstOrDefault();
                this.fundoocontext.CollabTable.Remove(check);
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
