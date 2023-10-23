using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Orders.Components
{
	public enum OrderByType
	{
		priceAsc,
		priceDesc,
		titleAsc,
		quantitySoldAsc,
		quantitySoldDesc,
		lastUpdate,
		newest,
		stockQuantityAsc,
		stockQuantityDesc
	}
}
