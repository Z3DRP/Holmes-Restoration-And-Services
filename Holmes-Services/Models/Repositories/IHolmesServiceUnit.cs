using Holmes_Services.Models.DomainModels;

namespace Holmes_Services.Models.Repositories
{
    public interface IHolmesServiceUnit
    {
        Repo<Customer> Customers { get;}
        Repo<Decking> Decks { get; }
        Repo<Railing> Rails { get;}
        Repo<Rail_Type> RailTypes { get; }
        Repo<Deck_Type> DeckTypes { get;  }
        Repo<Pattern> Patterns { get; }
        Repo<Price_Groups> Groups { get; }
        Repo<Design> Designs { get; }
        Repo<Job> Jobs { get; }
        Repo<Idea> Ideas { get; }
        Repo<CompletedJob> CompletedJobs { get; }
    }
}
