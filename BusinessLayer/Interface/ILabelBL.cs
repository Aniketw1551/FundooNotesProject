﻿using System.Collections.Generic;
using RepositoryLayer.Entity;

namespace BusinessLayer.Interface
{
   public interface ILabelBL
    {
        public Labels CreateLabel(long userId, long notesId, string labelName);

        public Labels UpdateLabel(string labelName, long notesId, long userId);

        public IEnumerable<Labels> ViewLabelsByUserId(long userId);

        public IEnumerable<Labels> ViewLabelsByNotesId(long notesId);

        public bool Removelabel(long userId, long labelId);

        public List<Labels> ViewAllLabels();
    }
}