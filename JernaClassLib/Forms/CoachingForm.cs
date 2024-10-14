using JernaClassLib.Enums;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace JernaClassLib.Forms;

public class CoachingForm
{
    [Required(ErrorMessage = "Please provide contact information so Jerna can reach our if needed")]
    public string? ContactInfo { get; set; }
    public List<int> ChildrensAge { get; set; } = [];
    public bool WantsDeploma { get; set; } = false;
    public bool WantsTranscript { get; set; } = false;
    public string LearningEnvironment { get; set; } = Constants.Other.Display;
    [Required(ErrorMessage = "Please provide reasons for the coaching so we can give you the best session we can")]
    public string? ReasonForCoaching { get; set; }
}
