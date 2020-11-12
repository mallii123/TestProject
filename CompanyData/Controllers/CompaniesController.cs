using Microsoft.AspNetCore.Mvc;
using CompanyData.Models;
using CompanyData.Services;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using log4net;

namespace CompanyData.Controllers
{
    [ApiController]
    [Route("company")]
    [Authorize]

    public class CompaniesController : ControllerBase
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public CompanyActions _companyActions;

        public CompaniesController()
        {
            if(_companyActions == null)
                _companyActions = new CompanyActions();
        }

        [HttpPost("create")]
        public ActionResult<Company> Create(Company company)
        {
            _log.Info("CALL: Create(Company company)");
            var companyID = _companyActions.Create(company);

            if(companyID > 0)
            {
                return Ok(companyID);
            }
            return Problem("Error under company create, exception saved in traces");
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public ActionResult<CompanySearchParameters> Search(CompanySearchParameters CompanySearchParameters)
        {
            _log.Info("CALL: Search(CompanySearchParameters CompanySearchParameters)");
            List<Company> companies = _companyActions.Search(CompanySearchParameters);

            if(companies.Count != 0)
                return Ok(new { Results = companies });
            return NotFound();
        }

        [HttpPut("update/{id}")]
        public ActionResult<Company> Update(long id, Company company)
        {
            _log.Info("CALL: Update(long id, Company company)");
            company.ID = id;
            var updateDone = _companyActions.Update(company);

            if (updateDone)
            {
                return Ok("Update Done");
            }
            return Problem("Error under company update, exception saved in traces");
        }

        [HttpDelete("delete/{id}")]
        public ActionResult Delete(long id)
        {
            _log.Info("CALL: Delete(long id)");
            var updateDone = _companyActions.Delete(id);

            if (updateDone)
            {
                return Ok("Update Done");
            }
            return Problem("Error under company delete, exception saved in traces");
        }
    }
}
