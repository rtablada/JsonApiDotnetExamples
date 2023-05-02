using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace JsonApiExample.Data;

[Resource]
public class Country : Identifiable<Guid>
{
    [Attr] public string Name { get; set; }

    [Attr] public Population Population { get; set; }

    public Guid RegionId { get; set; }
    [HasOne] public Region Region { get; set; }
}
