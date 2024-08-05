namespace StudentManagement.Models.Entity
{
    public class Staff
    {
        public Guid staffId { get; set; }
        public string staffName { get; set;  }
        public string address { get; set; }
        public DateOnly birthDay { get; set; }
        public string Department { get; set; }

        public Account Account { get; set; }

        // Foreign key for Account
        public int AccountId { get; set; }
    }
}
