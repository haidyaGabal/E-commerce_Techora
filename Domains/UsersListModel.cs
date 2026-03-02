namespace Techora.Models
{
    public class UsersListModel
    {
        public List<Customer> lstCustomer { get; set; }
        public List<Admin> lstAdmin { get; set; }
        public UsersListModel()
        {
            lstCustomer = new List<Customer>();
            lstAdmin = new List<Admin>();
        }
    }
}
