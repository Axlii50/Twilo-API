namespace Allegro_Api.Models.Offer
{
	public class OffersModel
    {
		/// <summary>
		/// The list of seller's offers matching the request's criteria
		/// </summary>
		public List<SimpleOfferModel> offers { get; set; }

		/// <summary>
		/// Number of offers in the search result.
		/// </summary>
		public int count { get; set; }

		/// <summary>
		/// The total number of offers matching the request's criteria.
		/// </summary>
		public int totalCount { get; set; }
    }
}
