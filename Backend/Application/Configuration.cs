using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class Configuration
    {
        public static string SECRET { get; set; } = Environment.GetEnvironmentVariable("SECRETJWT") ?? "40ceff0fb8965706f8a0667c12156771";
    }
}
