namespace Application.Interfaces.Services;

public interface ISlugifier
{
    string Slugify(string text);
}