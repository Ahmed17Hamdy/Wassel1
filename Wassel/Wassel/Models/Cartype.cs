﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Wassel.Models
{
  public  class Cartype
    {
        public int id { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public int parent { get; set; }
        public List<Carmodal> carmodals { get; set; }
    }
}
