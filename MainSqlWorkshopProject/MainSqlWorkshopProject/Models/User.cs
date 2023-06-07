using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSqlWorkshopProject.Models
{
	internal class User
	{
		public string FullName { get; set; }
		public string Title { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDay { get; set; }
		public int Age { get; set; }
		public decimal Salary { get; set; }
		public string CreditCard { get; set; }
		public DateTime registeredDate { get; set; }
		public int registeredAge { get; set; }
		public string Nat { get; set; }
		public string Gender { get; set; }
	}
}
