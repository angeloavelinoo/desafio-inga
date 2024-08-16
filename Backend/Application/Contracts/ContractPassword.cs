using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public class ContractPassword : Contract<string>
    {
        public ContractPassword(string password = "")
        {
            Requires()
                .IsNotNullOrWhiteSpace(password, nameof(password), "Password is required");
        }
    }
}