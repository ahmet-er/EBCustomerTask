using EBCustomerTask.Application.DTOs;

namespace EBCustomerTask.Application.Interfaces
{
	public interface IExcelService
	{
		byte[] GenerateCustomersExcel(List<CustomerGetAllViewModel> customers);
	}
}
