using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Microsoft.AspNetCore.Cors;
using alison_etl_api.Models;

namespace alison_etl_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PipelineController : ControllerBase
    {
        // GET: api/Pipeline
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<ExpandoObject> Get()
        {
            Database db = new Database();
            db.Open();
            string query = "select * from `alison-etl`.Pipeline";
            List<ExpandoObject> pipelines = db.Select(query);
            db.Close();

            return pipelines;
        }

        // GET: api/Pipeline/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetPipeline")]
        public List<ExpandoObject> Get(int id)
        {
            Database db = new Database();
            db.Open();
            string query = "select id, name, description from `alison-etl`.Pipeline where id = @id";
            var values = new Dictionary<string, object>()
                {
                    {"@id", id},
                };
            List<ExpandoObject> pipelines = db.Select(query, values);
            db.Close();

            return pipelines;
        }

        // POST: api/Pipeline
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Pipeline/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Pipeline pipeline)
        {
            try
            {
                Database db = new Database();
                db.Open();

                //Update the pipeline table for future processing
                string updateQuery = "UPDATE `alison-etl`.Pipeline ";
                updateQuery += "SET Status = @Status, ScheduledMinutes = @ScheduledMinutes ";
                updateQuery += "WHERE Id = @Id;";

                var values = new Dictionary<string, object>()
                {
                    {"@id", id},
                    {"@Status", pipeline.Status},
                    {"@ScheduledMinutes", pipeline.ScheduledMinutes}
                };

                db.Update(updateQuery, values);

                //Update the Audit table to know when the change was made
                string insertQuery = "INSERT INTO `alison-etl`.PipelineConfigAudit ";
                insertQuery += "(PipelineId, ScheduledMinutes, Status)";
                insertQuery += "VALUES(@PipelineId, @ScheduledMinutes, @Status)";

                var insertValues = new Dictionary<string, object>()
                {
                    {"@PipelineId", id},
                    {"@Status", pipeline.Status},
                    {"@ScheduledMinutes", pipeline.ScheduledMinutes}
                };

                db.Insert(insertQuery, insertValues);
                //return CreatedAtAction();

                db.Close();
            }
            catch
            {
                Console.WriteLine("Something went wrong in the put");
                //return BadRequest();

            }

        }

        // DELETE: api/Pipeline/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
