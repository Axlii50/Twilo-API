using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models
{
	public struct VerificationULRModel
	{
		/// <summary>
		/// user code
		/// </summary>
		public string user_code;

		/// <summary>
		/// application code
		/// </summary>
		public string device_code;

		/// <summary>
		/// amount of seconds for expire of both codes above
		/// </summary>
		public string expires_in;

		/// <summary>
		/// Interval is in seconds
		/// </summary>
		public int interval;

		/// <summary>
		/// verification url without embeded device code
		/// </summary>
		public string verification_uri;

		/// <summary>
		/// verification url with embeded device code
		/// </summary>
		public string verification_uri_complete;
	}
}
