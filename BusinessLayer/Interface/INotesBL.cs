﻿using CommonLayer.Model;
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
        public IEnumerable<Notes> ViewAllNotes();
    }
}