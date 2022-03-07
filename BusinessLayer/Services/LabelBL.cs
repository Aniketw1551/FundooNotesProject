using BusinessLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        //instance varable
        private readonly ILabelRL labelRL;

        //Constructor of LabelBL
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }
    }
}
