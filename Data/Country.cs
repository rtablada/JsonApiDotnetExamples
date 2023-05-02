using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace JsonApiExample.Data;

[Resource]
public class Country : Identifiable<int>
{
    [Attr] public string Name { get; set; }

    [Attr] public Statistics Statistics { get; set; }

    public int RegionId { get; set; }
    [HasOne] public Region Region { get; set; }
}
