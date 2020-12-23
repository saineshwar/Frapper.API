using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Frapper.ViewModel.Customers.Request
{
    public class CustomersRequest
    {
        [Required(ErrorMessage = "FirstName Required")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName Required")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Display(Name = "MobileNo")]
        [Required(ErrorMessage = "Mobile-no Required")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong Mobile-no")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "LandlineNo Required")]
        [Display(Name = "LandlineNo")]
        public string LandlineNo { get; set; }

        [Display(Name = "EmailId")]
        [Required(ErrorMessage = "EmailID Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail address")]
        public string EmailId { get; set; }

        [Display(Name = "Street")]
        [Required(ErrorMessage = "Street Required")]
        public string Street { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City Required")]
        public string City { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "State Required")]
        public string State { get; set; }

        [Display(Name = "Pincode")]
        [Required(ErrorMessage = "Pincode Required")]
        public string Pincode { get; set; }
    }
}
