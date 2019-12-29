using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    public string Username { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Parole")]
    public string Password { get; set; }

    [Display(Name = "Atcerēties mani")]
    public bool RememberMe { get; set; }
    public string ReturnUrl { get; set; }

}
