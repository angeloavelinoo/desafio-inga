using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public class ContractUsername : Contract<string>
    {
        public ContractUsername(string username = "")
        {
            Requires()
                .IsNotNullOrWhiteSpace(username, nameof(username), "Username is required");
        }
    }
}