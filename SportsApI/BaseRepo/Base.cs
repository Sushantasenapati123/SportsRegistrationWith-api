using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApI.BaseRepo
{
    public class Base
    {
        private readonly IConfiguration _configuration;


        protected Base(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected SqlConnection CreateConnection()
        {
            return new SqlConnection
            (_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
