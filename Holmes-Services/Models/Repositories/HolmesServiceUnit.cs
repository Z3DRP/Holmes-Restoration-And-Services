using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.QueryOptions;

namespace Holmes_Services.Models.Repositories
{
    public class HolmesServiceUnit : IHolmesServiceUnit
    {
        private HolmesContext _contxt { get; set; }
        public HolmesServiceUnit(HolmesContext ctx) => _contxt = ctx;

        private Repo<Customer> customerData;
        public Repo<Customer> Customers
        {
            get
            {
                if (customerData == null)
                    customerData = new Repo<Customer>(_contxt);
                return customerData;
            }
        }

        private Repo<Decking> deckData;
        public Repo<Decking> Decks
        {
            get
            {
                if (deckData == null)
                    deckData = new Repo<Decking>(_contxt);
                return deckData;
            }
        }

        private Repo<Railing> railData;
        public Repo<Railing> Rails
        {
            get
            {
                if (railData == null)
                    railData = new Repo<Railing>(_contxt);
                return railData;
            }
        }

        private Repo<Rail_Type> railTypeData;
        public Repo<Rail_Type> RailTypes
        {
            get
            {
                if (railTypeData == null)
                    railTypeData = new Repo<Rail_Type>(_contxt);
                return railTypeData;
            }
        }

        private Repo<Deck_Type> deckTypeData;
        public Repo<Deck_Type> DeckTypes
        {
            get
            {
                if (deckTypeData == null)
                    deckTypeData = new Repo<Deck_Type>(_contxt);
                return deckTypeData;
            }
        }
        private Repo<Pattern> patternData;
        public Repo<Pattern> Patterns
        {
            get
            {
                if (patternData == null)
                    patternData = new Repo<Pattern>(_contxt);
                return patternData;
            }
        }
        private Repo<Price_Groups> priceGroups;
        public Repo<Price_Groups> Groups
        {
            get
            {
                if (priceGroups == null)
                    priceGroups = new Repo<Price_Groups>(_contxt);
                return priceGroups;
            }
        }

        private Repo<Design> designData;
        public Repo<Design> Designs
        {
            get
            {
                if (designData == null)
                    designData = new Repo<Design>(_contxt);
                return designData;
            }
        }

        private Repo<Job> jobData;
        public Repo<Job> Jobs
        {
            get
            {
                if (jobData == null)
                    jobData = new Repo<Job>(_contxt);
                return jobData;
            }
        }

        public void DeleteCustomerDesigns(Customer customer)
        {
            var customerDesigns = Designs.List(new QueryOptions<Design>
            {
                Where = ci => ci.Customer_Id == customer.Id
            });

            foreach(Design customerDesign in customerDesigns)
                Designs.Delete(customerDesign);
        }
        
        public void AddNewDesign(Design design)
        {
            Designs.Insert(design);
            
        }

        public void Save() => _contxt.SaveChanges();

    }
}
