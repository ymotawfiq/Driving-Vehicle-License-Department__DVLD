using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Access_Tier
{
    internal class clsDataAccessTierSettings
    {
        public static string ConnectionString { get; } = @"Server=.;Database=DVLD;User Id=sa; Password=123456";
    }
}
