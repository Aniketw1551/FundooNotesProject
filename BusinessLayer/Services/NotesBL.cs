//-----------------------------------------------------------------------
// <copyright file="NotesBL.cs" company="Aniket">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Services
{
using System;
using System.Collections.Generic;
using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;

    /// <summary>
    /// Notes Class
    /// </summary>
    public class NotesBL : INotesBL
    {
        /// <summary>The notes RL</summary>
        private readonly INotesRL notesRL;

        ////Constructor of UserBL

        /// <summary>Initializes a new instance of the <see cref="NotesBL" /> class.</summary>
        /// <param name="notesRL">The notes RL.</param>
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }

        /// <summary>Notes the creation.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesCreation">The notes creation.</param>
        /// <returns>
        ///  Notes Creation
        /// </returns>
        public Notes NoteCreation(long userId, NotesCreation notesCreation)
        {
            try
            {
                return this.notesRL.NoteCreation(userId, notesCreation);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Notes the update.</summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="notesUpdate">The notes update.</param>
        /// <returns>
        /// Notes Update
        /// </returns>
        public Notes NoteUpdate(long notesId, NotesUpdate notesUpdate)
        {
            try
            {
                return this.notesRL.NoteUpdate(notesId, notesUpdate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Deletes the note.</summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>
        /// Delete Note
        /// </returns>
        public bool DeleteNote(long notesId)
        {
            try
            {
                return this.notesRL.DeleteNote(notesId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Views the notes by user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Get Note by User Id</returns>
        public IEnumerable<Notes> ViewNotesByUserId(long userId)
        {
            try
            {
                return this.notesRL.ViewNotesByUserId(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Views all notes.</summary>
        /// <returns>Get All Notes</returns>
        public List<Notes> ViewAllNotes()
        {
            try
            {
                return this.notesRL.ViewAllNotes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Notes the archive.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>Note Archive</returns>
        public Notes NoteArchive(long userId, long notesId)
        {
            try
            {
                return this.notesRL.NoteArchive(userId, notesId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Notes the pin.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>
        /// Note Pin
        /// </returns>
        public Notes NotePin(long userId, long notesId)
        {
            try
            {
                return this.notesRL.NotePin(userId, notesId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Notes the trash.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>Note Trash</returns>
        public Notes NoteTrash(long userId, long notesId)
        {
            try
            {
                return this.notesRL.NoteTrash(userId, notesId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Notes the color.</summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>Note Color</returns>
        public Notes NoteColor(long notesId, long userId, string color)
        {
            try
            {
                return this.notesRL.NoteColor(notesId, userId, color);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Images the upload.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>
        /// Image Upload
        /// </returns>
        public Notes ImageUpload(long userId, long notesId, IFormFile image)
        {
            try
            {
                return this.notesRL.ImageUpload(userId, notesId, image);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}