

using static Auth.Server.DataModels.DateModelConstants;

namespace Auth.Server.Models
{
    public class FilterModel<KeyType>
    {
        public KeyType Id { get; set; }
        public ushort PageIndex { get; set; } = 0;
        public ushort PageSize { get; set; } = 10;
        public string Sortby { get; set; } = string.Empty;
        public string SearchBy { get; set; } = string.Empty;
        public SortOrder Order { get; set; } = SortOrder.Asc;
    }
}
