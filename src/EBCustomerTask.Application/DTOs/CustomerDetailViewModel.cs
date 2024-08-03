﻿namespace EBCustomerTask.Application.DTOs
{
    public class CustomerDetailViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhotoUrl { get; set; }
    }
}
