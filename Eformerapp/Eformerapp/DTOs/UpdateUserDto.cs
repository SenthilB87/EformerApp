﻿namespace Eformerapp.DTOs
{
    public class UpdateUserDto
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public int UserRoleId { get; set; }
    }
}
