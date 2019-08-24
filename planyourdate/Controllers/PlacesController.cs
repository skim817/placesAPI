using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using planyourdate.Model;
using planyourdate.Transfer;
using planyourdate.DAL;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace planyourdate.Controllers
{

    public class IDGEN
    {
        public string PlaceIDE { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private IPlaceRepository PlaceRepository;
        private readonly IMapper _mapper;


        public PlacesController(planyourDATEContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            this.PlaceRepository = new PlaceRepository(new planyourDATEContext());
        }
        private readonly planyourDATEContext _context;

        // GET: api/Places
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Place>>> GetPlace()
        {
            return await _context.Place.ToListAsync();
        }

        // GET: api/Places/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Place>> GetPlace(int id)
        {
            var place = await _context.Place.FindAsync(id);

            if (place == null)
            {
                return NotFound();
            }

            return place;
        }

        // PUT: api/Places/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlace(int id, Place place)
        {
            if (id != place.PlaceId)
            {
                return BadRequest();
            }

            _context.Entry(place).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Places
        [HttpPost]
        public async Task<ActionResult<Place>> PostPlace([FromBody]IDGEN id)
        {
            Place place;
            String PlaceIde;

            try
            {
                // Constructing the video object from our helper function
                PlaceIde = id.PlaceIDE;
                place = Map.GetPlaceFromId(PlaceIde);
            }
            catch
            {
                return BadRequest("Invalid PlaceID");
            }
            _context.Place.Add(place);

            await _context.SaveChangesAsync();

            int ide = place.PlaceId;

            planyourDATEContext a = new planyourDATEContext();
            PhotosController pcc = new PhotosController(a);

            Task addPhoto = Task.Run(async () =>
            {
                List<Photo> placearray = new List<Photo>();
                placearray = Map.GetPhotosFromID(PlaceIde);

                for (int i = 0; i < placearray.Count; i++)
                {
                    Photo pl = placearray.ElementAt(i);
                    pl.PlaceId = ide;

                    await pcc.PostPhoto(pl);
                }
            });


            return CreatedAtAction("GetPlace", new { id = place.PlaceId }, place);
        }

        // DELETE: api/Places/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Place>> DeletePlace(int id)
        {
            var place = await _context.Place.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }

            _context.Place.Remove(place);
            await _context.SaveChangesAsync();

            return place;
        }

        private bool PlaceExists(int id)
        {
            return _context.Place.Any(e => e.PlaceId == id);
        }

        [HttpGet("SerachInPhoto/{searchString}")]
        public async Task<ActionResult<IEnumerable<Place>>> Search(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return BadRequest("Search string cannot be null or empty.");
            }

            var place = await _context.Place.Include(places => places.Photo).Select(a => new Place
            {
                PlaceId = a.PlaceId,
                PlaceName = a.PlaceName,
                RankBy = a.RankBy,
                PlaceAddress = a.PlaceAddress,
                PhoneNumber = a.PhoneNumber,
                IsFavourite = a.IsFavourite,
                IsOpenNow = a.IsOpenNow,
                PlaceGeolat = a.PlaceGeolat,
                PlaceGeolng = a.PlaceGeolng,
                PhotoRef = a.PhotoRef,
                Photo = a.Photo.Where(tran => tran.PhotoName.Contains(searchString)).ToList()
            }).ToListAsync();

            place.RemoveAll(abc => abc.Photo.Count == 0);
            return Ok(place);
        }

        [HttpPatch("update/{id}")]
        public PlaceDTO Patch(int id, [FromBody]JsonPatchDocument<PlaceDTO> PlacePatch)
        {

            Place originplace = PlaceRepository.GetPlaceByID(id);

            PlaceDTO placeDTO = _mapper.Map<PlaceDTO>(originplace);

            PlacePatch.ApplyTo(placeDTO);

            _mapper.Map(placeDTO, originplace);

            _context.Update(originplace);
            _context.SaveChanges();
            return placeDTO;
        }


    }

}
