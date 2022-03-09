//-----------------------------------------------------------------------
// <copyright file="LabelRL.cs" company="Aniket">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Services
{
using System;
using System.Collections.Generic;
using System.Linq;
using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;


    /// <summary>
    /// Label Class
    /// </summary>
    public class LabelRL : ILabelRL
    {
        ////Instance variable

        /// <summary>The FUNDOO CONTEXT</summary>
        private readonly FundooContext fundooContext;

        ////Constructor

        /// <summary>Initializes a new instance of the <see cref="LabelRL" /> class.</summary>
        /// <param name="fundooContext">The FUNDOO CONTEXT.</param>
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        /// <summary>
        /// Method to create new LAbel
        /// </summary>
        /// <param name="userId">This User Id</param>
        /// <param name="notesId">This Notes Id</param>
        /// <param name="labelName">This Label Name</param>
        /// <returns>Create Label</returns>
        public Labels CreateLabel(long userId, long notesId, string labelName)
        {
            try
            {
                var result = this.fundooContext.NotesTable.FirstOrDefault(x => x.NotesId == notesId);
                if (result.NotesId == notesId)
                {
                    Labels label = new Labels();
                    label.LabelName = labelName;
                    label.Id = userId;
                    label.NotesId = notesId;
                    this.fundooContext.LabelTable.Add(label);
                    this.fundooContext.SaveChanges();
                    return label;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to update label
        /// </summary>
        /// <param name="labelName">The Label Name</param>
        /// <param name="notesId">The Notes Id</param>
        /// <param name="userId">The User Id</param>
        /// <returns>Update Label</returns>
        public Labels UpdateLabel(string labelName, long notesId, long userId)
        {
            try
            {
                var label = this.fundooContext.LabelTable.FirstOrDefault(x => x.NotesId == notesId && x.Id == userId);
                if (label != null)
                {
                    label.LabelName = labelName;
                    this.fundooContext.LabelTable.Update(label);
                    this.fundooContext.SaveChanges();
                    return label;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
            
        /// <summary>
        /// Method to get labels by user id
        /// </summary>
        /// <param name="userId">The User Id</param>
        /// <returns>Label by user id</returns>
        public IEnumerable<Labels> ViewLabelsByUserId(long userId)
        {
            try
            {
                ////Getting all details from label for given userid
                var data = this.fundooContext.LabelTable.Where(y => y.Id == userId).ToList();
                if (data != null)
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            { 
                throw;
            }
        }

        /// <summary>
        /// Method to get labels by notes id
        /// </summary>
        /// <param name="notesId">The Notes Id</param>
        /// <returns>Labels by Notes Id</returns>
        public IEnumerable<Labels> ViewLabelsByNotesId(long notesId)
        {
            try
            {
                ////Getting all details from label for given userid
                var data = this.fundooContext.LabelTable.Where(y => y.NotesId == notesId).ToList();
                if (data != null)
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to get all labels
        /// </summary>
        /// <returns>All Labels</returns>
        public List<Labels> ViewAllLabels()
        {
            try
            {
                ////Getting all details of labels present in database
                var label = this.fundooContext.LabelTable.ToList();
                if (label != null)
                {
                    return label;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method too remove label
        /// </summary>
        /// <param name="userId">The User Id</param>
        /// <param name="labelId">The Label Id</param>
        /// <returns>Remove Label</returns>
        public bool Removelabel(long userId, long labelId)
        {
            var result = this.fundooContext.LabelTable.FirstOrDefault(x => x.Id == userId && x.LabelId == labelId);
            if (result != null)
            {
                ////Removing label from the database
                this.fundooContext.LabelTable.Remove(result);
                this.fundooContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
