using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InputScrubbingModelBinder.Web.Example
{
    public class AddCustomer : IUserAware
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Guid UserId { get; set; }
    }

    public interface IUserAware
    {
        Guid UserId { get; set; }
    }
}
