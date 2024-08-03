using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EBCustomerTask.Core.Entities
{
    public class Customer
    {
        [BsonId]
        [Key]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime BirthDate { get; set; }
        public string PhotoUrl { get; set; }
    }
}
