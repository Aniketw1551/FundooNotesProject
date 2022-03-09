using System.Collections.Generic;
using System;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface INotesRL
    {
        public Notes NoteCreation(long userId, NotesCreation notesCreation);

        public Notes NoteUpdate(long notesId, NotesUpdate notesUpdate);

        public bool DeleteNote(long notesId);

        public IEnumerable<Notes> ViewNotesByUserId(long userId);

        public List<Notes> ViewAllNotes();

        public Notes NoteArchive(long userId, long notesId);

        public Notes NotePin(long userId, long notesId);

        public Notes NoteTrash(long userId, long notesId);

        public Notes NoteColor(long notesId, long userId, string color);

        public Notes ImageUpload(long userId, long notesId, IFormFile image);
    }
}
