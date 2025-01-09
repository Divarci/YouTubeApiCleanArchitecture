using Microsoft.AspNetCore.Identity;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Roles;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users.DTOs;

namespace YouTubeApiCleanArchitecture.Domain.UnitTests.Identity.Users;
public class AppUserData
{
    public static readonly string Existing_User_Fullname = "Michael Jordon";
    public static readonly string Existing_User_Email = "michael@gmail.com";

    public static readonly string New_User_Fullname = "John Wick";
    public static readonly string New_User_Email = "john@gmail.com";

    public static readonly string Existing_Admin_Fullname = "Jackie Chan";
    public static readonly string Existing_Admin_Email = "jackie@gmail.com";

    public static readonly string New_Admin_Fullname = "Tony Stark";
    public static readonly string New_Admin_Email = "tony@gmail.com";

    public static async Task<AppUser> CreateExistingValidUserAsync(
        UserManager<AppUser> appUser)
    {
        var user = await AppUser.Create(
            new RegisterUserDto
            {
                Fullname = Existing_User_Fullname,
                Email = Existing_User_Email,
                Password = "Password12**",
                ConfirmPassword = "Password12**"
            },
            appUser);

        return user;
    }

    public static async Task<AppUser> CreateExistingValidAdminAsync(
        UserManager<AppUser> appUser)
    {
        var admin = await AppUser.Create(
            new RegisterUserDto
            {
                Fullname = Existing_Admin_Fullname,
                Email = Existing_Admin_Email,
                Password = "Password12**",
                ConfirmPassword = "Password12**",
                AdminKey = AppRole.ADMIN_KEY
            },
            appUser);

        return admin;
    }

    public static async Task<AppUser> CreateNewValidUserAsync(
       UserManager<AppUser> appUser)
    {
        var user = await AppUser.Create(
            new RegisterUserDto
            {
                Fullname = New_User_Fullname,
                Email = New_User_Email,
                Password = "Password12**",
                ConfirmPassword = "Password12**"
            },
            appUser);

        return user;
    }

    public static async Task<AppUser> CreateNewValidAdminAsync(
        UserManager<AppUser> appUser)
    {
        var admin = await AppUser.Create(
            new RegisterUserDto
            {
                Fullname = New_Admin_Fullname,
                Email = New_Admin_Email,
                Password = "Password12**",
                ConfirmPassword = "Password12**",
                AdminKey = AppRole.ADMIN_KEY
            },
            appUser);

        return admin;
    }
}
