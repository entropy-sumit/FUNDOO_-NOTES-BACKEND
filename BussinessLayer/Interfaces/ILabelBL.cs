using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface ILabelBL
    {
        public bool CreateLabel(LabelModel labelModel);
    }
}
