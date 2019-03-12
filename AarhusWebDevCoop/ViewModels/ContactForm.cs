using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AarhusWebDevCoop.ViewModels
{
    public class ContactForm
    {
        [Required(ErrorMessage = "Please enter your name")]
        public String Name { get; set; }
        [Required(ErrorMessage = "Please enter your email")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Please enter your subject")]
        public String Subject { get; set; }
        [Required(ErrorMessage = "Please enter your message")]
        public String Message { get; set; }

    }
}