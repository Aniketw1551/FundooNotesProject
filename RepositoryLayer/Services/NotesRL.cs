//-----------------------------------------------------------------------
// <copyright file="NotesRL.cs" company="Aniket">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Services
{
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

    /// <summary>
    /// Notes Class
    /// </summary>
    public class NotesRL : INotesRL
    {
        ////Cloudinary constants

        /// <summary>The cloud name</summary>
        private const string CloudName = "ani1551";

        /// <summary>
        /// The API key
        /// </summary>
        private const string ApiKey = "334343811552193";

        /// <summary>The API secret</summary>
        private const string ApiSecret = "0MrHz-x-np6XYuk1e-KiOxK0YSE";

        ////Instance variable 

        /// <summary>The FUNDOO CONTEXT</summary>
        private readonly FundooContext fundooContext;
       
        ////Constructor
        
        /// <summary>Initializes a new instance of the <see cref="NotesRL" /> class.</summary>
        /// <param name="fundooContext">The FUNDOO CONTEXT.</param>
        public NotesRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        /// <summary>
        /// Method to create Note in database
        /// </summary>
        /// <param name="userId">The UserId</param>
        /// <param name="notesCreation">The Note</param>
        /// <returns>THe Note</returns>
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
                this.fundooContext.NotesTable.Add(notes);
                int result = this.fundooContext.SaveChanges();
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
        /// <param name="notesId">The notes Id</param>
        /// <param name="notesUpdate">The Notes Update</param>
        /// <returns>Note Update</returns>
        public Notes NoteUpdate(long notesId, NotesUpdate notesUpdate)
        {
            try
            {
                var note = this.fundooContext.NotesTable.Where(x => x.NotesId == notesId).FirstOrDefault();
                if (note != null)
                {
                    note.Title = notesUpdate.Title;
                    note.Description = notesUpdate.Description;
                    note.Color = notesUpdate.Color;
                    note.Image = notesUpdate.Image;
                    note.ModifiedAt = notesUpdate.ModifiedAt;
                    ////Updating databse for given user id
                    this.fundooContext.NotesTable.Update(note);
                    this.fundooContext.SaveChanges();
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
        /// <param name="notesId">The Notes Id</param>
        /// <returns>Delete Note</returns>
        public bool DeleteNote(long notesId)
        {
            try
            {
                var user = this.fundooContext.NotesTable.Where(x => x.NotesId == notesId).FirstOrDefault();
                if (user != null)
                {
                    //Removing note from the database
                    this.fundooContext.NotesTable.Remove(user);
                    this.fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to get details of note of given user id
        /// </summary>
        /// <param name="userId">The User Id</param>
        /// <returns>View Notes</returns>
        public IEnumerable<Notes> ViewNotesByUserId(long userId)
        {
            try
            {
                ////Getting all details from notes for given user id
                var note = this.fundooContext.NotesTable.Where(y => y.Id == userId).ToList();
                if (note != null)
                {
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
        /// Method to get all notes present in database
        /// </summary>
        /// <returns>List of notes</returns>
        public List<Notes> ViewAllNotes()
        {
            try
            {
                ////Getting all details present in database
                var note = this.fundooContext.NotesTable.ToList();
                if (note != null)
                {
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
        /// Method to check note is archived or not
        /// </summary>
        /// <param name="userId">The User Id</param>
        /// <param name="notesId">The notes Id</param>
        /// <returns>Note Archive</returns>
        public Notes NoteArchive(long userId, long notesId)
        {
            try
            {
                Notes note = this.fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == notesId);
                if (note != null)
                {
                    bool checkArchive = note.IsArchieve;
                    if (checkArchive == true)
                    {
                        note.IsArchieve = false;
                    }

                    if (checkArchive == false)
                    {
                        note.IsArchieve = true;
                    }

                    this.fundooContext.SaveChanges();
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
        /// Method to check note is pinned or not
        /// </summary>
        /// <param name="userId">The User Id</param>
        /// <param name="notesId">The notes Id</param>
        /// <returns>Note Pin</returns>
        public Notes NotePin(long userId, long notesId)
        {
            try
            {
                Notes note = this.fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == notesId);
                if (note != null)
                {
                    bool checkPin = note.IsPin;
                    if (checkPin == true)
                    {
                        note.IsPin = false;
                    }

                    if (checkPin == false)
                    {
                        note.IsPin = true;
                    }

                    this.fundooContext.SaveChanges();
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
        /// Method to check note is trashed or not
        /// </summary>
        /// <param name="userId">The User Id</param>
        /// <param name="notesId">The Notes Id</param>
        /// <returns>Note Trash</returns>
        public Notes NoteTrash(long userId, long notesId)
        {
            try
            {
                Notes note = this.fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == notesId);
                if (note != null)
                {
                    bool checkTrash = note.IsTrash;
                    if (checkTrash == true)
                    {
                        note.IsTrash = false;
                    }

                    if (checkTrash == false)
                    {
                        note.IsTrash = true;
                    }

                    this.fundooContext.SaveChanges();
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
        /// Method to change color of note
        /// </summary>
        /// <param name="notesId">The Notes Id</param>
        /// <param name="userId">The User Id</param>
        /// <param name="color">The Color</param>
        /// <returns>Notes Color</returns>
        public Notes NoteColor(long notesId, long userId, string color)
        {
            try
            {
                Notes note = this.fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == notesId);
                if (note != null)
                {
                    note.Color = color;
                    this.fundooContext.SaveChanges();
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
        /// <param name="userId">The User Id</param>
        /// <param name="notesId">The Notes Id</param>
        /// <param name="image">The Image</param>
        /// <returns>Image Upload</returns>
        public Notes ImageUpload(long userId, long notesId, IFormFile image)
        {
            try
            {
                var note = this.fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == notesId);
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
                    this.fundooContext.NotesTable.Update(note);
                    int imageupload = this.fundooContext.SaveChanges();
                    if (imageupload > 0)
                        return note;
                    else
                    {
                        return null;
                    }
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
    }
}
