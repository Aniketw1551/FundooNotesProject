//-----------------------------------------------------------------------
// <copyright file="CollabRL.cs" company="Aniket">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Services
{
using System;
using System.Collections.Generic;
using System.Linq;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

    /// <summary>
    /// COLLAB class
    /// </summary>
    public class CollabRL : ICollabRL
    {
        ////Instance variable

        /// <summary>The FUNDOO CONTEXT</summary>
        private readonly FundooContext fundooContext;

        ////Constructor
        
        /// <summary>Initializes a new instance of the <see cref="CollabRL" /> class.</summary>
        /// <param name="fundooContext">The FUNDOO CONTEXT.</param>
        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        /// <summary>
        /// Method to create COLLAB with registered person
        /// </summary>
        /// <param name="userId">tThe User Id</param>
        /// <param name="notesId">The Notes Id</param>
        /// <param name="email">The Email</param>
        /// <returns>Create COLLAB</returns>
        public Collaborator CreateCollab(long userId, long notesId, string email)
        {
            try
            {
                var result = this.fundooContext.UserTable.FirstOrDefault(x => x.Email == email);
                if (result.Email == email)
                {
                    Collaborator collab = new Collaborator();
                    collab.CollabEmail = email;
                    collab.Id = userId;
                    collab.NotesId = notesId;
                    this.fundooContext.CollabTable.Add(collab);
                    this.fundooContext.SaveChanges();
                    return collab;
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
        /// Method to view COLLAB by notes id
        /// </summary>
        /// <param name="notesId">The Notes Id</param>
        /// <returns>Get COLLAB by Notes Id</returns>
        public IEnumerable<Collaborator> ViewCollabByNotesId(long notesId)
        {
            try
            {
                ////Getting all details from collab for given collab id
                var data = this.fundooContext.CollabTable.Where(y => y.NotesId == notesId).ToList();
                if (data != null)
                {
                    return data;
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
        /// Method to View all collaborators in database
        /// </summary>
        /// <returns>Get all COLLABS</returns>
        public List<Collaborator> ViewAllCollaborators()
        {
            try
            {
                ////Getting all details of collaborators present in database
                var collab = this.fundooContext.CollabTable.ToList();
                if (collab != null)
                {
                    return collab;
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
        /// Method to Remove COLLAB 
        /// </summary>
        /// <param name="userId">The User Id</param>
        /// <param name="collabId">The COLLAB Id</param>
        /// <returns>Remove COLLAB</returns>
        public Collaborator RemoveCollab(long userId, long collabId)
        {
            var result = this.fundooContext.CollabTable.FirstOrDefault(x => x.Id == userId && x.CollabId == collabId);
            if (result != null)
            {
                ////Removing collab from the database
                this.fundooContext.CollabTable.Remove(result);
                this.fundooContext.SaveChanges();
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}