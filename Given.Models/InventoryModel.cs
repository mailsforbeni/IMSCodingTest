using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Given.Models
{
    public class CommonMethods
    {
        public static ApiResult CommonAPIResult(string ReturnData, int ReturnCode, string ReturnMessage, bool ReturnStatus)
        {
            ApiResult apiResult = new ApiResult();
            apiResult.ReturnData = ReturnData;
            apiResult.ReturnCode = ReturnCode;
            apiResult.ReturnMessage = ReturnMessage;
            apiResult.ReturnStatus = ReturnStatus;

            return apiResult;
        }
    }
    public class InventoryModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Stock Name is required.")] 
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        [Range(typeof(double), "0", "9999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        public double? Price { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        public string SupplierName { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }

    public class ApiResult
    {
        public int ReturnCode { get; set; }
        public object ReturnMessage { get; set; }
        public object ReturnData { get; set; }
        public bool ReturnStatus { get; set; }
        public ApiResult()
        {
            ReturnCode = 0;
            ReturnMessage = null;
            ReturnData = null;
            ReturnStatus = false;
        } 
    }
}
