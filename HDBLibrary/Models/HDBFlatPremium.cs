using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBLibrary.Models
{
    public class HDBFlatPremium
    {
        public string HDBFlatType { get; set; }
        public decimal? SumInsured { get; set; }
        public decimal? PremiumAmount { get; set; }
        public decimal? PremiumAmountGST { get; set; }
        public decimal? PremiumTotalAmount { get; set; }

        public bool LoadFlatPremium(string flatType)
        {
            try
            {
                var p = new { HDBFlatType = flatType };

                using (IDbConnection cnn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    var output = cnn.QueryFirstOrDefault<HDBFlatPremium>(@"select *
                                                                           from dbo.HDBFLATPREMIUMS
                                                                           where HDBFlatType = @HDBFlatType", p);

                    this.HDBFlatType = output.HDBFlatType;
                    this.SumInsured = output.SumInsured;
                    this.PremiumAmount = output.PremiumAmount;
                    this.PremiumAmountGST = output.PremiumAmountGST;
                    this.PremiumTotalAmount = output.PremiumTotalAmount;
                }
                return true;
            }
            catch (Exception ex)
            {
                //TODO: log error
                return false;
            }
        }
    }
}
