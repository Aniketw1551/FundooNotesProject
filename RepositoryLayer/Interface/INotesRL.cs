//-----------------------------------------------------------------------
// <copyright file="INotesRL.cs" company="Aniket">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Interface
{
using System;
using System.Collections.Generic;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;

    /// <summary>
    ///  Interface class
    /// </summary>
    public interface INotesRL
    {
        /// <summary>Notes the creation.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesCreation">The notes creation.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Notes NoteCreation(long userId, NotesCreation notesCreation);

        /// <summary>Notes the update.</summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="notesUpdate">The notes update.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Notes NoteUpdate(long notesId, NotesUpdate notesUpdate);

        /// <summary>Deletes the note.</summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public bool DeleteNote(long notesId);

        /// <summary>Views the notes by user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public IEnumerable<Notes> ViewNotesByUserId(long userId);

        /// <summary>Views all notes.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<Notes> ViewAllNotes();

        /// <summary>Notes the archive.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Notes NoteArchive(long userId, long notesId);

        /// <summary>Notes the pin.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Notes NotePin(long userId, long notesId);

        /// <summary>Notes the trash.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Notes NoteTrash(long userId, long notesId);

        /// <summary>Notes the color.</summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Notes NoteColor(long notesId, long userId, string color);

        /// <summary>Images the upload.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Notes ImageUpload(long userId, long notesId, IFormFile image);
    }
}
