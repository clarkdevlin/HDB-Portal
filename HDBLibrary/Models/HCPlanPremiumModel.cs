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
    public static class HCPlanPremiumModel
    {
        public static List<HCPlanPremium> GetHCPlanPremiums()
        {
            var output = new List<HCPlanPremium>();
            try
            {
                using (IDbConnection cnn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    output = cnn.Query<HCPlanPremium>(@"select *
                                                        from dbo.HCPLANPREMIUMS").ToList();
                }
            }
            catch (Exception ex)
            {
                //TODO: log error
            }

            return output;
        }
    }
}
