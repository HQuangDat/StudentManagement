﻿namespace StudentManagement.Models.Entity
{
    public class Account
    {   
        public int Id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public bool gender { get; set; }
    }
}
