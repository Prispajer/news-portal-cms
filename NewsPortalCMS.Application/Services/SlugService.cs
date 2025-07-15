using System.Text;
using System.Text.RegularExpressions;
using NewsPortalCMS.Application.Interfaces;

public class SlugService : ISlugService
{
    private readonly IArticleRepository _articleRepository;

    public SlugService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<string> GenerateUniqueSlugAsync(string baseText)
    {
        string ReplacePolishChars(string input)
        {
            var replacements = new Dictionary<char, char>
            {
                ['ą'] = 'a',
                ['ć'] = 'c',
                ['ę'] = 'e',
                ['ł'] = 'l',
                ['ń'] = 'n',
                ['ó'] = 'o',
                ['ś'] = 's',
                ['ź'] = 'z',
                ['ż'] = 'z',
                ['Ą'] = 'A',
                ['Ć'] = 'C',
                ['Ę'] = 'E',
                ['Ł'] = 'L',
                ['Ń'] = 'N',
                ['Ó'] = 'O',
                ['Ś'] = 'S',
                ['Ź'] = 'Z',
                ['Ż'] = 'Z'
            };

            var sb = new StringBuilder(input.Length);
            foreach (var c in input)
                sb.Append(replacements.TryGetValue(c, out var rep) ? rep : c);
            return sb.ToString();
        }

        var text = ReplacePolishChars(baseText);
        var slugBase = Regex.Replace(text.ToLowerInvariant(), @"[^a-z0-9\s-]", "");
        slugBase = Regex.Replace(slugBase, @"\s+", "-").Trim('-');

        var slug = slugBase;
        var index = 1;

        while (await _articleRepository.GetBySlugAsync(slug) != null)
        {
            slug = $"{slugBase}-{index++}";
        }

        return slug;
    }
}
