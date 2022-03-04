using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
        public bool CreateLabel(LabelModel labelModel);
    }
}
