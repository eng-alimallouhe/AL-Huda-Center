using ClosedXML.Excel;
using LMS.Application.Abstractions.Services.Helpers;
using LMS.Application.DTOs.Admin.Financial;
using LMS.Application.DTOs.Admin.Sales;
using LMS.Application.DTOs.Stock;

namespace LMS.Infrastructure.Services.Helpers
{
    public class ExcelReportGeneratorHelper : IExcelReportGeneratorHelper
    {

        public byte[] GenerateFinancialReport(ICollection<PaymentDetailsDto> paymentsList, ICollection<FinancialDetailsDto> revenuesList)
        {
            using var workbook = new XLWorkbook();

            var expensesSheet = workbook.Worksheets.Add("Expenses");

            expensesSheet.Cell(1, 1).Value = "Payment ID";
            expensesSheet.Cell(1, 2).Value = "Employee";
            expensesSheet.Cell(1, 3).Value = "Amount";
            expensesSheet.Cell(1, 4).Value = "Details";
            expensesSheet.Cell(1, 5).Value = "Date";

            int row = 2;
            foreach (var payment in paymentsList)
            {
                expensesSheet.Cell(row, 1).Value = payment.PaymentId.ToString();
                expensesSheet.Cell(row, 2).Value = payment.EmployeeName;
                expensesSheet.Cell(row, 3).Value = payment.Amount;
                expensesSheet.Cell(row, 4).Value = payment.Details;
                expensesSheet.Cell(row, 5).Value = payment.Date;
                row++;
            }

            //style:
            var usedRange1 = expensesSheet.RangeUsed();
            usedRange1!.Style.Font.Bold = false;
            expensesSheet.Row(1).Style.Font.Bold = true;
            expensesSheet.SheetView.FreezeRows(1);
            expensesSheet.Columns().AdjustToContents();
            expensesSheet.Column(5).Style.DateFormat.Format = "dd/MM/yyyy";
            usedRange1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            usedRange1.Style.Alignment.WrapText = true;
            usedRange1.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            usedRange1.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            expensesSheet.Range("A1:E1").Style.Fill.BackgroundColor = XLColor.LightGray;


            var revenuesSheet = workbook.Worksheets.Add("Revenues");

            revenuesSheet.Cell(1, 1).Value = "Revenues ID";
            revenuesSheet.Cell(1, 2).Value = "Employee";
            revenuesSheet.Cell(1, 3).Value = "Customer";
            revenuesSheet.Cell(1, 4).Value = "Amount";
            revenuesSheet.Cell(1, 5).Value = "Targated Service";
            revenuesSheet.Cell(1, 6).Value = "Date";

            row = 2;
            foreach (var revenue in revenuesList)
            {
                revenuesSheet.Cell(row, 1).Value = revenue.FinancialId.ToString();
                revenuesSheet.Cell(row, 2).Value = revenue.EmployeeName;
                revenuesSheet.Cell(row, 3).Value = revenue.CustomerName;
                revenuesSheet.Cell(row, 4).Value = revenue.Amount;
                revenuesSheet.Cell(row, 5).Value = revenue.Service.ToString();
                revenuesSheet.Cell(row, 6).Value = revenue.CreatedAt;
                row++;
            }


            //style: 
            var usedRange2 = revenuesSheet.RangeUsed();
            usedRange2!.Style.Font.Bold = false;
            revenuesSheet.Row(1).Style.Font.Bold = true;
            revenuesSheet.SheetView.FreezeRows(1);
            revenuesSheet.Columns().AdjustToContents();
            revenuesSheet.Column(6).Style.DateFormat.Format = "dd/MM/yyyy";
            usedRange2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            usedRange2.Style.Alignment.WrapText = true;
            usedRange2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            usedRange2.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            revenuesSheet.Range("A1:F1").Style.Fill.BackgroundColor = XLColor.LightGray;


            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public byte[] GenerateStockReport(ICollection<StockSnapshotDto> stockList)
        {
            using var workbook = new XLWorkbook();


            var sheet = workbook.Worksheets.Add("Stock Snapshot");


            sheet.Cell(1, 1).Value = "Product ID";
            sheet.Cell(1, 2).Value = "Product Name";
            sheet.Cell(1, 3).Value = "Stock Quantity";
            sheet.Cell(1, 4).Value = "Unit Price";
            sheet.Cell(1, 5).Value = "Total Value";
            sheet.Cell(1, 6).Value = "Last Updated";
            sheet.Cell(1, 7).Value = "Logs Count";

            int row = 2;
            foreach (var stock in stockList)
            {
                sheet.Cell(row, 1).Value = stock.ProductId.ToString();
                sheet.Cell(row, 2).Value = stock.ProductName;
                sheet.Cell(row, 3).Value = stock.ProductStock;
                sheet.Cell(row, 4).Value = stock.ProductPrice;
                sheet.Cell(row, 5).Value = stock.TotalValue;
                sheet.Cell(row, 6).Value = stock.UpdatedAt;
                sheet.Cell(row, 7).Value = stock.LogsCount;
                row++;
            }


            sheet.Cell(row, 2).Value = "Totals:";
            sheet.Cell(row, 3).FormulaA1 = $"=SUM(C2:C{row - 1})"; 
            sheet.Cell(row, 4).FormulaA1 = $"=SUM(D2:D{row - 1})";
            sheet.Cell(row, 5).FormulaA1 = $"=SUM(E2:E{row - 1})";
            sheet.Cell(row, 7).FormulaA1 = $"=SUM(G2:G{row - 1})";



            var usedRange = sheet.RangeUsed();
            sheet.SheetView.FreezeRows(1);
            sheet.Columns().AdjustToContents();
            sheet.Row(1).Style.Font.Bold = true;
            sheet.Row(1).Style.Fill.BackgroundColor = XLColor.LightGray;
            sheet.Row(row).Style.Font.Bold = true;
            sheet.Row(row).Style.Fill.BackgroundColor = XLColor.LightYellow;
            usedRange!.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            usedRange.Style.Alignment.WrapText = true;
            sheet.Column(4).Style.NumberFormat.Format = "$ #,##0.00";
            sheet.Column(5).Style.NumberFormat.Format = "$ #,##0.00";
            sheet.Column(6).Style.DateFormat.Format = "dd/MM/yyyy";
            usedRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            usedRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public byte[] GeneratDeadStockReport(ICollection<DeadStockDto> deadStocks)
        {
            using var workbook = new XLWorkbook();
            var sheet = workbook.Worksheets.Add("Dead Stock");

            // Table Header:
            sheet.Cell(1, 1).Value = "Product Name";
            sheet.Cell(1, 2).Value = "Quantity";
            sheet.Cell(1, 3).Value = "Unit Price";
            sheet.Cell(1, 4).Value = "Adding Date";
            sheet.Cell(1, 5).Value = "Total Value";

            int row = 2;
            foreach (var item in deadStocks)
            {
                sheet.Cell(row, 1).Value = item.ProductName;
                sheet.Cell(row, 2).Value = item.ProductStock;
                sheet.Cell(row, 3).Value = item.ProductPrice;
                sheet.Cell(row, 4).Value = item.CreatedAt;
                sheet.Cell(row, 5).Value = item.ProductStock * item.ProductPrice;
                row++;
            }

            // Last Row for totals: 
            sheet.Cell(row, 1).Value = "Totals:";
            sheet.Cell(row, 2).FormulaA1 = $"=SUM(B2:B{row - 1})"; // quantity sum
            sheet.Cell(row, 3).FormulaA1 = $"=SUM(C2:C{row - 1})"; // price sum
            sheet.Cell(row, 5).FormulaA1 = $"=SUM(E2:E{row - 1})"; // total price sum

            // Table style:
            var usedRange = sheet.RangeUsed();
            sheet.SheetView.FreezeRows(1);
            sheet.Columns().AdjustToContents();
            sheet.Row(1).Style.Font.Bold = true;
            sheet.Row(1).Style.Fill.BackgroundColor = XLColor.LightGray;
            sheet.Row(row).Style.Font.Bold = true;
            sheet.Row(row).Style.Fill.BackgroundColor = XLColor.LightYellow;
            usedRange!.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            usedRange.Style.Alignment.WrapText = true;
            sheet.Column(4).Style.DateFormat.Format = "dd/MM/yyyy";
            sheet.Column(3).Style.NumberFormat.Format = "$ #,##0.00";
            sheet.Column(5).Style.NumberFormat.Format = "$ #,##0.00";
            usedRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            usedRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}