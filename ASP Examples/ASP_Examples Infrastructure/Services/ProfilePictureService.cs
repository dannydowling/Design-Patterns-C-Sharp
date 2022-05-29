using System;
using System.Collections.Generic;
using System.Text;

namespace ASP_Examples
{
    public class ProfilePictureService
    {
        // Should be async when doing an API call
        public static byte[] GetFor(string lookup)
        {
            // Fetch from some 3rd party API based on the e-mail or name
            return new byte[] { 0};
        }
    }
}
