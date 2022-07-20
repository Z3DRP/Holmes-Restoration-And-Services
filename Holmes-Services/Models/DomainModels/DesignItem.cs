using Holmes_Services.Models.DTOs;
using System.Collections.Generic;
namespace Holmes_Services.Models.DomainModels
{
    public class DesignItem
    {
        // desing id is just a geneerated id to keep designs unique in
        // the session data. this id will not be stored in database
        public DesignDTO Design { get; set; }
        public double Price { get; set; }
    }
}
