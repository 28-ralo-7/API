using System.ComponentModel.DataAnnotations;

public enum Role
{
    [Display(Name = "Administrator")]
    Administrator = 1,

    [Display(Name = "PracticeLead")]
    PracticeLead = 2
}