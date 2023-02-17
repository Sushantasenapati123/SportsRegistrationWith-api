using Dapper;
using Exam.Domain.Sports;
using Exam.Irepository.ISport;
using FstMonthExam.IRepository.Factory;

using PathoLab.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Repository.PatientRepo
{
    public class PatientRepositary : RepositoryBase, SpotInterface
    {
        public PatientRepositary(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
        public async Task<List<Spot>> GetAll(Spot p)
        {
            try
            {
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
   

        public async  Task<Spot> GetById(int PatientID)
        {
            try
            {

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

