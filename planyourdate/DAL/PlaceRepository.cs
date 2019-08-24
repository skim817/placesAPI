using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using planyourdate.Model;

namespace planyourdate.DAL
{
    public class PlaceRepository : IPlaceRepository, IDisposable
    {
        private planyourDATEContext context;

        public PlaceRepository(planyourDATEContext context)
        {
            this.context = context;
        }

        public IEnumerable<Place> GetPlace()
        {
            return context.Place.ToList();
        }

        public Place GetPlaceByID(int PlaceId)
        {
            return context.Place.Find(PlaceId);
        }

        public void InsertPlace(Place place)
        {
            context.Place.Add(place);
        }

        public void DeletePlace(int PlaceId)
        {
            Place place = context.Place.Find(PlaceId);
            context.Place.Remove(place);
        }

        public void UpdatePlace(Place place)
        {
            context.Entry(place).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Place> GetPlaces()
        {
            throw new NotImplementedException();
        }
    }
}
