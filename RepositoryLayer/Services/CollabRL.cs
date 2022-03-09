using System;
using System.Collections.Generic;
using System.Linq;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CollabRL : ICollabRL
    {
        // Instance variable
        private readonly FundooContext fundooContext;

        // Constructor
        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        /// <summary>
        /// Method to create collab with registered person
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notesId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public Collaborator CreateCollab(long userId, long notesId, string email)
        {
            try
            {
                var result = fundooContext.UserTable.FirstOrDefault(x => x.Email == email);
                if (result.Email == email)
                {
                    Collaborator collab = new Collaborator();
                    collab.CollabEmail = email;
                    collab.Id = userId;
                    collab.NotesId = notesId;
                    fundooContext.CollabTable.Add(collab);
                    fundooContext.SaveChanges();
                    return collab;
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
        /// Method to view collab by notes id
        /// </summary>
        /// <param name="notesId"></param>
        /// <returns></returns>
        public IEnumerable<Collaborator> ViewCollabByNotesId(long notesId)
        {
            try
            {
                // Getting all details from collab for given collab id
                var data = fundooContext.CollabTable.Where(y => y.NotesId == notesId).ToList();
                if (data != null)
                {
                    return data;
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
        /// Method to View all collaborators in database
        /// </summary>
        /// <returns></returns>
        public List<Collaborator> ViewAllCollaborators()
        {
            try
            {
                // Getting all details of collaborators present in database
                var collab = fundooContext.CollabTable.ToList();
                if (collab != null)
                {
                    return collab;
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
        /// Method to Remove collab 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="collabId"></param>
        /// <returns></returns>
        public Collaborator RemoveCollab(long userId, long collabId)
        {
            var result = fundooContext.CollabTable.FirstOrDefault(x => x.Id == userId && x.CollabId == collabId);
            if (result != null)
            {
                // Removing collab from the database
                fundooContext.CollabTable.Remove(result);
                fundooContext.SaveChanges();
                return result;
            }
            else
                return null;
        }
    }
}