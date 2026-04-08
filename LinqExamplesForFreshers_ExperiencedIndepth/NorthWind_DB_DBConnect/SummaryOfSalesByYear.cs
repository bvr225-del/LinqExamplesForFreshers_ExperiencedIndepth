using System;
using System.Collections.Generic;

namespace LinqExamplesForFreshers_ExperiencedIndepth.NorthWind_DB_DBConnect;

public partial class SummaryOfSalesByYear
{
    public DateTime? ShippedDate { get; set; }

    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
