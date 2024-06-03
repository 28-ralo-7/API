using API.Domain.shared;

namespace API.Domain.adminPanel;

public class PracticeScheduleSettingsOptions
{
    public Item[] PracticeOptions { get; set; }
    public Item[] PracticeLeadOptions { get; set; }
    public Item[] GroupOptions { get; set; }

    public PracticeScheduleSettingsOptions(Item[] practiceOptions, Item[] practiceLeadOptions, Item[] groupOptions)
    {
        PracticeOptions = practiceOptions;
        PracticeLeadOptions = practiceLeadOptions;
        GroupOptions = groupOptions;
    }
}