using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public class ContractName : Contract<string>
    {
        public ContractName(string username = "")
        {
            Requires()
                .IsNotNullOrWhiteSpace(username, nameof(username), "Name is required");
        }
    }
}
