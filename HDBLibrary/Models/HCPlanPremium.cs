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
    public class HCPlanPremium
    {
        public string PlanName { get; set; }
        public decimal? Section1 { get; set; }
        public bool FreeAddOnsToSection1 { get; set; }
        public bool Section2 { get; set; }
        public decimal? Section3 { get; set; }
        public decimal? TotalSumInsured { get; set; }
        public decimal? Premium1YearAmount { get; set; }
        public decimal? Premium1YearAmountGST { get; set; }
        public decimal? Premium1YearAmountPerDay { get; set; }
        public decimal? Premium5YearAmount { get; set; }
        public decimal? Premium5YearAmountGST { get; set; }
        public decimal? Premium5YearAmountPerDay { get; set; }

        public bool LoadHCPlanPremiumByPlanName(string planName)
        {
            try
            {
                var p = new { PlanName = planName };

                using (IDbConnection cnn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    var output = cnn.QueryFirstOrDefault<HCPlanPremium>(@"select *
                                                                          from dbo.HCPLANPREMIUMS
                                                                          where PlanName = @PlanName", p);

                    this.PlanName = output.PlanName;
                    this.Section1 = output.Section1;
                    this.FreeAddOnsToSection1 = output.FreeAddOnsToSection1;
                    this.Section2 = output.Section2;
                    this.Section3 = output.Section3;
                    this.TotalSumInsured = output.TotalSumInsured;
                    this.Premium1YearAmount = output.Premium1YearAmount;
                    this.Premium1YearAmountGST = output.Premium1YearAmountGST;
                    this.Premium1YearAmountPerDay = output.Premium1YearAmountPerDay;
                    this.Premium5YearAmount = output.Premium5YearAmount;
                    this.Premium5YearAmountGST = output.Premium5YearAmountGST;
                    this.Premium5YearAmountPerDay = output.Premium5YearAmountPerDay;
                }
                return true;
            }
            catch (Exception ex)
            {
                //TODO: log error
                return false;
            }
        }

        public bool LoadHCPlanPremiumBySection1(decimal section1)
        {
            try
            {
                var p = new { Section1 = section1 };

                using (IDbConnection cnn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    var output = cnn.QueryFirstOrDefault<HCPlanPremium>(@"select *
                                                                          from dbo.HCPLANPREMIUMS
                                                                          where Section1 = @Section1", p);

                    this.PlanName = output.PlanName;
                    this.Section1 = output.Section1;
                    this.FreeAddOnsToSection1 = output.FreeAddOnsToSection1;
                    this.Section2 = output.Section2;
                    this.Section3 = output.Section3;
                    this.TotalSumInsured = output.TotalSumInsured;
                    this.Premium1YearAmount = output.Premium1YearAmount;
                    this.Premium1YearAmountGST = output.Premium1YearAmountGST;
                    this.Premium1YearAmountPerDay = output.Premium1YearAmountPerDay;
                    this.Premium5YearAmount = output.Premium5YearAmount;
                    this.Premium5YearAmountGST = output.Premium5YearAmountGST;
                    this.Premium5YearAmountPerDay = output.Premium5YearAmountPerDay;
                }
                return true;
            }
            catch (Exception ex)
            {
                //TODO: log error
                return false;
            }
        }

        public bool LoadHCPlanPremiumByFlatType(string flatType)
        {
            try
            {
                var p = new { HDBFlatType = flatType };

                using (IDbConnection cnn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    var output = cnn.QueryFirstOrDefault<HCPlanPremium>(@"select a.*
                                                                          from dbo.HCPLANPREMIUMS a 
                                                                                left join dbo.HCPLANRECOMMENDATIONS b on b.PlanName = a.PlanName
                                                                          where b.HDBFlatType = @HDBFlatType", p);
                    this.PlanName = output.PlanName;
                    this.Section1 = output.Section1;
                    this.FreeAddOnsToSection1 = output.FreeAddOnsToSection1;
                    this.Section2 = output.Section2;
                    this.Section3 = output.Section3;
                    this.TotalSumInsured = output.TotalSumInsured;
                    this.Premium1YearAmount = output.Premium1YearAmount;
                    this.Premium1YearAmountGST = output.Premium1YearAmountGST;
                    this.Premium1YearAmountPerDay = output.Premium1YearAmountPerDay;
                    this.Premium5YearAmount = output.Premium5YearAmount;
                    this.Premium5YearAmountGST = output.Premium5YearAmountGST;
                    this.Premium5YearAmountPerDay = output.Premium5YearAmountPerDay;
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
