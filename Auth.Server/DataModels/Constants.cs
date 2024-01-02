using Auth.Server.Data.Entities.General;

namespace Auth.Server.DataModels
{
    public static class DateModelConstants
    {
        public enum SortOrder
        {
            Asc,
            Desc
        }
        public enum EntityActionTypes : ushort
        {
            admin = 1,
            category = 2,
            user = 3,
            service = 4,
            state = 5,
            district = 6,
            location = 7
        }
        public enum EntityTypes : ushort
        {
            Category = 1,
            Service = 2,
            User = 3
        }
        public enum Role
        {
            SuperAdmin,
            Admin,
            User
        }
        public static class SeedableData
        {
            public static List<Country> Countries
            {
                get
                {
                    return new List<Country> { new Country()
                {
                    Id = 1,
                    CreatedById = 1,
                    CreatedOn = DateTime.Now,
                    ModifiedById = 1,
                    ModifiedOn = DateTime.Now,
                    ISOCode = "IN",
                    Name = "India",
                    IsDeleted = false
                } };
                }
            }
         
            public static List<Locality> Localities
            {
                get
                {
                    return new List<Locality>() { new Locality(){
                    Id = 1,
                    CreatedById = 1,
                    ModifiedById = 1,
                    CreatedOn = DateTime.Now,
                    ModifiedOn =DateTime.Now,
                    DistrictId = 1,
                    Name = "Manvi",
                    Description = "Village Manvi",
                    IsCity = false,
                    IsDeleted = false
                    }
                    };
                }
            }
        }
    }
}
