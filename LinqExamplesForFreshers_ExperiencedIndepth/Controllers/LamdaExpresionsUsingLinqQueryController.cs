using LinqExamplesForFreshers_ExperiencedIndepth.NorthWind_Connect;
using LinqExamplesForFreshers_ExperiencedIndepth.NorthWind_DB_DBConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LinqExamplesForFreshers_ExperiencedIndepth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LamdaExpresionsUsingLinqQueryController : ControllerBase
    {
        //In EntityFramework Core To extract the data purpose we used linq queries.
        NorthwindContext _northwindContext;
        NorthwindDbContext _northwindDbContext;
        //INJECT THE DEPENCIES INTO CONSTRUCTOR LIKE THIS WAY

        public LamdaExpresionsUsingLinqQueryController(NorthwindContext northwindContext, NorthwindDbContext northwindDbContext)
        {
            _northwindContext = northwindContext;
            _northwindDbContext = northwindDbContext;
        }
        [HttpGet]
        [Route("GetEmployeesData")]
        //2nd of shortcutfor routing 
        //[HttpGet("GetEmployeesData")]
        //Example: Fetching All Records from employee table example
        public async Task<IActionResult> GetEmployeesData()
        {
            //Basic LamdaLinQ synatx is
            //A lambda expression is written using the => lamda operator
            //lamda expressions will reduce the normal linq query synatx.
            //now a days in realtime we are using this lamda expressions with linq queries(beacuse synatx is very short).
            //SqlQuery:Select * from employees
            //Normal LinqQuery:  var result = from abc in _northwind_DBContext.Employees select abc;
            //the above normal linq query we can also write below way by using Lamda expressions
            //Lamda expression Linq queryis below for fetching data from employee
            var result = _northwindDbContext.Employees.ToList(); // Returns all employees data with all columns.
            //.ToList(); is a method it will return total data of your model class.

            //sqlqueryconverted by compiler:select * from employees
            //the below written for json serialization refrence looping purpose written .net 8.0 to fix this refrence looping we are using this one.lower versions you will not get.
            var jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result, jsonSettings);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("GetEmployeesDatawith_CityWise")]
        public async Task<IActionResult> GetAllEmployeesDataCityWise()
        {

            //Normal LINQ QUERY:var result = from a in _northwind_DBContext.Employees where a.City == "London" select a;//Normallinqquey 
            //SqlQuery:     //select * from  Employees where City='London'
            //LAMDA EXPRESSION USING LINQ query:
            var result = _northwindDbContext.Employees.Where(a => a.City == "London").ToList();//=>we called lamda opertor
                                                                                               //(parameters) => expression
                                                                                               //here expression is a anoymous function.these functions we used in lamda expressions
            var jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result, jsonSettings);
            return StatusCode(StatusCodes.Status200OK, convertedData);
            //linq query         // var highEarners = context.Employees.Where(e => e.Salary > 50000).ToList(); 
            // Filters employees with salary > 50,000
            //sqlquery :select * from Employees where Salary > 50000
            //.ToList() means take/fetch the total records.
        }
        [HttpGet]
        [Route("GetEmployeesDatawith_CityWise_MultipleAnd ConditionsUage")]
        public async Task<IActionResult> GetAllEmployeesDataCityWise_MultipleAndConditionsUage()
        {

            //Normal LINQ QUERY:var result = from a in _northwind_DBContext.Employees where a.City == "London"&& a.Country == "UK" && a.Title == "Sales Manager" select a;//linqquey 
            //SqlQuery:     //select * from  Employees where City=='London'and Country == "UK" and Title == "Sales Manager" 
            //LAMDA EXPRESSION USING LINQ query:
            var result = _northwindDbContext.Employees.Where(a => a.City == "London" && a.Country == "UK" && a.Title == "Sales Manager").ToList();//=>we called lamda opertor
                                                                                                                                                  //(parameters) => expression
                                                                                                                                                  //here expression is a anoymous function.these functions we used in lamda expressions
            var jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("GetEmployeesDatawith_RequiredColumnsonlyShowing")]
        public async Task<IActionResult> GetEmployeesDatawith_RequiredColumnsonlyShowing()
        {//here we are fetchingall the data and showing only Required column only.
         //normal linq query:var result = from a in _northwind_DBContext.Employees select new { FirstName, LastName, Address, City };
         //SqlQuery Format:select FirstName+LastName as 'EmployeeFullName' from  Employees 
         //Lamda Expression With Linq query:Thebelow LinQ Query we will get the required colmns only.
            var result = _northwindDbContext.Employees.Select(e => new { e.FirstName, e.LastName, e.Address, e.City }).ToList();
            var jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("GetOrderDataWithNamestatswiths")]
        public async Task<IActionResult> GetDataByNamesStartswiths()
        {//here we are fetchingall employess  data.
         //normal linq query:var result = from s in _northwind_DBContext.Customers where s.ContactName.StartsWith("A") select s;
         //lamda expression linq query like below.
            var result = _northwindDbContext.Customers.Where(a => a.ContactName.StartsWith("A")).ToList();
            //SQLQUERY:select * from Customers where ContactName like 'A%'
            var jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result, jsonSettings);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("OrderByusage")]
        public async Task<IActionResult> OrderbyUsage()
        {
            /*
             //Normal linq queries with ascending order and descending order write like this.
               var orderByAscendingResult = from s in lststudentsObj
                                            orderby s.StudentName ascending
                                            select s;//it will Show the  total columns 
                                                     //Select new{StudentId,Age}//you can also select required columns
               var orderByDescendingResult = from s in lststudentsObj
                                             orderby s.StudentName descending
                                             select s;
            //Here  select s   take The total records.
            //In Lamda Expesions same thing can achieve by .ToList() Predfiend mehod.
            */
            //ascending order/descending order  lamda expresion linq query.
            //sqlquery :Select * from Employees order by ContactName
            //sqlquery :Select * from Employees order by ContactName desc
            var orderByAscendingResult = _northwindDbContext.Customers.OrderBy(e => e.ContactName).ToList();//ascending order
            var orderByDescendingResult = _northwindDbContext.Customers.OrderByDescending(e => e.ContactName).ToList();//descending order

            //Order by appling on multile columns combinations.
            //sqlquery :Select * from Employees order by FirstName,LastName

            var orderByonMultipleColumns = _northwindDbContext.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ToList();

            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(orderByDescendingResult);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("GroupByusageWithOrginalSingleTable")]
        public async Task<IActionResult> GroupByusage()
        {

            //Sql Groupby Query:select   CompanyName as CompanyName,Count(*) as Count     from Customers group by CompanyName
            var groupedCompanyNameData = _northwindDbContext.Customers.GroupBy(s => s.CompanyName)
                                     .Select(g => new { CompanyName = g.Key, CompanyName1 = g.ToList() });
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(groupedCompanyNameData);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }

    }
}
