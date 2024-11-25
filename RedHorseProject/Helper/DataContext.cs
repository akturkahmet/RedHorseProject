using EntityLayer.Concrete;
using RedHorseProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RedHorseProject.Helper
{
    public class DataContext:DbContext
    {
        public DataContext() : base("RedHorseContext") 
        {
        }



    }
}