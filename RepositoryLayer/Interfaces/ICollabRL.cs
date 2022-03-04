using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
   public interface ICollabRL
    {
        public bool CollaborationMethod(CollabModel collab, long UserId);
        public IEnumerable<Collaborator> GetCollabsByNoteId(long NotesId);
    }
}
