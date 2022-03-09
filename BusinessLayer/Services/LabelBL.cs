using System;
using System.Text;
using System.Collections.Generic;
using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        // Instance varable
        private readonly ILabelRL labelRL;

        // Constructor of LabelBL
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

            public Labels CreateLabel(long userId, long notesId, string labelName)
            {
                try
                {
                    return this.labelRL.CreateLabel(userId, notesId, labelName);
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
                return this.labelRL.UpdateLabel(labelName, notesId, userId);
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
                return this.labelRL.ViewLabelsByUserId(userId);
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
                return this.labelRL.ViewLabelsByNotesId(notesId);
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
                return this.labelRL.ViewAllLabels();
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
                return this.labelRL.Removelabel(userId, labelId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
