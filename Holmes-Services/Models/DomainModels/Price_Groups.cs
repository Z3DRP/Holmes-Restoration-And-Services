using System.ComponentModel.DataAnnotations;

namespace Holmes_Services.Models.DomainModels
{
    public class Price_Groups
    {
        public int Id { get; set; }

        public int GroupId { get; set; }

        [MaxLength(2, ErrorMessage = "Group name can only be 2 characters or less")]
        public string Group_Name { get; set; }

        // for now decks or rails can be empty
        public ICollection<Decking> Decks { get; set; }
        public ICollection<Railing> Rails { get; set; }
    }
}
