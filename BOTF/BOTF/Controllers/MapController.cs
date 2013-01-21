using BOTF.Infrastructure;
using BOTF.ModelView;
using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BOTF.Controllers
{
    public class MapController : ApiController
    {
         ContextDb _db = new ContextDb();
        // GET api/map
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/map/5
        public List<VotersMapCoordinates> Get(int id)
        {

            List<VotersMapCoordinates> output = new List<VotersMapCoordinates>();
            var histories = _db.VoteHistory.Where(c => c.ProposalId == id).ToList();    
            foreach (var history in histories)
            {
                if (history.Coordinates != null)
                {
                    DbGeography coordinate = history.Coordinates;
                    output.Add(new VotersMapCoordinates { latitude = coordinate.Latitude.Value, longitude = coordinate.Longitude.Value });
                }
              
            }

            return output;
        }

        // POST api/map
        public void Post([FromBody]string value)
        {
        }

        // PUT api/map/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/map/5
        public void Delete(int id)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
