using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyTripApi.Data;

namespace MyTripApi.Extesions
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {

            var builder = services.AddIdentityCore<ApiUser>(option =>
            {
                option.Password.RequiredLength = 8;
                option.User.RequireUniqueEmail = true;

            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<MyTripDbContext>().AddDefaultTokenProviders();
        }
    }
}
