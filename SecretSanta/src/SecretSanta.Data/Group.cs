using Devise;
namespace SecretSanta.Data
{
    [Devise]
    [DeviseCustom("Mapping")]
    public class Group
    {
        public int Id { get; set; }
        public string? Name { get; set; } = "";
    }
}
