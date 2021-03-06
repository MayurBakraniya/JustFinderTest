//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JustFinderTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Contact_Us
    {
        [Key]
        public int id { get; set; }
        public Nullable<int> busi_id { get; set; }

        [Required(ErrorMessage = "Please Enter Your Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string email { get; set; }

        [Required(ErrorMessage = "Enter Your Subject Name")]
        public string subject { get; set; }

        [Required(ErrorMessage = "Enter Your Message")]
        public string msg { get; set; }

        public virtual Business_Owner Business_Owner { get; set; }
    }
}
