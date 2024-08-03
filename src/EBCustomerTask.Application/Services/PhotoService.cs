using EBCustomerTask.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace EBCustomerTask.Application.Services
{
	public class PhotoService : IPhotoService
	{
		private readonly string _photoStoragePath;
		private readonly string[] permittedExtensions = { ".jpg", ".jpeg", ".png" };
		public PhotoService(IWebHostEnvironment webHostEnvironment)
		{
			_photoStoragePath = Path.Combine(webHostEnvironment.WebRootPath, "photos");
		}

		public async Task<string> UploadPhotoAsync(IFormFile file, string fileName)
		{
			if (file is null || file.Length is 0)
			{
				throw new ArgumentException("Invalid file.");
			}

			var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
			if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
			{
				throw new InvalidOperationException("Invalid file type.");
			}

			var filePath = Path.Combine(_photoStoragePath, $"{fileName}{extension}");

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			return $"/photos/{fileName}{extension}";
		}
		public Task DeletePhotoAsync(string fileUrl)
		{
			var filePath = Path.Combine(_photoStoragePath, Path.GetFileName(fileUrl));
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
			return Task.CompletedTask;
		}
	}
}
