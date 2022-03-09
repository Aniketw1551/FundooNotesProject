//-----------------------------------------------------------------------
// <copyright file="LabelBL.cs" company="Aniket">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Services
{
using System;
using System.Text;
using System.Collections.Generic;
using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

    /// <summary>
    /// Label Class
    /// </summary>
    public class LabelBL : ILabelBL
    {
        ////Instance varable

        /// <summary>The label RL</summary>
        private readonly ILabelRL labelRL;

        ////Constructor of LabelBL

        /// <summary>Initializes a new instance of the <see cref="LabelBL" /> class.</summary>
        /// <param name="labelRL">The label RL.</param>
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        /// <summary>Creates the label.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <returns>
        ///  Create Label
        /// </returns>
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

        /// <summary>Updates the label.</summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///  Update Label
        /// </returns>
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

        /// <summary>Views the labels by user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///  Get Labels by User Id
        /// </returns>
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

        /// <summary>Views the labels by notes identifier.</summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>
        /// Get Labels By Notes Id
        /// </returns>
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

        /// <summary>Views all labels.</summary>
        /// <returns>
        /// Get All Labels
        /// </returns>
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

        /// <summary>Remove label the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>
        ///  Delete Label
        /// </returns>
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
