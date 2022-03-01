using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface INotesBL
    {
        public bool GenerateNote(UserNotes notes, long UserId);
    }
}
