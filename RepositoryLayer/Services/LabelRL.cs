using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
        private readonly FundooContext fundooContext;

        //Constructor
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

    }
}
