using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Wassel.Models;

namespace Wassel.Model
{
    public class Response<T, I>
    {
        public bool success { get; set; }
        public T data { get; set; }
        public I message { get; set; }
    }

    public class MainResponseMessage
    {
        public ObservableCollection<Settinginfo> settings { get; set; }
        public ObservableCollection<Car> cars { get; set; }     
        public ObservableCollection<Cartype> cartype { get; set; }
    }
}
