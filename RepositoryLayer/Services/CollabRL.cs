using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CollabRL
    {
        private readonly FundooContext fundoocontext;
        IConfiguration _configure;
        public CollabRL(FundooContext fundoocontext, IConfiguration configure)
        {
            this.fundoocontext = fundoocontext;
            _configure = configure;
        }
        
    }
}
