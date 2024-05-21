using API.Domain.shared;

namespace API.Domain.adminPanel;

public class UserSettingOptions
{
    public Item[] RoleOptions { get; set; }
    public Item[] GroupOptions { get; set; }

    public UserSettingOptions(Item[] roleOptions, Item[] groupOptions)
    {
        RoleOptions = roleOptions;
        GroupOptions = groupOptions;
    }
}