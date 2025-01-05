using Microsoft.AspNetCore.Identity;
using YouTubeApiCleanArchitecture.Domain.Abstraction.Entity;
using YouTubeApiCleanArchitecture.Domain.Attributes;

namespace YouTubeApiCleanArchitecture.Domain.Entities.Identity.Roles;
public class AppRole : IdentityRole<Guid>, IHaveAutoseedData
{
    public const string ADMIN_KEY = "123456";

    private AppRole(
        Guid id,
        string concurrencyStamp,
        string name)
    {
        Id = id;
        ConcurrencyStamp = concurrencyStamp;
        Name = name;
        NormalizedName = name.ToUpper();
    }

    public AppRole() { }

    [AutoSeedData]
    public static AppRole User => new AppRole(
        Guid.Parse("0E8C2030-7E8C-4436-96A2-92DEE907BE1D"),
        Guid.NewGuid().ToString(),
        "User");

    [AutoSeedData]
    public static AppRole Admin => new AppRole(
        Guid.Parse("B61F0859-CF2E-47F9-8B94-86CBEE824344"),
        Guid.NewGuid().ToString(),
        "Admin");
}