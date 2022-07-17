using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.Extensions;

namespace Holmes_Services.Models.Sessions
{
    public class NewCustomerSession
    {
        private const string CustomerKey = "customer";
        private const string DeckKey = "deck";
        private const string RailKey = "rail";
        private const string PatternKey = "pattern";
        private const string EstiamteKey = "estimate";

        private ISession session { get; set; }
        public NewCustomerSession(ISession session) => this.session = session;
        public void SetCustomer(Customer customer) => session.SetObject(CustomerKey, customer);
        public Customer GetCustomer() => session.GetObject<Customer>(CustomerKey);
        public void RemoveCustomer() => session.Remove(CustomerKey);
        public void SetDeck(Decking deck) => session.SetObject(DeckKey, deck);
        public Decking GetDeck() => session.GetObject<Decking>(DeckKey);
        public void RemoveDeck() => session.Remove(DeckKey);
        public void SetRail(Railing rail) => session.SetObject(RailKey, rail);
        public Railing GetRail() => session.GetObject<Railing>(RailKey);
        public void RemoveRail() => session.Remove(RailKey);
        public void SetPattern(Pattern pattern) => session.SetObject(PatternKey, pattern);
        public Pattern GetPattern() => session.GetObject<Pattern>(PatternKey);
        public void RemovePattern() => session.Remove(PatternKey);
        public void SetEstimate(double estimate) => session.SetObject(EstiamteKey, estimate);
        public double GetEstimate() => session.GetObject<double>(EstiamteKey);
        public void RemoveEstimate() => session.Remove(EstiamteKey);
    }
}
