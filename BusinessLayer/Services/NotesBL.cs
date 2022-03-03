using BusinessLayer.Interface;
using CommonLayer.Model;
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
        private readonly INotesRL NotesRL;
        private INotesRL notesRL;

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
        public IEnumerable<Notes> ViewAllNotes()
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
    }
}
