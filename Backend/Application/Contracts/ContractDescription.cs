using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public class ContractDescription : Contract<string>
    {
        public ContractDescription(string description = "")
        {
            Requires()
                .IsNotNullOrWhiteSpace(description, nameof(description), "Description is required");
        }
    }
}
