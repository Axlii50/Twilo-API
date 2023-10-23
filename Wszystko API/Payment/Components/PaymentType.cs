using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Payment.Components
{
	public enum PaymentType
	{
		fastTransfer,
		traditionalTransfer,
		card,
		blik,
		prePayment,
		installment
	}
}
