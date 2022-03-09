using System;
using System.Collections.Generic;
using System.Linq;
using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
        // Instance variable
        private readonly FundooContext fundooContext;

        // Constructor
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        /// <summary>
        /// Method to create new LAbel
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notesId"></param>
        /// <param name="labelName"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Method to update label
        /// </summary>
        /// <param name="labelName"></param>
        /// <param name="notesId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Labels UpdateLabel(string labelName, long notesId, long userId)
        {
            try
            {
                var label = fundooContext.LabelTable.FirstOrDefault(x => x.NotesId == notesId && x.Id == userId);
                if (label != null)
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
            
        /// <summary>
        /// Method to get labels by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Method to get labels by notes id
        /// </summary>
        /// <param name="notesId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Method to get all labels
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Method too remove label
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="labelId"></param>
        /// <returns></returns>
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
