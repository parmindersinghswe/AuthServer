namespace Auth.Server.Models.ViewModels.Admin.MemberManagement
{
    public class MemberDetailViewModel
    {
        public Guid Id { get; set; }
        public PersonalInfoViewModel PersonalInfo { get; set; }
        public ContactInfoViewModel ContactInfo { get; set; }
        public AddressInfoViewModel AddressInfo { get; set; }
    }
}
