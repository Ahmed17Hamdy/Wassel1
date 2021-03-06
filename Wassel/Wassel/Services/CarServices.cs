﻿
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Wassel.Model;
using Wassel.Models;

namespace Wassel.Services
{
   public class CarServices
    {
        public async Task<ObservableCollection<Car>> GetAllCars()
        {
            var client = new HttpClient();
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    var response = await client.GetAsync("https://waselksa.alsalil.net/api/settingmember");
                    var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
                    var Req = JsonConvert.DeserializeObject<Response<string, ObservableCollection<Car>>>(serverResponse);
                    var Cars = Req.message;
                    return Cars;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return  null;
            }
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

