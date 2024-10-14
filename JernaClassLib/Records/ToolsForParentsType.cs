namespace JernaClassLib.Records;

public record ToolsForParentsType(ToolsForParentsTypeEnum Value, string Display, string? Description = null);

public enum ToolsForParentsTypeEnum
{
    Plant,
    Animal,
    Dinosaur
}
