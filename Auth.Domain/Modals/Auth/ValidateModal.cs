using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Modals.Auth
{
	public class ValidateModal
	{
		public required string Token { get; set; }
		public required string ClaimType { get; set; }
	}
}
