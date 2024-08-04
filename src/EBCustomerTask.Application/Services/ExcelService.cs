using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using EBCustomerTask.Application.DTOs;
using EBCustomerTask.Application.Interfaces;

namespace EBCustomerTask.Application.Services
{
	public class ExcelService : IExcelService
	{
		public byte[] GenerateCustomersExcel(List<CustomerGetAllViewModel> customers)
		{
			using (var memoryStream = new MemoryStream())
			{
				using (var document = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook))
				{
					var workbookPart = document.AddWorkbookPart();
					workbookPart.Workbook = new Workbook();

					var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
					worksheetPart.Worksheet = new Worksheet(new SheetData());

					var sheets = document.WorkbookPart.Workbook.AppendChild(new Sheets());
					var sheet = new Sheet()
					{
						Id = document.WorkbookPart.GetIdOfPart(worksheetPart),
						SheetId = 1,
						Name = "Customers"
					};
					sheets.Append(sheet);

					var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

					var headerRow = new Row();
					headerRow.Append(
							new Cell { CellValue = new CellValue("First Name"), DataType = CellValues.String },
							new Cell { CellValue = new CellValue("Last Name"), DataType = CellValues.String },
							new Cell { CellValue = new CellValue("Email"), DataType = CellValues.String },
							new Cell { CellValue = new CellValue("Phone Number"), DataType = CellValues.String }
						);
					sheetData.AppendChild(headerRow);

					foreach (var customer in customers)
					{
						var row = new Row();
						row.Append(
								new Cell { CellValue = new CellValue(customer.FirstName), DataType = CellValues.String },
								new Cell { CellValue = new CellValue(customer.LastName), DataType = CellValues.String },
								new Cell { CellValue = new CellValue(customer.Email), DataType = CellValues.String },
								new Cell { CellValue = new CellValue(customer.PhoneNumber), DataType = CellValues.String }
							);
						sheetData.AppendChild(row);
					}
				}

				return memoryStream.ToArray();
			}
		}
	}
}
