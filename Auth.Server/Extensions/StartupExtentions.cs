using Auth.Server.Data.Context;
using Auth.Server.Services.Auth;
using Auth.Server.Services.Utility;
using Auth.Server.Utilities;

namespace Auth.Server.Extensions
{
    public static class StartupExtentions
    {

        public static void InjectSingleton(this IServiceCollection services)
        {

        }
        public static void InjectTransiant(this IServiceCollection services)
        {
            services.AddTransient<IMailService, MailService>();
        }
        public static void InjectScopped(this IServiceCollection services)
        {
            #region Inject API Controllers
            //services.AddScoped<APIController<Category, CategoryModel, int?>, APIController<Category, CategoryModel, int?>>();
            #endregion

            services.AddScoped<IJwtUtils, JwtUtils>();
            //services.AddScoped<IAddressRepository, AddressRepository>();
            //services.AddScoped<IAssetRepository, AssetRepository>();
            //services.AddScoped<IAssetTypeRepository, AssetTypeRepository>();
            //services.AddScoped<IContactRepository, ContactRepository>();
            //services.AddScoped<ICountryRepository, CountryRepository>();
            //services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            //services.AddScoped<IDistrictRepository, DistrictRepository>();
            //services.AddScoped<IGeneralServiceRepository, GeneralServiceRepository>();
            //services.AddScoped<ILocalityRepository, LocalityRepository>();
            //services.AddScoped<IMemberRepository, MemberRepository>();
            //services.AddScoped<IModuleTypeRepository, ModuleTypeRepository>();
            //services.AddScoped<IOtherInformationRepository, OtherInformationRepository>();
            //services.AddScoped<IServiceAssetRepository, ServiceAssetRepository>();
            //services.AddScoped<IServiceCostPerUnitRepository, ServiceCostPerUnitRepository>();
            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<IStateRepository, StateRepository>();
            //services.AddScoped<ITimeUnitRepository, TimeUnitRepository>();

            //services.AddScoped<IAddressService, AddressService>();
            //services.AddScoped<IAssetService, AssetService>();
            //services.AddScoped<IAssetTypeService, AssetTypeService>();
            //services.AddScoped<IContactService, ContactService>();
            //services.AddScoped<ICountryService, CountryService>();
            //services.AddScoped<ICurrencyService, CurrencyService>();
            //services.AddScoped<IDistrictService, DistrictService>();
            //services.AddScoped<IGenService, GenService>();
            //services.AddScoped<ILocalityService, LocalityService>();
            //services.AddScoped<IMemberService, MemberService>();
            //services.AddScoped<IModuleTypeService, ModuleTypeService>();
            //services.AddScoped<IOtherInformationService, OtherInformationService>();
            //services.AddScoped<IServiceAssetService, ServiceAssetService>();
            //services.AddScoped<IServiceCostPerUnitService, ServiceCostPerUnitService>();
            //services.AddScoped<ICategoryService, CategoryService>();
            //services.AddScoped<IStateService, StateService>();
            //services.AddScoped<ITimeUnitService, TimeUnitService>();


            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            //services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserAuthorizationService, UserAuthorizationService>();
            //services.AddScoped<IBlockService, BlockService>();
            //services.AddScoped<IDeleteService, DeleteService>();
        }

    }
}
