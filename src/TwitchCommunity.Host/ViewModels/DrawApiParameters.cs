using System.Collections.Generic;

namespace TwitchCommunity.Host.Controllers
{
    public class DrawApiParameters
    {
        public List<int> Selected { get; set; }

        public List<int> ActiveSelected { get; set; }
    }
}