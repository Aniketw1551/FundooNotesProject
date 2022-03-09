using System.Collections.Generic;
using RepositoryLayer.Entity;

namespace BusinessLayer.Interface
{
   public interface ICollabBL
    {
        public Collaborator CreateCollab(long userId, long notesId, string email);

        public IEnumerable<Collaborator> ViewCollabByNotesId(long notesId);

        public Collaborator RemoveCollab(long userId, long collabId);

        public List<Collaborator> ViewAllCollaborators();
    }
}