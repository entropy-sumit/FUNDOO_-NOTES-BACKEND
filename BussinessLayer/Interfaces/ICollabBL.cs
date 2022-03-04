using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface ICollabBL
    {
        public bool CollaborationMethod(CollabModel collab, long UserId);
        public IEnumerable<Collaborator> GetCollabsByNoteId(long NotesId);
        public bool DeleteCollab(long NotesId);
    }
}
