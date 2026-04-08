using System;
using System.Collections.Generic;

namespace LinqExamplesForFreshers_ExperiencedIndepth.NorthWind_Connect;

public partial class SummaryOfSalesByYear
{
    public DateTime? ShippedDate { get; set; }

    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
