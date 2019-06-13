using HDBLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDB_Trasnsfer_File_Processor
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"\\ecicsfs\Department\Operations\Operations\HDB Processing\SETL.HDB";

            //var list = HDBModel.ReadTransmissionFile(HDBModel.SetName.SetL, path);

            //HDBModel.InsertRecords(list);

            //var records = HDBModel.GetAllRecords();

            //foreach (var rec in records)
            //{
            //    Console.WriteLine("-------------------------------------------------");
            //    Console.WriteLine($"ReferenceNo: {rec.HDBReferenceNo}");
            //    Console.WriteLine($"Blk: {rec.AddressBlk}");
            //    Console.WriteLine($"Level: {rec.AddressLevel}");
            //    Console.WriteLine($"Unit: {rec.AddressUnit}");
            //    Console.WriteLine($"StreetName: {rec.AddressStreetName}");
            //    Console.WriteLine($"Level: {rec.AddressPostalCode}");
            //}

            var hdb = HDBModel.GetHDBRecords("88899966601")[0];

            

            Console.ReadLine();
        }
    }

   
}

