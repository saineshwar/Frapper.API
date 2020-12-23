using Frapper.ViewModel.Customers.Request;

namespace Frapper.Repository.Customers.Command
{
    public interface ICustomersCommand
    {
        void Add(CustomersRequest customersViewModel, long userId);
    }
}
