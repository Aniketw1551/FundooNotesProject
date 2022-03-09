using System.Collections.Generic;
using RepositoryLayer.Entity;
using System;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollabRL
    {
        public Collaborator CreateCollab(long userId, long notesId, string email);

        public IEnumerable<Collaborator> ViewCollabByNotesId(long notesId);

        public Collaborator RemoveCollab(long userId, long collabId);

        public List<Collaborator> ViewAllCollaborators();
    }
}