using System;
using System.Collections.Generic;

namespace Given.DataContext.IMSEntities
{
    public partial class Inventory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public string SupplierName { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
