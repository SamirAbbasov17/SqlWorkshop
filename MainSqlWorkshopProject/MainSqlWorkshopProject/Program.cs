using MainSqlWorkshopProject.Models;
using Newtonsoft.Json;
using System.Data;
using System;
using static MainSqlWorkshopProject.Program;
using Microsoft.Data.SqlClient;

namespace MainSqlWorkshopProject
{
	internal partial class Program
	{
		private const string ConnectionString = "server=localhost;uid=sa;pwd=Pro247!!;database=SqlWorkshop;";
		HttpClient client = new HttpClient();
		static async Task Main(string[] args)
		{

			Program program = new Program();
			Dictionary<string, List<object>> allData = await program.GoTodoItems();

			foreach (var data in allData)
			{
				foreach (var item in data.Value.GetType().GetProperties())
				{
					
				}
			}

			using (SqlConnection connection = new(ConnectionString))
			{
				//string commandText = $"INSERT INTO Persons (FirstName, LastName, Phone, Mail, [Address]) VALUES('{person.FirstName}','{person.LastName}','{person.Phone}', '{person.Mail}', '{person.Address}')";
				foreach (var data in allData)
				{
					

						//Console.WriteLine("-------------------------------------------------");
						//Console.WriteLine(data.Key + " : ");
						foreach (var obj in data.Value)
						{

							List<string> dataList = new List<string>();
							List<string> dataListValues = new List<string>();
							foreach (var item in obj.GetType().GetProperties())
							{
								
								dataList.Add(item.Name);
								dataListValues.Add(item.GetValue(obj).ToString());
							}
							string commandText = $"INSERT INTO {data.Key} ({string.Join(",", dataList)}) VALUES({string.Join(",", dataListValues)})";


							using (SqlCommand command = new(cmdText: commandText, connection: connection))
							{
								connection.Open();
								command.ExecuteNonQuery();
							}

						//Console.WriteLine(item.Name + " : " + item.GetValue(obj));
						//List<string> values = new();
						//values.Add(item.GetValue(obj).ToString());

						//Console.WriteLine(string.Join(", " , values));
						//using (SqlCommand command = new(cmdText: commandText, connection: connection))
						//	{

						//		command.Parameters.AddWithValue("@FirstName", person.FirstName);
						//		command.Parameters.AddWithValue("LastName", person.LastName);

						//		command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = person.Phone;
						//		command.Parameters.Add("@Mail", SqlDbType.NVarChar).Value = person.Mail;
						//		command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = person.Address;

						//		connection.Open();
						//		command.ExecuteNonQuery();
						//	}
						Console.WriteLine(commandText);
							

						}
						//string commandText = $"INSERT INTO {data.Key} ({string.Join(",", obj.GetType().GetProperties().ToList())}) VALUES({item.GetValue(obj)})";

						//string commandText = $"INSERT INTO {data.Key} ({string.Join(",", obj.GetType().GetProperties().ToList())}) VALUES({item.GetValue(obj)})";
						
					
					
				}
			}
		}





		private async Task<Dictionary<string, List<object>>> GoTodoItems()
		{
			Random r = new();
			Dictionary<string, List<object>> DictionaryData = new Dictionary<string, List<object>>();
			List<object> userListData = new();
			List<object> cellListData = new();
			List<object> phoneListData = new();
			List<object> loginListData = new();
			List<object> locationListData = new();
			List<object> pictureListData = new();
			string response = await client.GetStringAsync("https://randomuser.me/api?results=5");
			var root = JsonConvert.DeserializeObject<Root>(response);
			for (int j = 0; j < 5; j++)
			{
				foreach (var person in root.results)
				{

					var user = new User
					{
						FullName = $"{person.name.title} {person.name.first} {person.name.last}",
						Title = person.name.title,
						FirstName = person.name.first,
						LastName = person.name.last,
						BirthDay = person.dob.date,
						Age = person.dob.age,
						Gender = person.gender,
						Salary = r.Next(500, 4000),
						CreditCard = r.Next(1000, 9999).ToString() + "-" + r.Next(1000, 9999).ToString() + "-" + r.Next(1000, 9999).ToString() + "-" + r.Next(1000, 9999).ToString(),
						Nat = person.nat
					};
					var cell = new UserCellPhone
					{
						CellNumber = person.cell
					};
					var location = new UserLocation
					{
						Street = person.location.street.name,
						City = person.location.city,
						State = person.location.state,
						PostCode = person.location.postcode.ToString(),
						Cordinates = person.location.coordinates.longitude + "," + person.location.coordinates.latitude,
						Timezone = person.location.timezone.offset,
						Country = person.location.country

					};
					var login = new UserLogin
					{
						Uuid = person.login.uuid,
						Username = person.login.username,
						Password = person.login.password,
						Salt = person.login.salt,
						Md5 = person.login.md5,
						Sha1 = person.login.sha1,
						Sha256 = person.login.sha256,
						LoginEmail = person.email
					};
					var phone = new UserPhone
					{
						PhoneNumber = person.phone
					};
					var picture = new UserPicture
					{
						Large = person.picture.large,
						Medium = person.picture.medium,
						Thumbnail = person.picture.thumbnail,
					};


					userListData.Add(user);
					loginListData.Add(login);
					cellListData.Add(cell);
					phoneListData.Add(phone);
					pictureListData.Add(picture);
					locationListData.Add(location);

				}
			}
			DictionaryData.Add("User", userListData);
			DictionaryData.Add("UserLocation", locationListData);
			DictionaryData.Add("UserCellPhone", cellListData);
			DictionaryData.Add("UserLogin", loginListData);
			DictionaryData.Add("UserPicture", pictureListData);
			DictionaryData.Add("UserPhone", phoneListData);



			return DictionaryData;
		}



	}






}





















//	}
//			}


//					DictionaryData.Add(user.GetType().Name, user);
//					DictionaryData.Add(cell.GetType().Name, cell);
//					DictionaryData.Add(location.GetType().Name, location);
//					DictionaryData.Add(login.GetType().Name, login);
//					DictionaryData.Add(phone.GetType().Name, phone);
//					DictionaryData.Add(picture.GetType().Name, picture);
//				}
//			}
//			return DictionaryData; 

//		}

//	}
//}