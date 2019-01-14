using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkiHouse.Web.Data.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a first address line")]
        [Display(Name = "Address1")]
        public string Address1 { get; set; }
        [Display(Name = "Address2")]
        public string Address2 { get; set; }
        [Display(Name = "Address3")]
        public string Address3 { get; set; }
        [Required(ErrorMessage = "Please enter a city name")]
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Display(Name = "Zip")]
        public string Zip { get; set; }
        [Required(ErrorMessage = "Please enter a country name")]
        [Display(Name = "Country")]
        public string Country { get; set; }
        public bool GiftWrap { get; set; }

        [BindNever]
        public bool Shipped { get; set; }
    }
}
