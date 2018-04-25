using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetAllUpcomingGigs();
        IEnumerable<Gig> GetUpcomingGigsByArtist(string userId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigById(int id);
        Gig GegGigWithArtistAndGenre(int id);
        void Add(Gig gig);
    }
}