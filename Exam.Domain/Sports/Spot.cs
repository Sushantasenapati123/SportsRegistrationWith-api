using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Exam.Domain.Sports
{
	public class Spot
	{
		[Key]
		public int Registration_Id { get; set; } = 0;
		[Required]
		public string Applicant_name { get; set; } = null;
		public string Email { get; set; } = null;
		public string Mobile_no { get; set; } = null;
		public string image_path { get; set; } = null;

		public string Gender { get; set; } = null;

		public string dob { get; set; }


		public int club_id { get; set; } = 0;//
		public int sport_Id { get; set; } = 0;//

		[NotMapped]
		public string club_name { get; set; } = null;
		[NotMapped]
		public string sprot_name { get; set; } = null;

	}
}