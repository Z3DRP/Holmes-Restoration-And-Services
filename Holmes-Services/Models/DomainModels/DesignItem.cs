namespace Holmes_Services.Models.DomainModels
{
    public class DesignItem
    {
        // desing id is just a geneerated id to keep designs unique in
        // the session data. this id will not be stored in database
        public int DesignId { get; set; }
        public Decking Deck { get; set; }
        public Railing Rail { get; set; }
        public double Price { get; set; }
    }
}
