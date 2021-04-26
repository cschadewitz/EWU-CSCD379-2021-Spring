namespace SecretSanta.Data
{
    public class User : IData
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; } = "";
        public virtual string LastName { get; set; } = "";
    }
}
