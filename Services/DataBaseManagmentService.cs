using Microsoft.EntityFrameworkCore;
using WizStore.Data;

namespace WizStore.Services
{
    public static class DataBaseManagmentService
    {
        public static void MigrationInitialization(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var serviceDB = serviceScope.ServiceProvider.GetService<DataContext>();

            serviceDB?.Database.Migrate();
        }
    }
}
