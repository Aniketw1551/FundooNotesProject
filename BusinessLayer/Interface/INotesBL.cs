using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        public Notes NoteCreation(long userId, NotesCreation notesCreation);
        public Notes NoteUpdate(long NotesId, long userId, NotesUpdate notesUpdate);
        public bool DeleteNote(long NotesId);
        public IEnumerable<Notes> ViewNotesByUserId(long userId);
        public List<Notes> ViewAllNotes();
        public Notes NoteArchive(long userId, long NotesId);
        public Notes NotePin(long userId, long NotesId);
        public Notes NoteTrash(long userId, long NotesId);
        public Notes NoteColor(long NotesId, long userId, string Color);
        public Notes ImageUpload(long userId, long NotesId, IFormFile image);
    }
}
