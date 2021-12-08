using Microsoft.AspNetCore.Mvc;
using QLCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace QLCL.Controllers
{
    public class CongNhanController : Controller
    {
        public IActionResult LietKeTrieuChung()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ListByTC(int stc)
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(QLCL.Models.DataContext)) as DataContext;
            return View(context.Lietketrieuchung(stc));
            
        }
    }
}
