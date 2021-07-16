using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Microsoft.AspNetCore.Cors;

namespace alison_etl_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PipelineHistoryController : ControllerBase
    {
        // GET: api/PipelineHistory
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<ExpandoObject> Get()
        {
            Console.WriteLine("In the history controler");
            Database db = new Database();
            db.Open();
            string query = "SELECT a.Id, a.PipelineId, b.Name, a.Step, a.Status, a.TimeStamp ";
            query += "FROM `alison-etl`.PipelineStatus a, `alison-etl`.Pipeline b ";
            query += "WHERE a.PipelineId = b.Id ";
            query += "ORDER BY a.TimeStamp DESC";
            List<ExpandoObject> pipelines = db.Select(query);
            db.Close();

            return pipelines;
        }

        // GET: api/PipelineHistory/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetSingleHistory")]
        public List<ExpandoObject> Get(int id)
        {
            Console.WriteLine("In the history controler");
            Database db = new Database();
            db.Open();
            string query = "SELECT a.Id, a.PipelineId, b.Name, a.Step, a.Status, a.TimeStamp ";
            query += "FROM `alison-etl`.PipelineStatus a, `alison-etl`.Pipeline b ";
            query += "WHERE a.PipelineId = @id and a.PipelineId = b.Id ";
            query += "ORDER BY a.TimeStamp DESC";
            var values = new Dictionary<string, object>()
                {
                    {"@id", id},
                };
            List<ExpandoObject> pipelines = db.Select(query, values);
            db.Close();

            return pipelines;
        }

        // POST: api/PipelineHistory
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/PipelineHistory/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/PipelineHistory/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
