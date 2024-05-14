using API.Domain.practice.domain;
using API.Domain.shared;

namespace API.Domain.practice.view;

public class PracticeLogView
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Period { get; set; }
    public Item Group { get; set; }
    public PracticeLogItemView[]? LogItems { get; set; }

    public PracticeLogView(PracticeLogDomain domain)
    {
        Id = domain.Id;
        Name = domain.Name;
        Period = domain.Period;
        Group = new Item(
            domain.Group?.ToString() ?? "",
            domain.Group?.Name ?? ""
        );
        LogItems = domain.LogItems?.Select(item => new PracticeLogItemView(item)).ToArray();
    }
}