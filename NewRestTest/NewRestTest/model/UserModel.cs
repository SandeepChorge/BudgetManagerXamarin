using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewRestTest.model
{
    [Table("users")]
    public class UserModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(250), Unique]

        public string Name { get; set; }

        public int Gender { get; set; }

        [MaxLength(250), Unique]
        public string Email { get; set; }

        [MaxLength(50), Unique]
        public string MobileNumber { get; set; }

        public string Password { get; set; }
    }
}
