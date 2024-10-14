using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JernaClassLib.Enums;
public record LearningEnvironment(LearningEnvironmentEnum Value, string Display, string? Description = null);

public enum LearningEnvironmentEnum
{
    Structured,
    Open,
    Other,
}
