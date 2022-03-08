using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
        //instance variable 
        private readonly FundooContext fundooContext;
        //cloudinary 
        private const string CloudName = "ani1551";
        private const string ApiKey = "334343811552193";
        private const string ApiSecret = "0MrHz-x-np6XYuk1e-KiOxK0YSE";

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
        public Notes NoteUpdate(long NotesId, NotesUpdate notesUpdate)
        {
            try
            {
                var note = fundooContext.NotesTable.Where(x => x.NotesId == NotesId).FirstOrDefault();
                if (note != null)
                {
                    note.Title = notesUpdate.Title;
                    note.Description = notesUpdate.Description;
                    note.Color = notesUpdate.Color;
                    note.Image = notesUpdate.Image;
                    note.ModifiedAt = notesUpdate.ModifiedAt;
                    //Updating databse for given user id
                    fundooContext.NotesTable.Update(note);
                    fundooContext.SaveChanges();
                    return note;
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
        public List<Notes> ViewAllNotes()
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
        //Method to check note is archived or not
        public Notes NoteArchive(long userId, long NotesId)
        {
            try
            {
                Notes note = fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == NotesId);
                if (note != null)
                {
                    bool CheckArchive = note.IsArchieve;
                    if (CheckArchive == true)
                    {
                        note.IsArchieve = false;
                    }
                    if (CheckArchive == false)
                    {
                        note.IsArchieve = true;
                    }
                    fundooContext.SaveChanges();
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
        //Method to check note is pinned or not
        public Notes NotePin(long userId, long NotesId)
        {
            try
            {
                Notes note = fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == NotesId);
                if (note != null)
                {
                    bool CheckPin = note.IsPin;
                    if (CheckPin == true)
                    {
                        note.IsPin = false;
                    }
                    if (CheckPin == false)
                    {
                        note.IsPin = true;
                    }
                    fundooContext.SaveChanges();
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
        //Method to check note is trashed or not
        public Notes NoteTrash(long userId, long NotesId)
        {
            try
            {
                Notes note = fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == NotesId);
                if (note != null)
                {
                    bool CheckTrash = note.IsTrash;
                    if (CheckTrash == true)
                    {
                        note.IsTrash = false;
                    }
                    if (CheckTrash == false)
                    {
                        note.IsTrash = true;
                    }
                    fundooContext.SaveChanges();
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
        //Method to change color of note
        public Notes NoteColor(long NotesId, long userId, string Color)
        {
            try
            {
                Notes note = fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == NotesId);
                if (note != null)
                {
                    note.Color = Color;
                    fundooContext.SaveChanges();
                    return note;
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
        //Method to upload image
        public Notes ImageUpload(long userId, long NotesId, IFormFile image)
        {
            try
            {
                var note = fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == NotesId);
                if (note != null)
                {
                    Account account = new Account(CloudName, ApiKey, ApiSecret);
                    Cloudinary cloud = new Cloudinary(account);
                    var imagePath = image.OpenReadStream();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, imagePath),
                    };
                    var uploadResult = cloud.Upload(uploadParams);
                    note.Image = image.FileName;
                    fundooContext.NotesTable.Update(note);
                    int imageupload = fundooContext.SaveChanges();
                    if (imageupload > 0)
                        return note;
                    else
                        return null;
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
