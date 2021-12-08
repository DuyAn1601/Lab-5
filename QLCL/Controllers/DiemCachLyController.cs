using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QLCL.Models;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace QLCL.Controllers
{
    public class DiemCachLyController : Controller
    {
        public IActionResult ThemDiemCachLy()
        {
            return View();
        }


        [HttpPost]
        public string InsertDCL(DiemCachLyModels diemCachLy)
        {
            int count;
            DataContext context = HttpContext.RequestServices.GetService(typeof(QLCL.Models.DataContext)) as DataContext;
            count = context.sqlInsertDiemCachLy(diemCachLy);
            if (count == 1)
            {
                return "Thêm Thành Công!";
            }
            return "Thêm thất bại";
        }

       
        
        public IActionResult LietKeDiemCachLy(string MaDiemCL)
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(QLCL.Models.DataContext)) as DataContext;
            ViewData["MaDiemCL"] = new SelectList(context.slqSelectDiaDiem(), "MaDiemCL", "TenDiemCL");
            return View();
        }
    }
}
