using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CollabBL : ICollabBL
    {
        //instance varable
        private readonly ICollabRL collabRL;

        //Constructor of UserBL
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }
        public Collaborator CreateCollab(long userId, long notesId, string email)
        {
            try
            {
                return collabRL.CreateCollab(userId, notesId, email);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<Collaborator> ViewCollabByNotesId(long NotesId)
        {
            try
            {
                return collabRL.ViewCollabByNotesId(NotesId);
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
                return collabRL.ViewAllCollaborators();
            }
            catch (Exception)
            {

                throw;
            }
        }
            public Collaborator RemoveCollab(long userId, long CollabId)
        {
            try
            {
                return collabRL.RemoveCollab(userId, CollabId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
