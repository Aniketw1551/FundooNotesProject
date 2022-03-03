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
    public class NotesRL : INotesRL
    {
        private readonly FundooContext fundooContext;

        //Constructor
        public NotesRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        //Method to create Note in database
        public Notes NoteCreation(long userId, NotesCreation notesCreation)
        {
            try
            {
                Notes notes = new Notes();
                notes.Title = notesCreation.Title;
                notes.Description = notesCreation.Description;
                notes.Color = notesCreation.Color;
                notes.Image = notesCreation.Image;
                notes.IsArchieve = notesCreation.IsArchieve;
                notes.IsTrash = notesCreation.IsTrash;
                notes.IsPin = notesCreation.IsPin;
                notes.CreatedAt = notesCreation.CreatedAt;
                notes.ModifiedAt = notesCreation.ModifiedAt;
                notes.Id = userId;
                fundooContext.NotesTable.Add(notes);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                    return notes;
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Method to update note in database 
        public Notes NoteUpdate(long NotesId, long userId, NotesUpdate notesUpdate)
        {
            try
            {
                var user = fundooContext.NotesTable.Where(x => x.NotesId == NotesId).FirstOrDefault();
                if (user != null)
                {
                    user.Title = notesUpdate.Title;
                    user.Description = notesUpdate.Description;
                    user.Color = notesUpdate.Color;
                    user.Image = notesUpdate.Image;
                    user.ModifiedAt = notesUpdate.ModifiedAt;
                    //Updating databse for given user id
                    fundooContext.NotesTable.Update(user);
                    fundooContext.SaveChanges();
                    return user;
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
        //Method to delete note 
        public bool DeleteNote(long NotesId)
        {
            try
            {
                var user = fundooContext.NotesTable.Where(x => x.NotesId == NotesId).FirstOrDefault();
                if (user != null)
                {
                    //Removing note from the database
                    fundooContext.NotesTable.Remove(user);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Method to get details of note of given user id
        public IEnumerable<Notes> ViewNotesByUserId(long userId)
        {
            try
            {
                //Getting all details from notes for given user id
                var note = fundooContext.NotesTable.Where(y => y.Id == userId).ToList();
                if (note != null)
                {
                    return note;
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Method to get all notes present in database
        public IEnumerable<Notes> ViewAllNotes()
        {
            try
            {
                //Getting all details present in database
                var note = fundooContext.NotesTable.ToList();
                if (note != null)
                {
                    return note;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
