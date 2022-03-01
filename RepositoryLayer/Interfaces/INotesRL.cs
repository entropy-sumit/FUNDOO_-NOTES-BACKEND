using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
   public interface INotesRL
    {

        public bool GenerateNote(UserNotes notes, long UserId);
    }
}
