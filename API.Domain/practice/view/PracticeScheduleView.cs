using System.Runtime.InteropServices.JavaScript;
using API.Domain.practice.domain;
using API.Domain.shared;

namespace API.Domain.practice.view;

public class PracticeScheduleView
{
    public string? Id { get; set; }
    public Item? Practice { get; set; }
    public Item Group { get; set; }
    public Item PracticeLead { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }

    public PracticeScheduleView(string? id, Item? name, Item group, Item practiceLead, DateTime dateStart, DateTime dateEnd)
    {
        Id = id;
        Practice = name;
        Group = group;
        PracticeLead = practiceLead;
        DateStart = dateStart;
        DateEnd = dateEnd;
    }

    public PracticeScheduleView(PracticeScheduleDomain domain)
    {
        Id = domain.Id.ToString();
        Practice = domain.Practice;
        Group = domain.Group;
        PracticeLead = domain.PracticeLead;
        DateStart = domain.DateStart.ToDateTime(new TimeOnly());
        DateEnd = domain.DateEnd.ToDateTime(new TimeOnly());
    }
    
}