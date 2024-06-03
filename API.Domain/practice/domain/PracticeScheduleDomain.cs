using API.Database;
using API.Domain.group;
using API.Domain.shared;
using API.Domain.user;

namespace API.Domain.practice.domain;

public class PracticeScheduleDomain
{
    public Guid Id { get; set; }
    public Item Practice { get; set; }
    public Item Group { get; set; }
    public Item PracticeLead { get; set; }
    public DateOnly DateStart { get; set; }
    public DateOnly DateEnd { get; set; }
    public bool IsRemoved { get; set; }

    public PracticeScheduleDomain(Practiceschedule ps, Practice practice, GroupDomain group, UserDomain user)
    {
        Id = ps.Id;
        Practice = new Item(practice.Id.ToString(), practice.Name);
        Group = new Item(group.Id.ToString(), group.Name);
        PracticeLead = new Item(user.Id.ToString(), user.Surname + " "
                                                        + user.Name[0] + "."
                                                        + (user.Patronomic?.Length > 0 ? user.Patronomic[0] + "." 
                                                                                        : ""));
        DateStart = ps.Datestart;
        DateEnd = ps.Dateend;
        IsRemoved = ps.Isremoved;
    }

    public static List<PracticeScheduleDomain> ConvertFromPracticeScheduleList(Practiceschedule[] pss,
        Practice[] practices, GroupDomain[] groups, UserDomain[] users)
    {
        List<PracticeScheduleDomain> practiceScheduleDomains = new List<PracticeScheduleDomain>();

        for (int i = 0; i < pss.Length; i++)
        {
            Practice practice = practices.First(x => x.Id == pss[0].Practiceid);
            GroupDomain group = groups.First(x => x.Id == pss[0].Groupid);
            UserDomain user = users.First(x => x.Id == pss[0].Practiceleadid);
            
            practiceScheduleDomains.Add(new PracticeScheduleDomain(pss[0], practice, group, user));
        }

        return practiceScheduleDomains;
    }
}