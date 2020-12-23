using Frapper.Repository.Customers.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frapper.Repository
{
    public interface IUnitOfWorkDapper
    {
        ICustomersCommand CustomersCommand { get; }
        bool Commit();
    }
}
