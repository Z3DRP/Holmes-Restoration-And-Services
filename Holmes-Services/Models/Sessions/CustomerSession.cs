using Holmes_Services.Models.Extensions;

namespace Holmes_Services.Models.Sessions
{
    public class CustomerSession
    {
        private const string CustomerKey = "myInfo";
        private const string CountKey = "myCount";
        private const string Firstname = "fname";
        private const string Lastname = "lname";
        private const string DeckKey = "deck";
        private const string RailKey = "rail";
        
        private ISession session { get; set; }
        public CustomerSession(ISession sesh)
        {
            this.session = sesh;
        }
        public void SetId(int id)
        {
            session.SetObject(CustomerKey, id);
            session.SetInt32(CountKey, 1);
        }
        public int? GetId() => session.GetInt32(CountKey);
        public void RemoveId()
        {
            session.Remove(CustomerKey);
            session.Remove(CountKey);
        }
        public void SetFirstname(string fname) => session.SetString(Firstname, fname);
        public string? GetFirstname() => session.GetString(Firstname);
        public void RemoveFirstname() => session.Remove(Firstname);
        public void SetLastname(string lname) => session.SetString(Lastname, lname);
        public string? GetLastname() => session.GetString(Lastname);
        public void RemoveLastname() => session.Remove(Lastname);
        public void SetDeckId(int id) => session.SetInt32(DeckKey, id);
        public int? GetDeckId() => session.GetInt32(DeckKey);
        public void RemoveDeck() => session.Remove(DeckKey);
        public void SetRailId(int id) => session.SetInt32(RailKey, id);
        public int? GetRail() => session.GetInt32(RailKey);
        public void RemoveRail() => session.Remove(RailKey);
        
    }
}
