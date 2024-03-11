using BookingApp.Serializer;
using System;

namespace BookingApp.Model
{
    public enum Roles { OWNER, GUEST, GUIDE, TOURIST }
    public class User : ISerializable
    {
        public Roles Role {  get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }



        public User() { }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Role.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Role = (Roles)Enum.Parse(typeof(Roles), values[3]);
        }
    }
}
