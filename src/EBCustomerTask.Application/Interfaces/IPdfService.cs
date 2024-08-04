using EBCustomerTask.Application.DTOs;

namespace EBCustomerTask.Application.Interfaces
{
	public interface IPdfService
	{
		byte[] GenerateCustomerPdf(List<CustomerGetAllViewModel> customers);
	}
}
