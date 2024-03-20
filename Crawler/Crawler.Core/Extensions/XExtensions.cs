using System.Xml.Linq;

namespace Crawler.Core.Extensions;

public static class XExtensions
{
    public static XAttribute GetRequiredAttribute(this XElement root, XName name)
    {
        ArgumentNullException.ThrowIfNull(root, nameof(root));
        ArgumentNullException.ThrowIfNull(name, nameof(name));

        return root.Attribute(name) ?? throw new InvalidOperationException($"There is no attribute with {name} name.");
    }

    public static XElement GetRequiredElement(this XElement root, XName name)
    {
        ArgumentNullException.ThrowIfNull(root, nameof(root));
        ArgumentNullException.ThrowIfNull(name, nameof(name));

        return root.Element(name) ?? throw new InvalidOperationException($"There is no element with {name} name.");
    }
}
