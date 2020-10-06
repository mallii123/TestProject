using Microsoft.AspNetCore.Mvc;
using CompanyData.Models;
using CompanyData.Data;
using Microsoft.AspNetCore.Authorization;

namespace CompanyData.Controllers
{
    [ApiController]
    [Route("company")]
    [Authorize]

    public class CompaniesController : ControllerBase
    {

        private CompanyActions _companyActions;

        public CompaniesController()
        {
            if(_companyActions == null)
                _companyActions = new CompanyActions();
        }

        [HttpPost("create")]
        public ActionResult<Company> Create(Company company)
        {
            var companyID = _companyActions.Create(company);

            if(companyID > 0)
            {
                return Ok(companyID);
            }
            return NotFound();
        }
        [HttpPost("search")]
        [AllowAnonymous]
        public ActionResult<CompanySearchParameters> Search(CompanySearchParameters CompanySearchParameters)
        {
            var companies = _companyActions.Search(CompanySearchParameters);
            return Ok(new { Results = companies });
        }
        [HttpPut("update/{id}")]
        public ActionResult<Company> Update(long id, Company company)
        {
            company.ID = id;

            var udpateDone = _companyActions.Update(company);
            if (udpateDone)
            {
                return Ok("Update Done");
            }
            return NotFound();
        }
        [HttpDelete("delete/{id}")]
        public ActionResult Delete(long id)
        {
            var udpateDone = _companyActions.Delete(id);
            if (udpateDone)
            {
                return Ok("Update Done");
            }
            return NotFound();
        }
    }
}
