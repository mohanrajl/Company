﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Web.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public bool Active { get; set; }
    }
}