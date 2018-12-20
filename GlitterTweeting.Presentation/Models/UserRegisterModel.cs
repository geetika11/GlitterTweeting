﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GlitterTweeting.Presentation.Models
{
    public class UserRegisterModel
    {

        [Required(ErrorMessage = "Last Name cannot be empty")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Email Address Required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact Number Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone number format is not valid.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Invalid Password format")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Image { get; set; }

        public string Country { get; set; }
    }
}