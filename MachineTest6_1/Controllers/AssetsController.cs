using MachineTest6_1.Model;
using MachineTest6_1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;
using NuGet.Protocol.Core.Types;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MachineTest6_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetsRepository _repository;


        //Dependency injuction DI ---Constructor Instrucor

        public AssetsController(IAssetsRepository repository)
        {
            _repository = repository;
        }

        #region 1- get all Assets
        [HttpGet]
        [Authorize(AuthenticationSchemes  ="Bearer")]
        public async Task<ActionResult<IEnumerable<AssetsMaster>>> GetAllAssets()
        {
            var assets = await _repository.GetAssets();
            if (assets == null)
            {
                return NotFound("No assets found");
            }

            return Ok(assets);
        }

        #endregion


        #region 2--Insert by Stored procedure

        [HttpPost("Add")]
        public async Task<ActionResult<IEnumerable<AssetsMaster>>> PostAssetsMasterByProcedureReturnRecord(AssetsMaster assets)
        {
            if (ModelState.IsValid)
            {
                //insert a new record and return as an object named employee

                var newaAssets = await _repository.PostAssetsByProcedureReturnRecord(assets);

                if (newaAssets != null)
                {
                    return Ok(newaAssets);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();
        }
        #endregion 

        #region 3- get all employees-search by id
        [HttpGet("{id}")]
        public async Task<ActionResult<AssetsMaster>> GetAssetsDetailsById(int id)
        {
            var assets = await _repository.GetAssetsById(id);
            if (assets == null)
            {
                return NotFound("No assets found");
            }

            return Ok(assets);
        }

        #endregion

        #region 4 update
        [HttpPut("{id}")]
        public async Task<ActionResult<AssetsMaster>> PutAssetsDetails(int id, AssetsMaster assets)
        {
            if (ModelState.IsValid)
            {
                //insert a new record and return as an object named employee

                var updateAssets = await _repository.PutAssets(id, assets);

                if (updateAssets != null)
                {
                    return Ok(updateAssets);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();

        }

        #endregion


        #region 5- delete an Assets

        [HttpDelete("{id}")]
        public ActionResult DeleteAssetsDetails(int id)
        {
            try
            {
                var result = _repository.DeleteAssets(id);

                if (result == null)
                {

                    return NotFound(new
                    {
                        success = false,
                        message = "room could not be deleted or not found"
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An unexpected error occurs" });
            }
        }

        #endregion

    }
}
