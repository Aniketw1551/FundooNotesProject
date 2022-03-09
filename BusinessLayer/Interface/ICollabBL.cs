//-----------------------------------------------------------------------
// <copyright file="ICollabBL.cs" company="Aniket">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Interface
{
using System.Collections.Generic;
using RepositoryLayer.Entity;

    /// <summary>
    ///  Interface Class
    /// </summary>
    public interface ICollabBL
    {
        /// <summary>Creates the COLLAB.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="email">The email.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Collaborator CreateCollab(long userId, long notesId, string email);

        /// <summary>Views the COLLAB by notes identifier.</summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public IEnumerable<Collaborator> ViewCollabByNotesId(long notesId);

        /// <summary>Removes the COLLAB.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collabId">The COLLAB identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Collaborator RemoveCollab(long userId, long collabId);

        /// <summary>Views all collaborators.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<Collaborator> ViewAllCollaborators();
    }
}