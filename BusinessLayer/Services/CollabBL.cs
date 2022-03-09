using System;
using System.Collections.Generic;
using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace BusinessLayer.Services
{
    public class CollabBL : ICollabBL
    {
        // Instance varable
        private readonly ICollabRL collabRL;

        // Constructor of CollabBL
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }

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
