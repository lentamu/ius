using Application.Common.Interfaces;
using Domain.Abouts;
using Domain.Roles;
using Domain.ShortUrls;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContextInitialiser(
    ApplicationDbContext context,
    IPasswordHasher passwordHasher,
    ISlugGenerator slugGenerator)
{
    public async Task InitializeAsync()
    {
        await context.Database.MigrateAsync();
        await SeedAsync();
    }

    private async Task SeedAsync()
    {
        var adminRole = Role.New(new RoleId(Guid.NewGuid()), "Admin");
        var userRole = Role.New(new RoleId(Guid.NewGuid()), "User");

        var adminUser = User.New(UserId.New(), "admin", passwordHasher.Hash("admin"), adminRole.Id);
        var regularUser = User.New(UserId.New(), "user", passwordHasher.Hash("user"), userRole.Id);

        if (!context.Roles.Any())
        {
            await context.Roles.AddRangeAsync(adminRole, userRole);
            await context.SaveChangesAsync();
        }

        if (!context.Users.Any())
        {
            await context.Users.AddRangeAsync(adminUser, regularUser);
            await context.SaveChangesAsync();
        }

        if (!context.ShortUrls.Any())
        {
            var google = "https://google.com/";
            var googleSlug = await slugGenerator.GenerateUniqueSlug(google, CancellationToken.None);

            var github = "https://github.com/";
            var githubSlug = await slugGenerator.GenerateUniqueSlug(github, CancellationToken.None);

            var adminShortUrl = ShortUrl.New(ShortUrlId.New(), google, googleSlug, adminUser.Id);
            var userShortUrl = ShortUrl.New(ShortUrlId.New(), github, githubSlug, adminUser.Id);

            await context.ShortUrls.AddRangeAsync(adminShortUrl, userShortUrl);
            await context.SaveChangesAsync();
        }

        if (!context.Abouts.Any())
        {
            var about = About.New(
                new AboutId(Guid.NewGuid()),
                "Lorem Ipsum is simply dummy text of the printing and typesetting industry.");

            await context.Abouts.AddAsync(about);
            await context.SaveChangesAsync();
        }
    }
}