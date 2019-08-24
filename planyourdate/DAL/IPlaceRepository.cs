using System;
using System.Collections.Generic;
using planyourdate.Model;

namespace planyourdate.DAL
{
    public interface IPlaceRepository : IDisposable
    {
        IEnumerable<Place> GetPlaces();
        Place GetPlaceByID(int PlaceId);
        void InsertPlace(Place place);
        void DeletePlace(int PlaceId);
        void UpdatePlace(Place place);
        void Save();
    }
}
