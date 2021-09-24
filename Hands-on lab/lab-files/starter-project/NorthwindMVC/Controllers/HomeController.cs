using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using NorthwindMVC.Data;
using NorthwindMVC.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace NorthwindMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Oracle
            var salesByYear = await _context.SalesByYearDbSet.FromSqlRaw("BEGIN NW.SALESBYYEAR(:P_BEGIN_DATE, :P_END_DATE, :CUR_OUT); END;",
                                new OracleParameter { ParameterName = "P_BEGIN_DATE", OracleDbType = OracleDbType.TimeStamp, Direction = ParameterDirection.Input, Value = new OracleTimeStamp(1996, 1, 1) },
                                new OracleParameter { ParameterName = "P_END_DATE", OracleDbType = OracleDbType.TimeStamp, Direction = ParameterDirection.Input, Value = new OracleTimeStamp(1999, 12, 31) },
                                new OracleParameter { ParameterName = "CUR_OUT", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output }).ToListAsync();

            var model = from r in salesByYear
                        orderby r.Year
                        group r by r.Year into grp
                        select new SalesByYearViewModel { Year = grp.Key, Count = grp.Count() };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
