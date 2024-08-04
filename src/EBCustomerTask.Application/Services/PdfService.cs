using DinkToPdf;
using DinkToPdf.Contracts;
using EBCustomerTask.Application.DTOs;
using EBCustomerTask.Application.Interfaces;

namespace EBCustomerTask.Application.Services
{
	public class PdfService : IPdfService
	{
		private readonly IConverter _converter;

		public PdfService(IConverter converter)
		{
			_converter = converter;
		}

		public byte[] GenerateCustomerPdf(List<CustomerGetAllViewModel> customers)
		{
			var htmlContent = GenerateHtmlTable(customers);
			var doc = new HtmlToPdfDocument()
			{
				GlobalSettings =
				{
					ColorMode = ColorMode.Color,
					Orientation = Orientation.Portrait,
					PaperSize = PaperKind.A4
				},
				Objects =
				{
					new ObjectSettings()
					{
						PagesCount = true,
						HtmlContent = htmlContent,
						WebSettings = 
						{ 
							DefaultEncoding = "utf-8",
							UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/lib/bootstrap/dist/css/bootstrap.css")
						},
						HeaderSettings =
						{
							FontSize = 9,
							Right = "Page [page] of [toPage]",
							Line = true,
							Spacing = 2.81
						}
					}
				}
			};

			return _converter.Convert(doc);
		}

		private string GenerateHtmlTable(List<CustomerGetAllViewModel> customers)
		{
			var html = "<html><head></head><body>";
			html += "<table class='table table-striped' align='center'><tr><th>First Name</th><th>Last Name</th><th>Email</th><th>Phone Number</th></tr>";

			foreach (var customer in customers)
			{
				html += $"<tr><td>{customer.FirstName}</td><td>{customer.LastName}</td><td>{customer.Email}</td><td>{customer.PhoneNumber}</td></tr>";
			}

			html += "</table></body></html>";
			return html;
		}
	}
}
