using MainSqlWorkshopProject.Models;
using Newtonsoft.Json;

namespace MainSqlWorkshopProject
{
	internal partial class Program
	{
		HttpClient client = new HttpClient();
		static async Task Main(string[] args)
		{
			Program program = new Program();
			List<User> allData = await program.GoTodoItems();
			int b = 0;

			foreach (var item in allData)
			{
				b++;
                await Console.Out.WriteLineAsync(
					$"""
					ID        :{b}
					FullName  :{item.FullName}
					Name      :{item.FirstName}
					LastName  :{item.LastName}
					Title     :{item.Title}
					Gender    :{item.Gender}
					Age       :{item.Age}
					BirthDay  :{item.BirthDay}
					Salary    :{item.Salary}
					CCard     :{item.CreditCard}
					----------------------------------------
					"""
					);
            }
		}


		private async  Task<List<User>> GoTodoItems()
		{
			Random r = new();
			List<User> dataList = new();
			string response = await client.GetStringAsync("https://randomuser.me/api?results=5000");
			var root = JsonConvert.DeserializeObject<Root>(response);
			for (int j = 0; j < 1000; j++)
			{
				foreach (var person in root.results)
				{
					var data = new User
					{
						FullName = $"{person.name.title} {person.name.first} {person.name.last}",
						Title = person.name.title,
						FirstName = person.name.first,
						LastName = person.name.last,
						BirthDay = person.dob.date,
						Age = person.dob.age,
						Gender = person.gender,
						Salary = r.Next(500,4000),
						CreditCard = r.Next(1000,9999).ToString() + "-" + r.Next(1000, 9999).ToString() + "-" + r.Next(1000, 9999).ToString() + "-" + r.Next(1000, 9999).ToString()
					};

					dataList.Add(data);
				}
			}
			return dataList; 
			
		}

	}
}