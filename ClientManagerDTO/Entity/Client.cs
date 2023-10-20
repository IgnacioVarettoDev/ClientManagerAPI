using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ClientManagerDTO.Entity
{
    public class Client
    {
        public string ClientId { get; set; } = string.Empty;
        public string Rut { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool Married { get; set; } = false;
        public DateTime? DateOfBirth { get; set; } = DateTime.MinValue;
        public int Age { get; set; } = 0;
        public string Address { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
