﻿namespace TinyBlog;

public record Html(string Value)
{
    public Html Replace(string name, string value)
    {
        return new Html(Value.Replace($"{{{{ {name} }}}}", value));
    }
}

public static class Placeholder
{
    public const string Title = "title";
    public const string Author = "author";
    public const string Date = "date";
    public const string Content = "content";
    public const string Now = "now";
    public const string Year = "year";
    public const string CacheBuster = "cache_buster";
}

public class Layout
{
    public File File { get; private set; } = null!;
    public Html Html { get; private set; } = null!;

    public static Layout Create(File layout)
    {
        var html = System.IO.File.ReadAllText(layout.AbsolutePath);

        Guard.Against.MissingRequiredPlaceholder(html, Placeholder.Title);
        Guard.Against.MissingRequiredPlaceholder(html, Placeholder.Content);
        Guard.Against.MissingOptionalPlaceholder(html, Placeholder.Author);
        Guard.Against.MissingOptionalPlaceholder(html, Placeholder.Date);

        return new Layout
        {
            File = layout,
            Html = new Html(html)
        };
    }
}