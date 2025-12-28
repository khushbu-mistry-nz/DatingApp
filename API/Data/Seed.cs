using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.DTOs;
using API.Entities;
using DatingApp.Data;
using DatingApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(AppDbContext context)
    {
        if (await context.Users.AnyAsync()) return;

        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var users = JsonSerializer.Deserialize<List<SeedUserDto>>(userData, options);

        if (users == null)
        {
            Console.WriteLine("No members in seed data");
            return;
        }

        foreach (var user in users)
        {
            var hmac = new HMACSHA512();
            var newUser = new AppUser
            {
                Id = user.Id,
                Email = user.Email,
                ImageUrl = user.ImageUrl,
                DisplayName = user.DisplayName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd")),
                PasswordSalt = hmac.Key,
                Member = new Member
                {
                    Id = user.Id,
                    DisplayName = user.DisplayName,
                    DateOfBirth = user.DateOfBirth,
                    Description = user.Description,
                    ImageUrl = user.ImageUrl,
                    Gender = user.Gender,
                    City = user.City,
                    Country = user.Country,
                    Created = user.Created,
                    LastActive = user.LastActive
                }
            };

            newUser.Member.Photos.Add(new Photo
            {
                Url = user.ImageUrl!,
                MemberId = user.Id
            });

            context.Users.Add(newUser);
        }

        await context.SaveChangesAsync();
    }
}