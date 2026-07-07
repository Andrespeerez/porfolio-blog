using Application.Interfaces.Services;
using Slugify;

namespace Infrastructure.Services;

public class Slugifier : ISlugifier
{
    private readonly SlugHelper _helper = new SlugHelper();

    public string Slugify(string text)
    {
        return _helper.GenerateSlug(text);
    }
}