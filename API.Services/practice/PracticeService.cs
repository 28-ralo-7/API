using API.Database;
using API.Domain.practice.domain;
using API.Domain.shared;
using API.Domain.tools;
using API.Domain.user;
using API.Services.practice.interfaces;

namespace API.Services.practice;

public class PracticeService : IPracticeService
{
    private readonly IPracticeRepository _practiceRepository;

    public PracticeService(IPracticeRepository practiceRepository)
    {
        _practiceRepository = practiceRepository;
    }

    public Item[] GetPracticeItemsByPermissions(UserDomain systemUser)
    {
        Practiceschedule[] practiceSchedules = _practiceRepository.GetPracticeSchedulesByUserId(systemUser.Id);
        Guid[] practiceIds = practiceSchedules
            .Select(ps => ps.Practiceid)
            .Distinct()
            .ToArray();

        Practice[] practices = _practiceRepository.GetPracticesByIds(practiceIds);

        Item[] items = practices
            .Select(practice => new Item(practice.Id.ToString(), practice.Name))
            .ToArray();

        return items;
    }

    public void RemovePracticeLogsByUserIds(Guid[] userIds)
    {
        Practicelog[] practicelogs = _practiceRepository.GetPracticeLogsByUserIds(userIds);

        for (int i = 0; i < practicelogs.Length; i++)
        {
            practicelogs[i].Isremoved = true;
        }
        
        _practiceRepository.RemovePracticeLogs(practicelogs);
    }

    public Item[] GetAllPractices()
    {
        Practice[] practices = _practiceRepository.GetAllPractices();
        Item[] options = practices
            .Select(practice => new Item(practice.Id.ToString(), practice.Name))
            .ToArray();

        return options;
    }

    public Response SavePractice(Item practice)
    {
        Response response = new Response();

        if (String.IsNullOrWhiteSpace(practice.Label))
        {
            response.AddError("Заполните название практики");
        }
        else
        {
            Guid id = practice.Value.Length > 0 ? Guid.Parse(practice.Value) : new Guid();

            Practice? existsPractice = _practiceRepository.GetAllPractices().FirstOrDefault(x => x.Name.Trim() == practice.Label.Trim());
            
            if (id != existsPractice?.Id && existsPractice?.Name.Trim() == practice.Label.Trim())
            {
                response.AddError("Такая практика уже есть");
            }
            else if (String.IsNullOrWhiteSpace(practice.Value))
            {
                AddPractice(practice);
            }
            else
            {
                EditPractice(practice);
            }
        }

        return response;
    }

    public Response RemovePractice(string practiceId)
    {
        Guid id = Guid.Parse(practiceId);
        Practice practice = _practiceRepository.GetPracticeById(id);

        Practiceschedule[] practiceschedules = _practiceRepository.GetPracticeSchedulesByPracticeId(practice.Id); 
        Guid[] psIds = practiceschedules.Select(ps => ps.Id).ToArray();

        Practicelog[] practicelogs = _practiceRepository.GetPracticeLogsByPracticescheduleIds(psIds);

        foreach (var ps in practiceschedules)
        {
            ps.Isremoved = true;
        }
        foreach (var pl in practicelogs)
        {
            pl.Isremoved = true;
        }
        
        practice.Isremoved = true;
        _practiceRepository.RemovePractice(practice);
        _practiceRepository.RemovePracticeSchedules(practiceschedules);
        _practiceRepository.RemovePracticeLogs(practicelogs);
        
        return new Response();
    }

    public void RemovePracticeSchedulesByGroupId(Guid groupId)
    {
        Practiceschedule[] practiceschedules = _practiceRepository.GetPracticeSchedulesByGroupId(groupId);
        
        for (int i = 0; i < practiceschedules.Length; i++)
        {
            practiceschedules[i].Isremoved = true;
        }
        
        _practiceRepository.RemovePracticeSchedules(practiceschedules);
    }

    private void AddPractice(Item practice)
    {
        Guid id = Guid.NewGuid();
        Practice newPractice = new Practice(id, practice.Label);

        _practiceRepository.AddPractice(newPractice);
    }
    
    private void EditPractice(Item practice)
    {
        Guid id = Guid.Parse(practice.Value);
        Practice existsPractice = _practiceRepository.GetPracticeById(id);

        existsPractice.Name = practice.Label;

        _practiceRepository.EditPractice(existsPractice);
    }
}