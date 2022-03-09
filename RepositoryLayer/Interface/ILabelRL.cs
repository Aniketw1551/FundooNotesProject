//-----------------------------------------------------------------------
// <copyright file="ILabelRL.cs" company="Aniket">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Interface
{
using System;
using System.Collections.Generic;
using CommonLayer.Model;
using RepositoryLayer.Entity;

    /// <summary>
    ///  Interface class
    /// </summary>
    public interface ILabelRL
    {
        /// <summary>Creates the label.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Labels CreateLabel(long userId, long notesId, string labelName);

        /// <summary>Updates the label.</summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Labels UpdateLabel(string labelName, long notesId, long userId);

        /// <summary>Views the labels by user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public IEnumerable<Labels> ViewLabelsByUserId(long userId);

        /// <summary>Views the labels by notes identifier.</summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public IEnumerable<Labels> ViewLabelsByNotesId(long notesId);

        /// <summary>Remove label the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public bool Removelabel(long userId, long labelId);

        /// <summary>Views all labels.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<Labels> ViewAllLabels();
    }
}
