using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BackendApp.Models
{
    public class Location
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class Origin
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public string Species { get; set; }
        public string Type { get; set; }
        public Gender Gender { get; set; }
        public Origin Origin { get; set; }
        public Location Location { get; set; }
        public string Image { get; set; }
        public List<string> Episode { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }
    }

    public class CharactersCollection
    {
        public List<Character> CharactersList { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        [EnumMember(Value ="Alive")]
        Alive,
        [EnumMember(Value = "Dead")]
        Dead,
        [EnumMember(Value = "Unknown")]
        Unknown
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        [EnumMember(Value = "Male")]
        Male,
        [EnumMember(Value = "Female")]
        Female,
        [EnumMember(Value = "Genderless")]
        Genderless,
        [EnumMember(Value = "Unknown")]
        Unknown
    }
}
