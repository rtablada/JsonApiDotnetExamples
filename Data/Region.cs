using System.ComponentModel.DataAnnotations.Schema;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace JsonApiExample.Data;

[Resource]
public class Region  : Identifiable<Guid>
{
    [Attr] public string Name { get; set; }
    [HasMany] public IList<Country> Countries { get; set; } = new List<Country>();
}
