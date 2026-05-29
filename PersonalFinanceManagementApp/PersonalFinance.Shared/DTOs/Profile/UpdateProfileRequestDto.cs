namespace PersonalFinance.Shared.DTOs.Profile;

public class UpdateProfileRequestDto
{
    public string Name { get; set; }

    public string SurName { get; set; }

    public string Email { get; set; }

    public string? AvatarUrl { get; set; }
}