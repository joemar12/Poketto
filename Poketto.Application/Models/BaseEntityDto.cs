namespace Poketto.Application.Models
{
    public abstract record BaseEntityDto
    {
        public Guid Id { get; set; }
    }
}
