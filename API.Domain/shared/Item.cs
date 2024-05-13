namespace API.Domain.shared;

public class Item
{
    public String Value { get; set; }
    public String Label { get; set; }

    public Item(string value, string label)
    {
        Value = value;
        Label = label;
    }
}