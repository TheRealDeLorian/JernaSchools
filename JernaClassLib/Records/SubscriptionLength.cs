using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JernaClassLib.Records;

public record SubscriptionLength(SubscriptionLengthEnum Value, string Display, string? Description = null);

public enum SubscriptionLengthEnum
{
    Weekly,
    Biweekly,
    Monthly,
}
