using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Modals.Configurations
{
	public class Appsettings
	{
        public AuthorizationSettings? AuthorizationSettings { get; set; }
		public Permissions? Permissions { get; set; }
	}
	public class Permissions
	{
        public string? CreatePolicyLocalApi { get; set; }
    }
	public class AuthorizationSettings
	{
		public string? GatewayUrl { get; set; }
		public string? AuthServerUrl { get; set; }
	}
}
