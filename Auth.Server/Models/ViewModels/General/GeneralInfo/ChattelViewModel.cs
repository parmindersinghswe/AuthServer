using Auth.Server.Data.Entities.General;

namespace Auth.Server.Models.ViewModels.General.GeneralInfo
{
    public class ChattelViewModel : Asset
    {
        public string CreatedByName { get; set; }
        public string ModifiedByName { get; set; }
    }
}
