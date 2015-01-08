using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kamrul.sample.Models;
using Lib.Web.Mvc.JQuery.JqGrid;

namespace kamrul.sample.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        public ActionResult Index()
        {
            ViewBag.Message = "ASP.NET MVC application.";

            return View();
        }

        //GetProductList
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetProductList(JqGridRequest request, Product model)
        {
            string filterExpression = String.Empty;
            int totalRecords = 0;

            var list = _db.Products.ToList();

            #region Searching-------------

            if (request.Searching)
            {
                if (!string.IsNullOrEmpty(model.Name))
                {
                    list = list.Where(x => x.Name == model.Name.Trim()).ToList();
                }
                if (model.Price > 0)
                {
                    list = list.Where(x => x.Price == model.Price).ToList();
                }

            }

            #endregion

            totalRecords = list == null ? 0 : list.Count;

            JqGridResponse response = new JqGridResponse()
            {
                TotalPagesCount = (int)Math.Ceiling((float)totalRecords / (float)request.RecordsCount),
                PageIndex = request.PageIndex,
                TotalRecordsCount = totalRecords
            };

            list = list.Skip(request.PageIndex * request.RecordsCount).Take(request.RecordsCount * (request.PagesCount.HasValue ? request.PagesCount.Value : 1)).ToList();

            foreach (var d in list)
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(d.ProductId), new List<object>()
                {
                     d.ProductId,
                     d.Name,
                     d.Price,
                     "<a id='lnkProductDetails_" + d.ProductId + "' name='lnkAppDialog' class='lnkAppDialog button' href='/Home/Details/" + d.ProductId + "'>Details</a>"
                }));
            }

            //Return data as json
            return new JqGridJsonResult() { Data = response };
        }

        public ActionResult GetCategoryList()
        {
            var categories = _db.Categories.ToList();

            var viewProducts = categories.Select(cat => new Category() { CategoryId = cat.CategoryId, Name = cat.Name });

            //No of total records
            int totalRecords = (int)categories.Count;
            //Calculate total no of page  
            int totalPages = 1;   // (int)Math.Ceiling((float)totalRecords / (float)Rows);
            var getdata = new
            {
                total = totalPages,
                page = 1,
                records = totalRecords,
                rows = (
                    from p in viewProducts
                    select new
                    {
                        cell = new string[] { 
							p.CategoryId.ToString(),
                            p.CategoryId.ToString(),
							p.Name,
							"<a id='lnkDetailsCategory_" + p.CategoryId + "' class='lnkDetailsCategory button' href='/Category/Details/" + p.CategoryId + "'>Details</a>",
                            "<a id='lnkEditCategory_" + p.CategoryId + "' class='lnkEditCategory button' href='/Category/Edit/" + p.CategoryId + "'>Edit</a>",
							"<a id='lnkDeleteCategory_" + p.CategoryId + "' class='lnkDeleteCategory button' href='/Category/Delete/" + p.CategoryId + "'>Delete</a>" }
                    }).ToArray()
            };
            return Json(getdata);
        }

        [HttpPost]
        public ActionResult Save(Category model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //start do you code

                    //end do you code

                    return Content(Boolean.TrueString + "|Saved Successfully.");

                }

                return Content(Boolean.TrueString + "|Saved Successfully.");
            }
            catch (Exception ex)
            {
                return Content(Boolean.FalseString + "|Donot Saved Successfully.");
            }
        }

        public ActionResult Details(int id)
        {
            Product model = _db.Products.Find(id);

            return PartialView("_Details", model);
        }
    }
}
