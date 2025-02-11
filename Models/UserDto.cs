namespace gg_test.Models
{
    public record UserDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Department { get; init; } = string.Empty;
        public Role Role { get; init; }
    }

    // Update LoginResponse to use UserDto
    public record LoginResponse(string Token, UserDto User);
}
