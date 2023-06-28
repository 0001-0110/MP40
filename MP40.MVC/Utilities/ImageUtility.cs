using BllImage = MP40.BLL.Models.Image;
using MP40.MVC.Models;

namespace MP40.MVC.Utilities
{
	internal static class ImageUtility
	{
		private const long MAXSIZE = 50 * 1024 * 1024;

		public static Image? ToMvcImage(BllImage? image)
		{
			return image == null ? null : new()
			{
				Id = image.Id,
				Content = null!,
				Base64 = image.Content,
			};
		}

		public static BllImage? ToBllImage(Image? image)
		{
			if (image == null)
				return null;

			using MemoryStream memoryStream = new();
			image.Content.CopyTo(memoryStream);

			byte[] imageBytes = memoryStream.ToArray();
			return new BllImage()
			{
				Id = image.Id,
				Content = memoryStream.Length > MAXSIZE ? null! : Convert.ToBase64String(imageBytes),
			};
		}
	}
}
