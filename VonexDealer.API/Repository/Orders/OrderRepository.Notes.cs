using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Repository.Orders
{
    public partial class OrderRepository
    {
        public async Task<List<Note>> AddNotesToOrderAsync(long orderID, List<Note> Notes)
        {
            if (orderID == 0 || Notes.Count == 0)
                return null;

            foreach (Note note in Notes)
            {
                note.OrderID = orderID;
                if (note.NoteID > 0)
                {
                    _context.Notes.Update(note);
                }
                else
                {
                    if (note.NoteText != null)
                        _context.Notes.Add(note);
                }

            }
            try
            {
                await _context.SaveChangesAsync();
                return Notes;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return null;
            }
        }

        public async Task<bool> RemoveNotesFromOrderAsync(long orderID, long NotesID)
        {
            if (orderID == 0)
                return false;

            var note = await _context.Notes.FindAsync(NotesID);
            if (note == null)
                return false;

            _context.Entry(note).State = EntityState.Deleted;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return false;
            }

        }

        public async Task<Note> UpdateNotesOnOrderAsync(long orderID, Note note)
        {
            if (orderID == 0)
                return note;

            var existing = await _context.Notes.FindAsync(note.NoteID);
            if (note == null)
                return null;

            _context.Update(note);


            try
            {
                await _context.SaveChangesAsync();
                return note;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return null;

            }
        }

        public async Task<List<Note>> GetNotesOnOrderAsync(Int64 orderID, string Component = "")
        {
            if (orderID == 0)
                return null;

            List<Note> notes = await _context.Notes.Where(x => x.Component == Component || Component == "")
                .Where(x => x.OrderID == orderID).ToListAsync();
            return notes;

        }
    }
}
