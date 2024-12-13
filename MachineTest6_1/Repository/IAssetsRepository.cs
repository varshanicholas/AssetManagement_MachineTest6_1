using MachineTest6_1.Model;
using Microsoft.AspNetCore.Mvc;

namespace MachineTest6_1.Repository
{
    public interface IAssetsRepository
    {

        #region 1-Get all employees
        public Task<ActionResult<IEnumerable<AssetsMaster>>> GetAssets();
        #endregion

        #region-2
        public Task<ActionResult<IEnumerable<AssetsMaster>>> PostAssetsByProcedureReturnRecord(AssetsMaster assets);

        #endregion

        #region -3 Get an employee based on id

        //Get an Assets based on Id
        public Task<ActionResult<AssetsMaster>> GetAssetsById(int id);


        #endregion

        #region 4-  update
        public Task<ActionResult<AssetsMaster>> PutAssets(int id, AssetsMaster assets);

        #endregion

        #region -5  delete 


        public JsonResult DeleteAssets(int id);

        #endregion

    }
}
