using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models
{
	public struct AccessTokenModel
	{
		/// <summary>
		/// Access token for api after Oauth
		/// and is viable for 12 hours
		/// </summary>
		public string access_token;

		/// <summary>
		/// token type
		/// for us will be ussualy: bearer
		/// </summary>
		public string token_type;

		/// <summary>
		/// refresh token is viable for 3 months and is used for refreshing acces token
		/// </summary>
		public string refresh_token;

		/// <summary>
		/// after how many seconds acces token will expire 
		/// </summary>
		public int expires_in;

		/// <summary>
		/// zasięg danych/funkcjonalności do których użytkownik autoryzował aplikacje
		/// </summary>
		public string scope;

		/// <summary>
		/// flaga wskazująca na fakt, że token został wygenerowany dla celów API(brakbezpośredniego zastosowania)
		/// </summary>
		public bool allegro_api;

		/// <summary>
		/// identyfikator tokena JWT (brak bezpośredniego zastosowania)
		/// </summary>
		public string jti;
	}
}
