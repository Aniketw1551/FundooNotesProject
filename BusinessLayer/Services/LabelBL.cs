using BusinessLayer.Interface;
using RepositoryLayer.Entity;
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
        public Labels CreateLabel(long userId, long notesId, string labelName)
        {
            try
            {
                return labelRL.CreateLabel(userId, notesId, labelName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Labels UpdateLabel(string labelName, long notesId, long userId)
        {
            try
            {
                return labelRL.UpdateLabel(labelName, notesId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<Labels> ViewLabelsByUserId(long userId)
        {
            try
            {
                return labelRL.ViewLabelsByUserId(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<Labels> ViewLabelsByNotesId(long notesId)
        {
            try
            {
                return labelRL.ViewLabelsByNotesId(notesId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Labels> ViewAllLabels()
        {
            try
            {
                return labelRL.ViewAllLabels();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Removelabel(long userId, long labelId)
        {
            try
            {
                return labelRL.Removelabel(userId, labelId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
