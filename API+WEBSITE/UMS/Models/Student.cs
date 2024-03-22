using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UMS.Models
{
    public class Student
    {
        public Guid Id { get; set; }

        [DisplayName("Full Name *")]
        [Required(ErrorMessage = "Full Name is Required")]
        public string FullName { get; set; }

        [DisplayName("Email *")]
        [EmailAddress]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }

        [DisplayName("Date of Birth *")]
        [Required(ErrorMessage = "Date of Birth is Required")]
        public DateTime Dob { get; set; }


        [DisplayName("Subject *")]
        [Required(ErrorMessage = "Subject is Required")]
        public Guid SubjectId { get; set; }

        public string Address { get; set; }

        [DisplayName("Want Marketing Update?")]
        public bool WillAgreeWithMarketingUpdate { get; set; }

        [DisplayName("Want Cor. Welsh?")]
        public bool WillWantCorWelsh { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? LastModifiedOn { get; set; }
        public Subject Subject { get; set; }
    }
}
