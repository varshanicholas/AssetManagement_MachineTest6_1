using MachineTest6_1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NuGet.ContentModel;
using System.Drawing;

namespace MachineTest6_1.Repository
{
    public class AssetsRepository : IAssetsRepository
    {
        private readonly AssetsContext _context;

        //DI -Constructor injection

        public AssetsRepository(AssetsContext context)
        {
            _context = context;
        }

        #region 1- Get all dtails
        public async Task<ActionResult<IEnumerable<AssetsMaster>>> GetAssets()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.AssetsMasters
                        .Include(b => b.PurchaseOrder)

                        .ToListAsync();
                }

                // Return an empty list if context is null
                return new List<AssetsMaster>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region 2- Add 
        public async Task<ActionResult<IEnumerable<AssetsMaster>>> PostAssetsByProcedureReturnRecord(AssetsMaster assets)
        {
            try
            {
                //check if employee object is not null

                if (assets == null)
                {
                    throw new ArgumentNullException(nameof(assets), "assets Data is Null");
                }

                //ensure the context is not null

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                //call stored procedure[InsertAsset]

                var result = await _context.AssetsMasters.FromSqlRaw(
               "EXEC InsertAsset @Name, @AssetTypeId, @PurchaseOrderId, @SerialNumber, @Model, @Manufacturer, @WarrantyPeriod, @PurchaseDate, @Cost, @FromDate, @ToDate",
               new SqlParameter("@Name", assets.Name),
               new SqlParameter("@AssetTypeId", assets.AssetTypeId),
               new SqlParameter("@PurchaseOrderId", assets.PurchaseOrderId ?? (object)DBNull.Value), // Handle nullable
               new SqlParameter("@SerialNumber", assets.SerialNumber ?? (object)DBNull.Value), // Handle nullable
               new SqlParameter("@Model", assets.Model ?? (object)DBNull.Value), // Handle nullable
               new SqlParameter("@Manufacturer", assets.Manufacturer ?? (object)DBNull.Value), // Handle nullable
               new SqlParameter("@WarrantyPeriod", assets.WarrantyPeriod ?? (object)DBNull.Value), // Handle nullable
               new SqlParameter("@PurchaseDate", assets.PurchaseDate ?? (object)DBNull.Value), // Handle nullable
               new SqlParameter("@Cost", assets.Cost ?? (object)DBNull.Value), // Handle nullable
               new SqlParameter("@FromDate", assets.FromDate ?? (object)DBNull.Value), // Handle nullable
               new SqlParameter("@ToDate", assets.ToDate ?? (object)DBNull.Value) // Handle nullable
           ).ToListAsync();

                if (result != null && result.Count > 0)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion 

        #region -3 get Assets details based on id


        public async Task<ActionResult<AssetsMaster>> GetAssetsById(int id)
        {
            try
            {
                if (_context != null)
                {
                    //find the employee by id 

                    var assets = await _context.AssetsMasters
                        .Include(b => b.PurchaseOrder).FirstOrDefaultAsync(e => e.AssetTypeId == id);
                    return assets;
                }

                return null;

            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion



        #region 4 --Update a Assets detail with ID 

        public async Task<ActionResult<AssetsMaster>> PutAssets(int id, AssetsMaster assets)
        {
            try
            {
                if (assets == null)
                {
                    throw new ArgumentNullException(nameof(assets), "Assets data is null");
                }

                // Ensure the context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                // Call the stored procedure to update the asset cost and dates
                var result = await _context.AssetsMasters.FromSqlRaw(
                    "EXEC UpdateAssetCostDates @Id, @Cost, @FromDate, @ToDate",
                    new SqlParameter("@Id", id),
                    new SqlParameter("@Cost", assets.Cost),
                    new SqlParameter("@FromDate", assets.FromDate ?? (object)DBNull.Value), // Handle nullable
                    new SqlParameter("@ToDate", assets.ToDate ?? (object)DBNull.Value) // Handle nullable
                ).ToListAsync();

                // Return the updated asset or null if not found
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return null;
            }
        }

        #endregion


        #region 5 --delete

        public JsonResult DeleteAssets(int id)
        {
            try
            {

                //check if employee object is not null
                if (id == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "invalid Assets id. Please enter Correct Id"

                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };

                }

                //ensure the context is not null

                if (_context == null)
                {

                    return new JsonResult(new
                    {

                        success = false,
                        message = "Database context is not initialized. "

                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }

                //find the employee by ID
                var existingAssets = _context.AssetsMasters.Find(id);

                if (existingAssets == null)
                {
                    return new JsonResult(new
                    {

                        success = false,
                        message = "Assets Details Not Found . "

                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                //remove the employee record from the database

                _context.AssetsMasters.Remove(existingAssets);

                //save changes to the database

                _context.SaveChangesAsync();

                return new JsonResult(new
                {

                    success = false,
                    message = "Assets Details Deleted Successfully . "

                })
                {
                    StatusCode = StatusCodes.Status200OK
                };




            }

            catch (Exception ex)
            {

                return new JsonResult(new
                {

                    success = false,
                    message = "An error accured "

                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        #endregion


    }
}
