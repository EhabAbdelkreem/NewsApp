namespace NewsApp.Application.DTOs
{
    public record LoginDto(string Email, string Password);
    public record RegisterDto(string FullName, string Email, string Password);
    public record AuthResponseDto(string Token, string FullName, string Role);
}