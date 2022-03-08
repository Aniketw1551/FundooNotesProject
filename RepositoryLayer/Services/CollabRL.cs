using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CollabRL : ICollabRL
    {

        private readonly FundooContext fundooContext;

        //Constructor
        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        //Method to create collab with registered person
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
        //Method to view collab by notes id
        public IEnumerable<Collaborator> ViewCollabByNotesId(long NotesId)
        {
            try
            {
                //Getting all details from collab for given collab id
                var data = fundooContext.CollabTable.Where(y => y.NotesId == NotesId).ToList();
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
        //To View all collaborators in database
        public List<Collaborator> ViewAllCollaborators()
        {
            try
            {
                //Getting all details of collaborators present in database
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
            //Method to Remove collab 
            public Collaborator RemoveCollab(long userId, long CollabId)
        {
            var result = fundooContext.CollabTable.FirstOrDefault(x => x.Id == userId && x.CollabId == CollabId);
            if (result != null)
            {
                //Removing collab from the database
                fundooContext.CollabTable.Remove(result);
                fundooContext.SaveChanges();
                return result;
            }
            else
                return null;
        }
    }
}