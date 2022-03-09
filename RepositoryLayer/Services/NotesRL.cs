using System;
using System.Collections.Generic;
using System.Linq;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NotesRL : INotesRL
    {
        // Instance variable 
        private readonly FundooContext fundooContext;
        // Cloudinary constants
        private const string CloudName = "ani1551";
        private const string ApiKey = "334343811552193";
        private const string ApiSecret = "0MrHz-x-np6XYuk1e-KiOxK0YSE";

        // Constructor
        public NotesRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        /// <summary>
        /// Method to create Note in database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notesCreation"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Method to update note in database 
        /// </summary>
        /// <param name="notesId"></param>
        /// <param name="notesUpdate"></param>
        /// <returns></returns>
        public Notes NoteUpdate(long notesId, NotesUpdate notesUpdate)
        {
            try
            {
                var note = fundooContext.NotesTable.Where(x => x.NotesId == notesId).FirstOrDefault();
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
        /// <summary>
        /// Method to delete note
        /// </summary>
        /// <param name="notesId"></param>
        /// <returns></returns>
        public bool DeleteNote(long notesId)
        {
            try
            {
                var user = fundooContext.NotesTable.Where(x => x.NotesId == notesId).FirstOrDefault();
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
        /// <summary>
        /// Method to get details of note of given user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Method to get all notes present in database
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Method to check note is archived or not
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notesId"></param>
        /// <returns></returns>

        public Notes NoteArchive(long userId, long notesId)
        {
            try
            {
                Notes note = fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == notesId);
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
        /// <summary>
        /// Method to check note is pinned or not
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notesId"></param>
        /// <returns></returns>
        public Notes NotePin(long userId, long notesId)
        {
            try
            {
                Notes note = fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == notesId);
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
        /// <summary>
        /// Method to check note is trashed or not
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notesId"></param>
        /// <returns></returns>
        public Notes NoteTrash(long userId, long notesId)
        {
            try
            {
                Notes note = fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == notesId);
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
        /// <summary>
        /// Method to change color of note
        /// </summary>
        /// <param name="notesId"></param>
        /// <param name="userId"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public Notes NoteColor(long notesId, long userId, string color)
        {
            try
            {
                Notes note = fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == notesId);
                if (note != null)
                {
                    note.Color = color;
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
        /// <summary>
        /// Method to upload image
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notesId"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public Notes ImageUpload(long userId, long notesId, IFormFile image)
        {
            try
            {
                var note = fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == notesId);
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
