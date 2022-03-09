using System;
using System.Collections.Generic;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;

namespace BusinessLayer.Interface
{
    public interface INotesBL
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
