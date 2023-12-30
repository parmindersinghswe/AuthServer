namespace Auth.Server.Data.Interfaces
{
    public interface IEntity<KeyType>
    {
        KeyType Id { get; set; }
    }
}
