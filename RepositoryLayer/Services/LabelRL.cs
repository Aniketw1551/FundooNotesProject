using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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
        //To create label
        public Labels CreateLabel(long userId, long notesId, string labelName)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(x => x.NotesId == notesId);
                if (result.NotesId == notesId)
                {
                    Labels label = new Labels();
                    label.LabelName = labelName;
                    label.Id = userId;
                    label.NotesId = notesId;
                    fundooContext.LabelTable.Add(label);
                    fundooContext.SaveChanges();
                    return label;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //To update label
            public Labels UpdateLabel(string labelName, long notesId, long userId)
            {
                try
                {
                    var label = fundooContext.LabelTable.FirstOrDefault(x => x.NotesId == notesId && x.Id == userId);
                    if (label!=null)
                    {
                        label.LabelName = labelName;
                        fundooContext.LabelTable.Update(label);
                        fundooContext.SaveChanges();
                        return label;
                    }
                    else

                        return null;
                }

                catch (Exception)
                {
                    throw;
                }
            }
        // TO get labels by user id
        public IEnumerable<Labels> ViewLabelsByUserId(long userId)
        {
            try
            {
                //Getting all details from label for given userid
                var data = fundooContext.LabelTable.Where(y => y.Id == userId).ToList();
                if (data != null)
                {
                    return data;
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //To get labels by notes id
        public IEnumerable<Labels> ViewLabelsByNotesId(long notesId)
        {
            try
            {
                //Getting all details from label for given userid
                var data = fundooContext.LabelTable.Where(y => y.NotesId == notesId).ToList();
                if (data != null)
                {
                    return data;
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Get all labels in database 
        public List<Labels> ViewAllLabels()
        {
            try
            {
                //Getting all details of labels present in database
                var label = fundooContext.LabelTable.ToList();
                if (label != null)
                {
                    return label;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //To remove label
            public bool Removelabel(long userId, long labelId)
        {
            var result = fundooContext.LabelTable.FirstOrDefault(x => x.Id == userId && x.LabelId == labelId);
            if (result != null)
            {
                //Removing label from the database
                fundooContext.LabelTable.Remove(result);
                fundooContext.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
