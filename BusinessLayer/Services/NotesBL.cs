using System;
using System.Collections.Generic;
using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;

namespace BusinessLayer.Services
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL notesRL;

        // Constructor of UserBL
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }

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