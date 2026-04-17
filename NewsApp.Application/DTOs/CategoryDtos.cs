namespace NewsApp.Application.DTOs
{
    public record CategoryDto(Guid Id, string Name, string Slug);
    public record CreateCategoryDto(string Name, string Slug);
}