using System;

namespace API.DTOs;

public class UserDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string DisplayName { get; set; }
    public string? imageUrl { get; set; }
    public string token { get; set; }



}
