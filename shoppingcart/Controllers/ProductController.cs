using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using shoppingcart.Models;
using System.IO;

namespace shoppingcart.Controllers
{
    public class ProductController : Controller
    {

        private string[] validImageTypes = new string[] { "image/gif", "image/png", "image/jpeg"  };

        private ApplicationDbContext db = new ApplicationDbContext();

        private const int PAGE_SIZE = 2;



        // GET: Product
        //public ActionResult Index()
        //{
        //    return View(db.Products.ToList());
        //}




        // GET: Product
        public ActionResult Index(int page  = 1)
        {
            //  this gets all the products in one big list
            //var products = db.Products.ToList();

            //  we want to page thru it
            var products = db.Products
                             .OrderBy(p => p.Name)
                             .Skip(PAGE_SIZE * (page - 1))
                             .Take(PAGE_SIZE);

            var model = new ProductsViewModel
            {
                Products = products,
                Pager = new PagingModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PAGE_SIZE,
                    TotalItems = db.Products.Count()
                }
            };

            return View(model);
            //return View(db.Products.ToList());
        }






        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }






        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,Description,Price,QtyOnHand,ImageUrl")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Products.Add(product);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(product);
        //}



        private string ParseFilenameFromPath(string pathWithFile)
        {
            string filename = pathWithFile;

            if (pathWithFile.Contains(":\\") )
            {
                int index = pathWithFile.LastIndexOf("\\");
                filename = pathWithFile.Substring(index + 1, pathWithFile.Length-index-1);
            }

            return filename;
        }


        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel model)
        {
            if (model.ImageUpload != null)
            {
                if(     !validImageTypes.Contains( model.ImageUpload.ContentType  )   )
                {
                    ModelState.AddModelError("ImageUpload", "Please select a GIF, JPEG or PNG");
                }
            }

            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    QtyOnHand = model.QtyOnHand
                };

                if ( model.ImageUpload != null  && model.ImageUpload.ContentLength > 0)
                {
                    var uploadDir = "~/Images";
                    var uploadUrl = "/Images";

                    var BADPATH1 = Path.Combine(Server.MapPath(uploadDir), model.ImageUpload.FileName);
                    var BADPATH2 = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadDir), model.ImageUpload.FileName);

                    var temp1 = Server.MapPath(uploadDir);
                    var temp2 = System.Web.Hosting.HostingEnvironment.MapPath(uploadDir);

                    var filename = ParseFilenameFromPath(model.ImageUpload.FileName);
                    var imagePath = Path.Combine(temp2, filename);
                    var imageUrl = Path.Combine( uploadUrl, filename);

                    model.ImageUpload.SaveAs(imagePath);

                    product.ImageUrl = imageUrl;

                }

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(model);
        }







        // GET: Product/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}



        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }


            var model = new ProductEditViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                AltText = product.Name,
                Caption = product.Name,
                QtyOnHand = product.QtyOnHand,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };
            

            return View(model);
            //return View(product);
        }







        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,QtyOnHand,ImageUrl")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(product).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(product);
        //}




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductEditViewModel model)
        {

            if (model.ImageUpload != null)
            {
                if (!validImageTypes.Contains(model.ImageUpload.ContentType))
                {
                    ModelState.AddModelError("ImageUpload", "Please select a GIF, JPEG or PNG");
                }
            }

            if (ModelState.IsValid)
            {
                var product = db.Products.Find(id);

                if (product == null)
                {
                    return new HttpNotFoundResult();
                }

                //  map/transform/copy the FORM(MODEL) data to the database entity
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.QtyOnHand = model.QtyOnHand;
                product.ImageUrl = model.ImageUrl;
                
                if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                {
                    var uploadDir = "~/Images";
                    var uploadUrl = "/Images";

                    var BADPATH1 = Path.Combine(Server.MapPath(uploadDir), model.ImageUpload.FileName);
                    var BADPATH2 = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadDir), model.ImageUpload.FileName);

                    var temp1 = Server.MapPath(uploadDir);
                    var temp2 = System.Web.Hosting.HostingEnvironment.MapPath(uploadDir);

                    var filename = ParseFilenameFromPath(model.ImageUpload.FileName);
                    var imagePath = Path.Combine(temp2, filename);
                    var imageUrl = Path.Combine(uploadUrl, filename);

                    model.ImageUpload.SaveAs(imagePath);

                    product.ImageUrl = imageUrl;
                }

                //////////////////////db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }





        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
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
