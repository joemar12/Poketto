namespace Poketto.Application.Common
{
    public abstract record BaseAuditableEntityDto : BaseEntityDto
    {
        public DateTime Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}