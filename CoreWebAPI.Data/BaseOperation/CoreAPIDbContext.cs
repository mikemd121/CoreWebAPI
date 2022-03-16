using CoreWebAPI.Model;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CoreWebAPI.Data
{
    public class CoreAPIDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Resources/PopulationDatabase.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Estimate> Estimates { get; set; }
        public DbSet<Actual> Actuals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var estimateList = new List<Estimate>();
            var actualList = new List<Actual>();
            var actualSheetData = GetWorkSheetData("Actuals");
            var estimateSheetData = GetWorkSheetData("Estimates");

            GenerateEstimateTableData(estimateList, estimateSheetData);
            GenarateActualTableData(actualList, actualSheetData);

            modelBuilder.Entity<Estimate>().HasData(estimateList);
            modelBuilder.Entity<Actual>().HasData(actualList);
        }

        /// <summary>
        /// Gets the work sheet data.
        /// </summary>
        /// <param name="sheetName">Name of the sheet.</param>
        /// <returns></returns>
        static SheetData GetWorkSheetData(string sheetName)
        {
            var sheetData = new SheetData();
            var path = Path.Combine(AppContext.BaseDirectory, "Resources", "worksheetdata.xlsx");
            using (var doc = SpreadsheetDocument.Open(path, false))
            {
                var workbookPart = doc.WorkbookPart;
                var theSheetCollection = workbookPart.Workbook.GetFirstChild<Sheets>();
                foreach (var openXmlElement in theSheetCollection)
                {
                    var thesheet = (Sheet) openXmlElement;
                    if (thesheet.Name == sheetName)
                    {
                        var theWorksheet = ((WorksheetPart)workbookPart.GetPartById(thesheet.Id)).Worksheet;
                        sheetData = theWorksheet.GetFirstChild<SheetData>();
                    }
                }
            }

            return sheetData;
        }


        /// <summary>
        /// Generates the estimate table data.
        /// </summary>
        /// <param name="estmateList">The estmate list.</param>
        /// <param name="thesheetdata">The thesheetdata.</param>
        private static void GenerateEstimateTableData(List<Estimate> estmateList, SheetData thesheetdata)
        {
            foreach (var openXmlElement in thesheetdata)
            {
                var thecurrentrow = (Row) openXmlElement;
                var model = new Estimate();
                model.Id = Guid.NewGuid();
                model.State = Convert.ToInt32(thecurrentrow.ChildElements[0].InnerText);
                model.Districts = Convert.ToInt32(thecurrentrow.ChildElements[1].InnerText);
                model.EstimatesPopulation = Convert.ToInt32(thecurrentrow.ChildElements[2].InnerText);
                model.EstimatesHouseholds = Convert.ToInt32(thecurrentrow.ChildElements[3].InnerText);
                estmateList.Add(model);
            }
            estmateList.RemoveAt(0);
        }

        /// <summary>
        /// Genarates the actual table data.
        /// </summary>
        /// <param name="actualList">The actual list.</param>
        /// <param name="thesheetdata">The thesheetdata.</param>
        private static void GenarateActualTableData(List<Actual> actualList, SheetData thesheetdata)
        {
            foreach (var openXmlElement in thesheetdata)
            {
                var thecurrentrow = (Row) openXmlElement;
                var model = new Actual();
                model.State = Convert.ToInt32(thecurrentrow.ChildElements[0].InnerText);
                model.ActualPopulation = Convert.ToDouble(thecurrentrow.ChildElements[1].InnerText);
                model.ActualHouseholds = Convert.ToDouble(thecurrentrow.ChildElements[2].InnerText);
                actualList.Add(model);
            }
            actualList.RemoveAt(0);
        }
    }
}
