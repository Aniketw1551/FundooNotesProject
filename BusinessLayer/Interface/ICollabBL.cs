using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
   public interface ICollabBL
    {
        public Collaborator CreateCollab(long userId, long notesId, string email);
        public IEnumerable<Collaborator> ViewCollabByNotesId(long NotesId);
        public Collaborator RemoveCollab(long userId, long CollabId);
        public List<Collaborator> ViewAllCollaborators();
    }
}