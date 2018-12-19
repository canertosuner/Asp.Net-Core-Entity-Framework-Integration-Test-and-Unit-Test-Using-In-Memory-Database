using System;

namespace Customer.Contract
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string CityCode { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
