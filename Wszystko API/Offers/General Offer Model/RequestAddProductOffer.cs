﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Offers.Interfaces;

namespace Wszystko_API.Product
{
    public class RequestAddProductOffer : IBasicOffer
	{
		[JsonProperty("title")]
		public string Title { get; set; }
		public double Price { get; set; }
		public int CategoryId { get; set; }
		public string[] Gallery { get; set; }
		public string VatRate { get; set; }
		public List<ParameterKit> Parameters { get; set; }
		public List<Description> Descriptions { get; set; }
		public string GuaranteeId { get; set; }
		public string ComplaintPolicyId { get; set; }
		public string ReturnPolicyId { get; set; }
		public string ShippingTariffId { get; set; }
		public string LeadTime { get; set; }
		public string StockQuantityUnit { get; set; }
		public int UserQuantityLimit { get; set; }
		//[Required]
		[JsonProperty("isDraft")]
		public bool IsDraft { get; set; }
		public int StockQuantity { get; set; }
		[JsonProperty("status")]
		public OfferStatusType OfferStatus { get; set; }
		public bool ShowUnitPrice { get; set; }
	}
}
