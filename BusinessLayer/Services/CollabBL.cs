//-----------------------------------------------------------------------
// <copyright file="CollabBL.cs" company="Aniket">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Services
{
using System;
using System.Collections.Generic;
using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

    /// <summary>
    /// COLLAB Class
    /// </summary>
    public class CollabBL : ICollabBL
    {
        ////Instance varable

        /// <summary>The COLLAB RL</summary>
        private readonly ICollabRL collabRL;

        ////Constructor of CollabBL

        /// <summary>Initializes a new instance of the <see cref="CollabBL" /> class.</summary>
        /// <param name="collabRL">The COLLAB RL.</param>
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }

        /// <summary>Creates the COLLAB.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="email">The email.</param>
        /// <returns>
        ///  Create COLLAB
        /// </returns>
        public Collaborator CreateCollab(long userId, long notesId, string email)
        {
            try
            {
                return this.collabRL.CreateCollab(userId, notesId, email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Views the COLLAB by notes identifier.</summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>Get COLLAB by Notes Id</returns>
        public IEnumerable<Collaborator> ViewCollabByNotesId(long notesId)
        {
            try
            { 
                return this.collabRL.ViewCollabByNotesId(notesId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Views all collaborators.</summary>
        /// <returns>
        /// Get All COLLAB
        /// </returns>
        public List<Collaborator> ViewAllCollaborators()
        {
            try
            {
                return this.collabRL.ViewAllCollaborators();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Removes the COLLAB.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collabId">The COLLAB identifier.</param>
        /// <returns>
        /// Delete COLLAB
        /// </returns>
        public Collaborator RemoveCollab(long userId, long collabId)
        { 
            try
            {
                return this.collabRL.RemoveCollab(userId, collabId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
