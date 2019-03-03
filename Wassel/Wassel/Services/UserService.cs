using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wassel.Model;
using Wassel.Models;


namespace Wassel.Services
{
    public class UserService
    {
        public async Task<string> LoginCommandAsync(string name, string email, string password, string confirmpass, string firebase_token, string device_id)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                using (var client = new HttpClient())
                {
                    Dictionary<string, string> values = new Dictionary<string, string>();

                    values.Add("name", name);
                    values.Add("email", email);
                    values.Add("password", password);
                    values.Add("confirmpass", confirmpass);
                    values.Add("firebase_token", firebase_token);
                    values.Add("device_id", device_id);
                    string content = JsonConvert.SerializeObject(values);
                    try
                    {
                        var response = await client.PostAsync("https://waselksa.alsalil.net/api/login", new StringContent(content, Encoding.UTF8, "text/json"));
                        var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
                        return serverResponse;
                    }
                    catch (Exception)
                    {
                        return "false";
                    }
                }
            }
            else return "false";
        }

        public async Task<string> RegisterAsync(User userReg)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                using (var client = new HttpClient())
                {
                    Dictionary<string, string> values = new Dictionary<string, string>();
                    values.Add("name", userReg.name);
                    values.Add("email", userReg.email);
                    values.Add("password", userReg.password);
                    values.Add("confirmpass", userReg.confirmpass);
                    values.Add("lat", userReg.lat);
                    values.Add("lng", userReg.lng);
                    values.Add("firebase_token", userReg.firebase_token);
                    values.Add("device_id", userReg.device_id);
                    string content = JsonConvert.SerializeObject(values);
                    try
                    {
                        var response = await client.PostAsync("https://waselksa.alsalil.net/api/userregister", new StringContent(content, Encoding.UTF8, "text/json"));
                        var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
                        return serverResponse;
                    }
                    catch (Exception)
                    {
                        return "false";
                    }
                }
            }
            else return "false";
        }
        public async Task<ObservableCollection<Cartype>> GetCarstype()
        {
            var client = new HttpClient();
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    var response = await client.GetAsync("https://waselksa.alsalil.net/api/settingindex");
                    var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
                    var Req = JsonConvert.DeserializeObject<Response<string, MainResponseMessage>>(serverResponse);
                    var Carstype = Req.message.cartype;
                 
                    return Carstype;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else return null;
        }

    }
}
