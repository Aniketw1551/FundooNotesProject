using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL notesRL;

        //Constructor of UserBL
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }
        public Notes NoteCreation(long userId, NotesCreation notesCreation)
        {
            try
            {
                return notesRL.NoteCreation(userId, notesCreation);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Notes NoteUpdate(long NotesId, long userId, NotesUpdate notesUpdate)
        {
            try
            {
                return notesRL.NoteUpdate(NotesId, userId, notesUpdate);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteNote(long NotesId)
        {
            try
            {
                return notesRL.DeleteNote(NotesId);
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
                return notesRL.ViewNotesByUserId(userId);
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
                return notesRL.ViewAllNotes();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Notes NoteArchive(long userId, long NotesId)
        {
            try
            {
                return notesRL.NoteArchive(userId, NotesId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Notes NotePin(long userId, long NotesId)
        {
            try
            {
                return notesRL.NotePin(userId, NotesId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Notes NoteTrash(long userId, long NotesId)
        {
            try
            {
                return notesRL.NoteTrash(userId, NotesId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Notes NoteColor(long NotesId, long userId, string Color)
        {
            try
            {
                return notesRL.NoteColor(NotesId, userId, Color);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Notes ImageUpload(long userId, long NotesId, IFormFile image)
        {
            try
            {
                return notesRL.ImageUpload(userId, NotesId, image);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}