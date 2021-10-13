using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchCommunity.Connector.Configuration
{
    public sealed class TwitchConnectorConfiguration
    {
        public string UserName { get; set; }

        public string AccessToken { get; set; }

        public string Channel { get; set; }
    }
}
