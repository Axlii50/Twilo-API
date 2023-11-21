using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Offers.Simple_Offer_Model.Interface;

namespace Wszystko_API.Offers.Simple_Offer_Model.JsonConverter
{
	public class DownloadOffersModelConverter: JsonConverter<IDownloadOffersModel>
	{
		private readonly bool isFullDetail;

		public DownloadOffersModelConverter(bool isFullDetail)
		{
			this.isFullDetail = isFullDetail;
		}

		public override IDownloadOffersModel? ReadJson(JsonReader reader, Type objectType, IDownloadOffersModel? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JObject jsonObject = JObject.Load(reader);

			if (!isFullDetail)
			{
				System.Diagnostics.Debug.WriteLine("działa");
				const string jsonProperty = "Records";

				JToken propertyValue = jsonObject["Offers"];

				JProperty newProperty = new JProperty(jsonProperty, propertyValue);

				var jso = jsonObject.Property("Offers");

				System.Diagnostics.Debug.WriteLine(jsonObject.Remove(jsonProperty));

				jsonObject.Add(newProperty);

				return jsonObject.ToObject<SimpleDownloadOffersModel>();
			}
			else
			{
				var obj = jsonObject.ToObject<DetailedDownloadOffersModel>();
				return obj;
			}
		}

		public override void WriteJson(JsonWriter writer, IDownloadOffersModel? value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
