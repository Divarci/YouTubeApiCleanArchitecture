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
        Guid.Parse("ff52dfa7-d39c-45cc-aaa3-c74c23b06bc9").ToString(),
        "User");

    [AutoSeedData]
    public static AppRole Admin => new AppRole(
        Guid.Parse("B61F0859-CF2E-47F9-8B94-86CBEE824344"),
        Guid.Parse("e6995c25-d137-4e11-9e86-de56b2524f90").ToString(),
        "Admin");
}