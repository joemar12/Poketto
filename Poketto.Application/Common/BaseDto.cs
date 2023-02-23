namespace Poketto.Application.Common
{
    public abstract record BaseDto
    {
        public Guid Id { get; set; }
    }
}