using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Linkediner.Interfaces;
using Linkediner.Models;

namespace Linkediner.Controllers
{
    public class LinkedinerController : ApiController
    {
        private readonly IHtmlFetcher _fetcher;
        private readonly ILinkedinParser _parser;
        private readonly IDataAccessor _accessor;

        public LinkedinerController(IHtmlFetcher fetcher, ILinkedinParser parser, IDataAccessor accessor)
        {
            _fetcher = fetcher;
            _parser = parser;
            _accessor = accessor;

            _accessor.Init();
        }

        // GET api/Linkediner/barakar
        public IHttpActionResult Get(string id)
        {
            LinkedinProfile profile = _accessor.GetProfile(id);

            if (profile == null)
            {
                return NotFound();
            }

            return Json(profile);
        }

        // GET api/Linkediner?skills=C&skills=C++
        public IHttpActionResult Get([FromUri] List<string> skills)
        {
            List<LinkedinProfile> profiles = _accessor.GetProfilesBySkills(skills);

            return Json(profiles);
        }


        // POST api/Linkediner
        public IHttpActionResult Post([FromBody]string value)
        {
            if (!IsValidUri(value))
            {
                return BadRequest("invalid public linkedin URI");
            }

            var id = value.Split('/').Last();
            LinkedinProfile profile = null;
            
            var task = Task.Run(async () =>
            {
                var sourceCode = await _fetcher.Fetch(value).ConfigureAwait(false);

                profile = _parser.ParseProfile(id, sourceCode);

                _accessor.InsertProfile(profile);
            });

            Task.WaitAny(task);
            return Created(id, profile);
        }

        private bool IsValidUri(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            var uri = new Uri(value);

            if (!uri.Host.EndsWith("linkedin.com"))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(uri.Query))
            {
                return false;
            }

            return true;
        }
    }
}
