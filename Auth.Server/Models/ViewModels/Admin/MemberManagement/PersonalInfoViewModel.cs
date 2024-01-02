namespace Auth.Server.Models.ViewModels.Admin.MemberManagement
{
    public class PersonalInfoViewModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Religion { get; set; }
        public Guid? MotherTongue { get; set; }
        public Guid? EducationQualification { get; set; }
    }
}
