using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationOfData.Data
{
    public static class KeyTransformations
    {
        public static int ResidualSplitting(int key, int numberOfBuckets)
        {
            return key % numberOfBuckets - 1;
        }
    }
}
