using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using MongoData.Models;

namespace MongoData.Controllers
{
    public class UsersController : Controller
    {
        public UsersController()
        {
            MongoHelper.ConnectToMongoService();
            MongoHelper.users_collection = MongoHelper.database.GetCollection<Users>("Users");
        }
        // GET: Users
        public ActionResult Index()
        {
            var model = MongoHelper.users_collection.Find(FilterDefinition<Users>.Empty).ToList();

            return View(model);
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            var filter = Builders<Users>.Filter.Eq("Id", id);
            var result = MongoHelper.users_collection.Find(filter).FirstOrDefault();

            return View(result);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(Users usr)
        {
            try
            {
                var filter = Builders<Users>.Filter.Eq("UserName", usr.UserName);
                if (filter != null)
                {
                    ViewBag.Message = "Account exist!";
                }
                else
                {
                    // TODO: Add insert logic here
                    usr.Id = MongoHelper.GenerateRandomId(24);
                    usr.TimeCreated = DateTime.Now;

                    MongoHelper.users_collection.InsertOne(usr);
                    ViewBag.Message = "Added successfully!";  
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            var filter = Builders<Users>.Filter.Eq("Id", id);
            var result = MongoHelper.users_collection.Find(filter).FirstOrDefault();

            return View(result);
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Users usr)
        {
            try
            {
                // TODO: Add update logic here
                var filter = Builders<Users>.Filter.Eq("Id", usr.Id);

                var update = Builders<Users>.Update
                    .Set("UserName", usr.UserName)
                    .Set("Password", usr.Password)
                    .Set("Roles", usr.Roles);

                var result = MongoHelper.users_collection.UpdateOne(filter, update);

                if (result.IsAcknowledged)
                {
                    ViewBag.Message = "Employee updated successfully!";
                }
                else
                {
                    ViewBag.Message = "Error while updating Employee!";
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            var filter = Builders<Users>.Filter.Eq("Id", id);
            var result = MongoHelper.users_collection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var filter = Builders<Users>.Filter.Eq("Id", id);
                var result = MongoHelper.users_collection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
