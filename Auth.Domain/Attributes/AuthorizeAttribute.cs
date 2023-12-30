namespace Auth.Domain.Attributes
{
	public class AuthorizeAttribute : Attribute
	{
		public string Policy { get; }
		public AuthorizeAttribute(string policy)
		{
			Policy = policy;
		}
	}
}
