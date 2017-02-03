using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactsList.Admin.Models
{
    public class AddCompanyViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Укажите название компании")]
        public string Name { get; set; }
        public List<ActivityViewModel> Activities { get; set; }       
        public List<TownViewModel> InTowns { get; set; }     
        public int ActivityID { get; set; }
    }
    public class CountryViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    [Serializable]
    public class TownViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class EditDayViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

    }
    public class DayViewModel
    {
        public bool Checked { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
    }
    public class ActivityViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
  
    public class AddCountryViewModel
    { 
        public string Name { get; set; }
    }
    public class AddTownViewModel
    { 
        public string Name { get; set; }
    }
}
