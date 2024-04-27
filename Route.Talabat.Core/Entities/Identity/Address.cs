using System.Text.Json.Serialization;

namespace Route.Talabat.Core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;

        public string ApplicationUserId { get; set; } = null!;//Fk

        //1-return at dto
        //2-install package [netwonsoft.Json], dont forgrt to add it at program.cs
        //3- [JsonIgnore] to not load it and make infinity loop 
        [JsonIgnore]
        public ApplicationUser ApplicationUser { get; set; } = null!;//Navigational Property [one]
    }
}