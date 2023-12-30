using Auth.Server.Models.ViewModels.Admin.MemberManagement;

namespace Auth.Server.Models.ViewModels.Account
{
    public class MemberInfoViewModel
    {
        public List<MemberView> Members { get; set; }
        public int TotalNumberOfMembers { get; set; }
        public MemberInfoViewModel(int totalNumberOfMembers)
        {
            TotalNumberOfMembers = totalNumberOfMembers;
            Members = new List<MemberView>();
        }
        public MemberInfoViewModel()
        {

        }
        public MemberDetailViewModel MemberDetail { get; set; }
    }
}
