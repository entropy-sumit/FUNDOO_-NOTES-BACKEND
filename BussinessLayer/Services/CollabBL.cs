﻿using BussinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class CollabBL:ICollabBL
    {
        private readonly ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }
        public bool CollaborationMethod(CollabModel collab, long UserId)
        {
            try
            {
                return collabRL.CollaborationMethod(collab, UserId);
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
                return collabRL.GetCollabsByNoteId(NotesId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteCollab(long NotesId)
        {
            try
            {
                return collabRL.DeleteCollab(NotesId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
