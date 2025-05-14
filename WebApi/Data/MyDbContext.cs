using Microsoft.EntityFrameworkCore;

namespace WebApi.Data
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {


        }
    }

}
