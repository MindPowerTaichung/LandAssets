// 取代
// [Controller名稱] : (如:Roles)
// [DatabaseModelContainer名稱] : 資料庫ModelContainer類別名稱(如:LandAssetsEntities)
// LandTypeViewModel : 資料物件ViewModel類別名稱(如:RoleViewModel)
// [資料集名稱] : (如:Roles)
// [資料類別名稱] : (如:Role)
// [資料識別欄位] : (如:Id)
// [資料識別欄位型別] : (如:int)
// [資料ViewModel類別欄位1] :  (如:Name)

using LandAssets.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LandAssets.Controllers
{
    public class LandTypesController : ApiController
    {
        LandAssetsEntities db = new LandAssetsEntities();
        
        // GET: api/LandTypes
        public IEnumerable<LandTypeViewModel> Get()
        {
            var items = db.LandTypes.ToArray<LandType>().Select(item => ToLandTypeViewModel(item));
            return items;
        }

        // GET: api/LandTypes/5
        public IHttpActionResult Get(int id)
        {
            LandType item = db.LandTypes.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(ToLandTypeViewModel(item));
        }

        // POST: api/LandTypes
        [HttpPost]
        public IHttpActionResult Post(LandTypeViewModel item_viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            LandType item = new LandType { Id = item_viewModel.Id, Type=item_viewModel.Type};
												 //不需指定Timestamp欄位

            db.LandTypes.Add(item);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var entityError = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                var getFullMessage = string.Join("; ", entityError);
                var exceptionMessage = string.Concat(ex.Message, "errors are: ", getFullMessage);
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, exceptionMessage));
            }

            return CreatedAtRoute("DefaultApi", new { id = item.Id }, ToLandTypeViewModel(item));
        }

        // PUT: api/LandTypes/5
        [HttpPut]
        public IHttpActionResult Put(int id, LandTypeViewModel item_viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != item_viewModel.Id)
                return BadRequest();

            //把資料庫中的那筆資料讀出來
            var item_db = db.LandTypes.Find(id);
            if (item_db == null)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "這筆資料已被刪除!"));
            }
            else
            {
                try
                {
				    //不需指定Id
                    item_db.Type = item_viewModel.Type;
					//Timestamp欄位以如下方式設定
                    db.Entry(item_db).OriginalValues["Timestamp"] = Convert.FromBase64String(item_viewModel.TimestampString);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.LandTypes.Find(id) == null)
                        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound, "這筆資料已被刪除!"));
                    else
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict, "這筆資料已被其他人修改!"));
                }
            }

            return Ok(ToLandTypeViewModel(item_db));

        }

        // DELETE: api/LandTypes/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            LandType item_db = db.LandTypes.Find(id);
            if (item_db == null)
            {
                return NotFound();
            }

            db.LandTypes.Remove(item_db);
            db.SaveChanges();

            return Ok(new LandTypeViewModel { Id = id });
        }

        private LandTypeViewModel ToLandTypeViewModel(LandType item)
        {
            return new LandTypeViewModel { Id = item.Id, Type = item.Type, TimestampString = Convert.ToBase64String(item.Timestamp) };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}