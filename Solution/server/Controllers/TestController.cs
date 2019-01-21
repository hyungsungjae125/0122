using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using server.Modules;

namespace server.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        [Route("select")]
        [HttpGet]
        public ActionResult<ArrayList> Get()
        {
            return Query.GetSelect();
        }

        [Route("insert")]
        [HttpPost]
        public ActionResult<bool> Insert([FromForm] string nTitle,[FromForm] string nContents,[FromForm] string mName)
        {
            return Query.GetInsert(nTitle,nContents,mName);
        }
        [Route("update")]
        [HttpPost]
        public ActionResult<bool> Update([FromForm] string nNo,[FromForm] string nTitle,[FromForm] string nContents,[FromForm] string mName)
        {
            return Query.GetUpdate(nNo,nTitle,nContents,mName);
        }
        [Route("delete")]
        [HttpPost]
        public ActionResult<bool> Delete([FromForm] string nNo,[FromForm] string mName)
        {
            return Query.GetDelete(nNo,mName);
        }
    }
}
