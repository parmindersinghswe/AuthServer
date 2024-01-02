namespace Auth.Domain.Modals.Auth
{
	public class ValidateModal
	{
		public required string Token { get; set; }
		public required string ClaimType { get; set; }
	}
}
