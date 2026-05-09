using Microsoft.AspNetCore.Mvc;
using AspNetCoreSecureApi.Models;
using Microsoft.EntityFrameworkCore;
using AspNetCoreSecureApi.Services;
using System.Xml.Serialization;
using System.Configuration;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AspNetCoreSecureApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : Controller
{
    private readonly AppDbContext _dbContext;

    public TestController(ILogger<TestController> logger, AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: HomeController
    [HttpGet]
    public String Index()
    {
        return "this is test api";
    }

    [HttpPost]
    [Route("new")]
    public string CreateTest(TestDto testDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState).ToString();
        }
        //dbConnectionService dbconservice = new dbConnectionService();
        //string result = dbconservice.RunStoredProc(testDto.Text);

        var test = new Test

        {
            Id = testDto.Id,
            Text = testDto.Text
        };

        var x = _dbContext.Tests.Add(test);

        _dbContext.SaveChanges();

        //var x = _dbContext.Tests.FromSql($"EXECUTE dbo.Insert_test {}");

        return x.ToString();
        //return result;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<TestDto>> GetTest(int id)
    {

        var test = await _dbContext.Tests.FindAsync(id);

        if (test is null)
        {
            return NotFound();
        }

        var testDto = new TestDto
        {
            Id = test.Id,
            Text = test.Text
        };

        return testDto;
    }

    [HttpPut]
    [Route(("{id:int}"))]
    public async Task<ActionResult<Test>> UpdateTest(TestDto testDto)
    {

        var test = await _dbContext.Tests.FindAsync(testDto.Id);

        if (test is null)
        {
            return NotFound();
        }

        if(test.Text == testDto.Text)
        {
            return BadRequest();
        }

        test.Text = testDto.Text;
        _dbContext.Tests.Update(test);
        _dbContext.SaveChanges();

        test = await _dbContext.Tests.FindAsync(testDto.Id);
        return test;
    }

    [HttpDelete]
    [Route(("{id:int}"))]
    public async Task<ActionResult<Test>> DeleteTest(int id)
    {

        var test = await _dbContext.Tests.FindAsync(id);

        if (test is null)
        {
            return NotFound();
        }

        _dbContext.Tests.Remove(test);
        _dbContext.SaveChanges();

        test = await _dbContext.Tests.FindAsync(id);
        if (test is null)
        {
            return StatusCode(200, "deleted successfuly!");
        }
        return BadRequest();
    }

    [HttpGet]
    [Route("AllTestEFC")]
    public async Task<ActionResult<TestDto>> GetAllTestsEFC()
    {
        var tests = _dbContext.Tests.Select(c => new TestDto()
        {
            Id = c.Id,
            Text = c.Text
        });

        return View(tests);
    }

    [HttpGet]
    [Route("AllTestDapper")]
    public List<Test> GetAllTestsDapper()
    {
        var tests =  DataDapperService.UseDapper();

        return tests;
    }

    [HttpPost]
    [Route("multinew")]
    public List<Test> CreateMultipleTest(List<TestDto> testsDto)
    {
        if (!ModelState.IsValid)
        {
            return [];
        }
        List<Test> tests = new List<Test>();
        foreach (TestDto test in testsDto)
        {
            tests.Add(new Test
            {
                Id = test.Id,
                Text = test.Text,
            });
        }
        return DataDapperService.MultipleInsert(tests);
    }

    [HttpGet]
    [Route("AllTestDap")]
    public IActionResult GetAllTestsDap()
    {
        var tests = DataDapperService.UseDapper();
        var testsDto = new List<TestDto>();
        foreach(Test test in tests)
        {
            testsDto.Add(new TestDto
            {
                Id = test.Id,
                Text = test.Text
            });
        }
        return Ok(testsDto);
    }

    [HttpGet]
    [Route("AllBills")]
    public List<Bill> GetAllBills()
    {
        var bills = DataDapperService.ViewBills();

        return bills;
    }


    [HttpGet]
    [Route("AllBillsEFC")]
    public async Task<List<Bill>> GetAllBillsEFC()
    {
        var bills = await _dbContext.Bills.ToListAsync();

        return bills;
    }

}
