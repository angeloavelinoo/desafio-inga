using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public class ContractGuid : Contract<Guid>
    {
        public ContractGuid(Guid id)
        {
            Requires()
                .IsNotEmpty(id, nameof(id), "Id is required");
        }
    }
}
