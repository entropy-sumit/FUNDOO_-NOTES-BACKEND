﻿using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface ICollabBL
    {
        public bool CollaborationMethod(CollabModel collab, long UserId);
    }
}
