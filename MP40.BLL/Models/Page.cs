using MP40.DAL.Extensions;

namespace MP40.BLL.Models
{

	public class Page
	{
		protected const int DEFAULTPAGESIZE = 1;

		// The index of the currenlty displayed page
		public int PageIndex { get; set; }

		// Shouldn't ever happen (I hope)
		public int PageSize { get; set; }

		// The total number of pages
		public int? PageCount { get; set; }

		// The value of the current filter
		public string? Filter { get; set; }

		// The type of filter to apply
		public string? FilterBy { get; set; }

		// The type of ordering to apply
		public string? OrderBy { get; set; }

		public Page(int pageIndex, int pageSize = DEFAULTPAGESIZE)
		{
			PageIndex = pageIndex;
			PageSize = pageSize;
		}
	}

	public class Page<TModel> : Page where TModel : class
	{
		// The models on the current page
		public IEnumerable<TModel>? Models { get; set; }

		public Page(Page page, IEnumerable<TModel>? models) : base(page.PageIndex, page.PageSize) 
		{
			Models = models;
			this.CopyDataFrom(page);
		}

		/// <summary>
		/// Create a new page, and map all models from the given page to the new one
		/// </summary>
		/// <typeparam name="TMapped">The type of the new page</typeparam>
		/// <param name="mapping">A function to map models</param>
		/// <returns>A new page, containing the same values, except for the models since they have been mapped</returns>
		public Page<TMapped> Map<TMapped>(Func<TModel, TMapped> mapping) where TMapped : class
		{
			return new Page<TMapped>(this, Models?.Select(mapping));
		}
	}
}
