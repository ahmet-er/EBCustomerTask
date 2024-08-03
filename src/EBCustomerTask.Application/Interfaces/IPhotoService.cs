using Microsoft.AspNetCore.Http;

namespace EBCustomerTask.Application.Interfaces
{
	public interface IPhotoService
	{
		Task<string> UploadPhotoAsync(IFormFile file, string fileName);
		Task DeletePhotoAsync(string fileUrl);
	}
}
