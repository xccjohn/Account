using System;
using System.Linq;
using System.Web.Mvc;
using log4net;
using Model;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private static readonly BusinessBase<StudentEntity> Student = new BusinessBase<StudentEntity>();

        private readonly ILog _log = LogManager.GetLogger(typeof(HomeController));
        //private static readonly Student Student = new Student();
        public ActionResult Index()
        {
            _log.Debug("sdfos");

            ViewBag.Message = "欢迎使用 ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Query(QueryRequest queryRequest)
        {
            try
            {
                //var queryRequest = new QueryRequest { Page = 1, Rows = 1, Order = "desc", OrderBy = "CreateDate", Where = null };
                var paging = queryRequest.ConvertRequestTopaging<StudentEntity>();
                var response = Student.Query(paging);
                return Json(response);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
            }
            return null;
        }

        [HttpPost]
        public JsonResult AddOrUpdate(StudentEntity student)
        {
            var rev = true;
            var mess = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(student.Id))
                {
                    student.Id = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    Student.Add(student);
                }
                else
                {
                    var item = Student.Query(x => x.Id == student.Id).FirstOrDefault();
                    if (item != null)
                    {
                        Student.DelEntity(x => x.Id == student.Id);
                        Student.Add(student);
                    }
                    else
                    {
                        rev = false;
                        mess = "您更新都记录不存在!";
                    }
                }
            }
            catch (Exception ex)
            {
                rev = false;
                mess = ex.Message;
            }

            return Json(new { result = rev, message = mess });
        }

        [HttpPost]
        public JsonResult Del(string id)
        {
            var res = true;
            var mes = string.Empty;

            try
            {
                Student.DelEntity(x => x.Id == id);
            }
            catch (Exception e)
            {
                res = false;
                mes = e.Message;
            }

            return Json(new { result = res, message = mes });
        }
    }
}
