using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace HDBLibrary.Models
{
    public static class HDBModel 
    {
        public static List<HDB> GetAllRecords()
        {
            var sql = @"select *
                        from dbo.VWHDBMASTER";

            using (IDbConnection cnn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                var output = cnn.Query<HDB>(sql).ToList();

                return output;
            }
        }

        public static List<HDBForTransamission> GetAllRecordsForTransmission()
        {
            var sql = @"select a.*,b.EntryDate
                        from dbo.VWHDBMASTER a left join dbo.TRANSACTIONSHDB b on b.HDBReferenceNo = a.HDBReferenceNo";

            using (IDbConnection cnn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                var output = cnn.Query<HDBForTransamission>(sql).ToList();

                return output;
            }
        }

        public static List<HDB> GetHDBRecords(string referenceNo)
        {
            var p = new
            {
                ReferenceNo = referenceNo
            };

            var sql = @"select * 
                        from dbo.VWHDBMASTER 
                        where HDBReferenceNo = @ReferenceNo";

            using (IDbConnection cnn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                return cnn.Query<HDB>(sql, p).ToList();
            }
        }

        public static List<HDB> GetHDBRecords(string addressPostalCode, string addressLevel, string addressUnit)
        {
            var p = new
            {
                AddressPostalCode = addressPostalCode.Trim(),
                AddressLevel = addressLevel.Trim(),
                AddressUnit = addressUnit.Trim()
            };

            var sql = @"select * 
                        from dbo.[VWHDBMASTER] 
                        where AddressPostalCode = @AddressPostalCode
                            and AddressLevel = @AddressLevel
                            and AddressUnit = @AddressUnit";

            using (IDbConnection cnn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                return cnn.Query<HDB>(sql, p).ToList();
            }
        }
        
        public static void InsertRecords(List<HDB> hdbs)
        {

            var sql = @"insert into dbo.[HDBMASTER] 
                        values (@HDBReferenceNo,
                                @InsuranceEffectiveDate,
                                @InsuranceTerm,
                                @BranchCode,                                
                                @AddressBlk,
                                @AddressLevel,
                                @AddressUnit,
                                @AddressStreetName,
                                @AddressPostalCode,
                                @FlatTypeClassification,
                                @CompulsoryStatus,
                                @TransactionType,
                                @PremiumAmount,
                                @PremiumAmountGST,
                                @SendDateToInsurer,
                                @NameOfInsurer,
                                @ReturnDateToHDB)";

            using (IDbConnection cnn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                try
                {
                    cnn.Execute(sql, hdbs);
                }
                catch (Exception)
                {
                    //TODO logo error
                }
                
            }
        }

        public enum SetName
        {
            SetA,
            SetB,
            SetC,
            SetD,
            SetE,
            SetF,
            SetG,
            SetH,
            SetI,
            SetJ,
            SetK,
            SetL,
            SetM,
            SetN
        }

        public enum RowType
        {
            Header,
            Record,
            Trailer
        }

        private static List<FileSpec> GetFileSpec(SetName setName, RowType rowType)
        {
            var p = new
            {
                SetName = setName.ToString().Replace("Set", ""),
                RowType = rowType.ToString().Substring(0, 1)
            };

            var sql = @"select *
                        from
                        (
	                        select DataItem, NoOfBytes, case @SetName when 'A' then SetA
											                        when 'B' then SetB
											                        when 'C' then SetC
											                        when 'D' then SetD
											                        when 'E' then SetE
											                        when 'F' then SetF
											                        when 'G' then SetG
											                        when 'H' then SetH
											                        when 'I' then SetI
											                        when 'J' then SetJ
											                        when 'K' then SetK
											                        when 'L' then SetL
											                        when 'M' then SetM
											                        when 'N' then SetN
								                        end as Enabled,
								                        case @SetName when 'A' then SetAPos
											                        when 'B' then SetBPos
											                        when 'C' then SetCPos
											                        when 'D' then SetDPos
											                        when 'E' then SetEPos
											                        when 'F' then SetFPos
											                        when 'G' then SetGPos
											                        when 'H' then SetHPos
											                        when 'I' then SetIPos
											                        when 'J' then SetJPos
											                        when 'K' then SetKPos
											                        when 'L' then SetLPos
											                        when 'M' then SetMPos
											                        when 'N' then SetNPos
								                         end as SetPos
	                        from dbo.[FILESPEC]
	                        where  case @SetName when 'A' then SetAPos
						                        when 'B' then SetBPos
						                        when 'C' then SetCPos
						                        when 'D' then SetDPos
						                        when 'E' then SetEPos
						                        when 'F' then SetFPos
						                        when 'G' then SetGPos
						                        when 'H' then SetHPos
						                        when 'I' then SetIPos
						                        when 'J' then SetJPos
						                        when 'K' then SetKPos
						                        when 'L' then SetLPos
						                        when 'M' then SetMPos
						                        when 'N' then SetNPos
			                        end is not null 
			                        and RowType = @RowType
                        ) a
                        order by a.SetPos";

            using (IDbConnection cnn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                return cnn.Query<FileSpec>(sql, p).ToList();
            }
        }

        public static List<HDB> ReadTransmissionFile(SetName setName, string path)
        {
            var lines = File.ReadAllLines(path);
            var hdbLines = new List<string>(lines);

            var headerLine = hdbLines[0];
            var trailerLine = hdbLines[hdbLines.Count - 1];

            hdbLines.RemoveAt(0);
            hdbLines.RemoveAt(hdbLines.Count - 1);

            var headerSpec = GetFileSpec(setName, RowType.Header);
            var trailerSpec = GetFileSpec(setName, RowType.Trailer);

            var output = new List<HDB>();

            var recordSpecs = GetFileSpec(setName, RowType.Record);

            foreach (var line in hdbLines)
            {
                var hdb = new HDB();
                hdb.HeaderIdentifier = headerLine.Substring(headerSpec[0].SetPos, headerSpec[0].NoOfBytes).Trim();
                var dateY = int.Parse(headerLine.Substring(headerSpec[1].SetPos, 4).Trim());
                var dateM = int.Parse(headerLine.Substring(headerSpec[1].SetPos + 4, 2).Trim());
                var dateD = int.Parse(headerLine.Substring(headerSpec[1].SetPos + 6, 2).Trim());
                hdb.ProcessDate = new DateTime(dateY, dateM, dateD);

                hdb.TrailerIdentifier = trailerLine.Substring(trailerSpec[0].SetPos, trailerSpec[0].NoOfBytes).Trim();
                hdb.TrailerNoOfCases = int.Parse(trailerLine.Substring(trailerSpec[1].SetPos, trailerSpec[1].NoOfBytes).Trim());
                dateY = int.Parse(trailerLine.Substring(trailerSpec[2].SetPos, 4).Trim());
                dateM = int.Parse(trailerLine.Substring(trailerSpec[2].SetPos + 4, 2).Trim());
                dateD = int.Parse(trailerLine.Substring(trailerSpec[2].SetPos + 6, 2).Trim());
                hdb.TrailerProcessDate = new DateTime(dateY, dateM, dateD);
                hdb.TrailerNameOfInsurer = trailerLine.Substring(trailerSpec[3].SetPos,trailerSpec[3].NoOfBytes).Trim();

                foreach (var record in recordSpecs)
                {
                    foreach (var prop in hdb.GetType().GetProperties())
                    {
                        if (prop.Name == record.DataItem && record.Enabled)
                        {
                            if (prop.PropertyType == typeof(DateTime?))
                            {
                                dateY = int.Parse(line.Substring(record.SetPos, 4).Trim());
                                dateM = int.Parse(line.Substring(record.SetPos + 4, 2).Trim());
                                dateD = int.Parse(line.Substring(record.SetPos + 6, 2).Trim());
                                prop.SetValue(hdb, new DateTime(dateY, dateM, dateD));
                            }
                            else if (prop.PropertyType == typeof(int))
                            {
                                prop.SetValue(hdb,int.Parse(line.Substring(record.SetPos, record.NoOfBytes).Trim()));
                            }
                            else
                            {
                                prop.SetValue(hdb, line.Substring(record.SetPos, record.NoOfBytes).Trim());
                            }
                        }
                    }
                }
                output.Add(hdb);
            }

            return output;
        }

        public static string WriteTranmissionFile(SetName setName, DateTime? processDate = null)
        {
            if (!processDate.HasValue)
                processDate = DateTime.Today;

            var headerSpec = GetFileSpec(setName, RowType.Header);
            var recordSpecs = GetFileSpec(setName, RowType.Record);
            var trailerSpec = GetFileSpec(setName, RowType.Trailer);

            var hdbList = GetAllRecordsForTransmission();

            var records = new List<HDBForTransamission>(); ;
            var output = new StringBuilder();

            switch (setName)
            {
                case SetName.SetC:
                    break;
                case SetName.SetG:
                    records = hdbList.Where(x => x.EntryDate.Value.Date >= processDate.Value.AddDays(-7).Date && 
                                                      x.EntryDate <= processDate.Value.Date).ToList();

                    

                    break;
                case SetName.SetM:
                    break;
                case SetName.SetN:
                    break;
                default:
                    break;
            }

            foreach (var record in records)
            {

            }


            return null;
        }

        public static List<APIResultModel> GetValidHDBRecord(APIVerifyModel model)
        {
            var sql = @"select *
                        from 
                        (
	                        select *
	                        from dbo.[VWHDBMASTER]
	                        where (InsuranceEffectiveDate is null or GETDATE() between DATEADD(DAY,-62,InsuranceExpiryDate) and InsuranceExpiryDate)
                        ) a
                        where a.HDBReferenceNo = @HDBReferenceNo and a.AddressPostalCode = @AddressPostalCode and cast(a.AddressLevel as int) = cast(@AddressLevel as int) and cast(a.AddressUnit as int) = cast(@AddressUnit as int) --and isnull(a.PremiumAmount,0) = 0";

            using (IDbConnection cnn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                return cnn.Query<APIResultModel>(sql, model).ToList();
            }
        }
    }
}
