
using DinkToPdf;
using DinkToPdf.Contracts;
using Exam.Irepository.ISport;

using FstMonthExam.IRepository.Factory;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.Web.DiContaner
{
    public static class DiContainer
    {
        public static void AddCustomContainer(this IServiceCollection services, IConfiguration configuration)
        {
            //IConnectionFactory connectionFactory = new ConnectionFactory(configuration.GetConnectionString("DefaultConnection"));
            //services.AddSingleton(connectionFactory);
            //services.AddScoped<SpotInterface, PatientRepositary>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.AddControllers();



        }
    }
}
