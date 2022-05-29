using ASP_Examples.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASP_Examples.Lazy.Proxies
{
    public class CustomerProxy : Customer
    {
        public override byte[] ProfilePicture
        {
            get
            {
                if (base.ProfilePicture == null)
                {
                    base.ProfilePicture = ProfilePictureService.GetFor(Name);
                }

                return base.ProfilePicture;
            }
        }
    }
}
