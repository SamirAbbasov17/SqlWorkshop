using MainSqlWorkshopProject.Models;
using Newtonsoft.Json;
using System.Data;
using System;
using static MainSqlWorkshopProject.Program;
using Microsoft.Data.SqlClient;
using MainSqlWorkshopProject.Models;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using Azure.Core.GeoJson;
using System.Runtime.InteropServices;

namespace MainSqlWorkshopProject
{
    internal partial class Program
    {
      
        private const string ConnectionString = "server=localhost,1434;uid=sa;pwd=Alaska2017;TrustServerCertificate=true;database=SqlWorkshop;";
        HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Program program = new Program();
                Dictionary<string, List<object>> allData=new();
                sahi:
                try
                {
                allData = await program.GoTodoItems();

                }
                catch (Exception)
                {
                    Console.WriteLine("error again");
                     // Task.Delay(150000);
                    Thread.Sleep(70000); 
                    goto sahi;
                }

                using (SqlConnection connection = new(ConnectionString))
                {
                    foreach (var data in allData)
                    {
                        int i = 0;
                        foreach (var obj in data.Value)
                        {
                            i++;
                            List<string> dataList = new List<string>();
                            List<string> dataListValues = new List<string>();

                            foreach (var item in obj.GetType().GetProperties())
                            {
                                dataList.Add(item.Name);
                                dataListValues.Add(item.GetValue(obj).ToString());
                            }
                            string commandText = $"INSERT INTO {data.Key} ({string.Join(",", dataList)}) VALUES({string.Join(",", dataListValues.Select(v => int.TryParse(v, out int intValue) ? intValue.ToString() : $"'{v.Replace("'", string.Empty)}'"))})";
                            if (data.Key == "Users")
                            {

                                commandText = $"INSERT INTO {data.Key} ({string.Join(",", dataList)}) VALUES({string.Join(",", dataListValues.Select(v => int.TryParse(v, out int intValue) ? intValue.ToString() : $"'{v.Replace("'", string.Empty)}'"))})";
                            }
                            else if (data.Key == "UserPhone")
                            {
                                commandText = $"INSERT INTO {data.Key}(Phone,Type) values({dataListValues})";

                            }
                            else
                            {

                                commandText = $"INSERT INTO  {data.Key} (UserId,{string.Join(",", dataList)}) VALUES({i},{string.Join(",", dataListValues.Select(v => int.TryParse(v, out int intValue) ? intValue.ToString() : $"'{v.Replace("'", string.Empty)}'"))})";
                            }
                            // string commandText = $"INSERT INTO {data.Key} ({string.Join(",", dataList)}) VALUES({string.Join(",", dataListValues.Select(v => int.TryParse(v, out int intValue) ? intValue.ToString() : $"'{v}'"))})";
                            //string commandText = $"INSERT INTO {data.Key} ({string.Join(",", dataList)}) VALUES({string.Join(",", dataListValues.Select(v => int.TryParse(v, out int intValue) ? intValue.ToString() : $"'{v.Replace("'", string.Empty)}'"))})";

                            using (SqlCommand command = new(cmdText: commandText, connection: connection))
                            {
                                connection.Open();

                                if (!commandText.Contains("?"))
                                {
                                    Console.WriteLine(commandText);
                                    command.ExecuteNonQuery();
                                }
                                connection.Close();
                            }
                            SqlCommand cmd = new SqlCommand($"DBCC CHECKIDENT ('{data.Key}', RESEED, 0);", connection);
                        }
                    }
                }
                goto sahi;
            }
          
        }

        private async Task<Dictionary<string, List<object>>> GoTodoItems()
        {
            Random r = new();
            Dictionary<string, List<object>> DictionaryData = new Dictionary<string, List<object>>();
            List<object> userListData = new();
            List<object> phoneListData = new();
            List<object> loginListData = new();
            List<object> locationListData = new();
            List<object> pictureListData = new();
            List<object> emailListData = new();
         
            for (int j = 0; j < 4; j++)
            {
                string response = await client.GetStringAsync("https://randomuser.me/api?results=5000");
                var root = JsonConvert.DeserializeObject<Root>(response);

                foreach (var person in root.results)
                {
                    var user = new Users
                    {
                        Firstname = person.name.first,
                        Lastname = person.name.last,
                        BirthDate = person.dob.date,
                        Age = person.dob.age,
                        Gender = person.gender,
                        Salary = r.Next(500, 4000),
                        CreditCard = r.Next(1000, 9999).ToString() + "-" + r.Next(1000, 9999).ToString() + "-" + r.Next(1000, 9999).ToString() + "-" + r.Next(1000, 9999).ToString(),
                        Nationality = person.nat
                    };

                    
                    var location = new UserLocation
                    {
                        Street = person.location.street.name,
                        City = person.location.city,
                        State = person.location.state,
                        Postcode = person.location.postcode.ToString(),
                        Coordinates = person.location.coordinates.longitude + "," + person.location.coordinates.latitude,
                        Country = person.location.country,
                        Timezone = person.location.timezone.offset
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
                        Registerdate = person.registered.date,
                        Registered_duration = person.registered.age 
                    };

                    var phone = new UserPhone
                    {
                        Phone = person.phone,
                        Cell = person.cell
                    };

                    var picture = new UserPicture
                    {
                        Large = person.picture.large,
                        Medium = person.picture.medium,
                        Thumbnail = person.picture.thumbnail
                    };

                    var email = new UserEmails
                    {
                        Email = person.email
                    };
                    if (person.nat != "IR")
                    {

                        userListData.Add(user);
                        loginListData.Add(login);
                        phoneListData.Add(phone);
                        pictureListData.Add(picture);
                        locationListData.Add(location);
                        emailListData.Add(email);
                    }
                    
                }
            }

           DictionaryData.Add("Users", userListData);
			DictionaryData.Add("Locations", locationListData);
			DictionaryData.Add("Logins", loginListData);
			DictionaryData.Add("Pictures", pictureListData);
			DictionaryData.Add("Phones", phoneListData);
            DictionaryData.Add("Emails", emailListData);
            return DictionaryData;
        }
    }
}