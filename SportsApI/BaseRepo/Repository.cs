using Dapper;
using Exam.Domain.Sports;
using Exam.Irepository.ISport;
using FstMonthExam.IRepository.Factory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApI.BaseRepo
{
    public class Repository : Base, SpotInterface
    {
        public Repository(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task<List<Spot>> GetAll(Spot p)
        {
            try
            {
                var Connection = CreateConnection();
                if (Connection.State == ConnectionState.Closed) Connection.Open();

                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@mode", "Getall");
                var query = "spots";
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                var GetAppById = Connection.Query<Spot>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();
                return GetAppById;


            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public async Task<List<Spot>> BindClub()
        {
            try
            {
                var Connection = CreateConnection();
                if (Connection.State == ConnectionState.Closed) Connection.Open();

                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@mode", "BindClub");
                var query = "spots";
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                var GetAppById = Connection.Query<Spot>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();
                return GetAppById;


            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public async Task<List<Spot>> BindSportByClubId(int id)
        {
            try
            {
                var Connection = CreateConnection();
                if (Connection.State == ConnectionState.Closed) Connection.Open();


                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@club_id", id);
                ObjParm.Add("@mode", "BindSportByClubId");
                var query = "spots";
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                var GetAppById = Connection.Query<Spot>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();
                return GetAppById;

            }
            catch (Exception ex)
            {
                return null;

            }
        }


        public async Task<Spot> GetById(int PatientID)
        {
            try
            {
                var Connection = CreateConnection();
                if (Connection.State == ConnectionState.Closed) Connection.Open();


                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@Registration_Id", PatientID);
                ObjParm.Add("@mode", "GetById");
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);


                var query = "spots";
                var GetAppById = Connection.Query<Spot>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();
                return GetAppById[0];



            }
            catch (Exception ex)
            {
                return null;

            }
        }


        public async Task<int> insert(Spot om)
        {
            try
            {
                var Connection = CreateConnection();
                if (Connection.State == ConnectionState.Closed) Connection.Open();


                DynamicParameters param = new DynamicParameters();

                param.Add("@Registration_Id", om.Registration_Id);
                param.Add("@Applicant_name", om.Applicant_name);
                param.Add("@Email", om.Email);
                param.Add("@Mobile_no", om.Mobile_no);
                param.Add("@Gender", om.Gender);
                param.Add("@dob", om.dob);
                param.Add("@image_path", om.image_path);
                param.Add("@sport_Id", om.sport_Id);
                param.Add("@club_id", om.club_id);

                param.Add("@mode", "IU");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                Connection.Execute("[spots]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));

                return x;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }



    }
}

