using System.ComponentModel.DataAnnotations;

namespace EtecBookAPI.DataTransferObjects;

public class RegisterDto
{
    [Required(ErrorMessage = "Informe o Nome do Usuário")]
    [StringLength(60, ErrorMessage = "O Nome precisa ter no maximo 60 caracteres")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Informe o Email do Usuário")]
    [EmailAddress(ErrorMessage = "Informe um Email Válido")]
    [StringLength(100, ErrorMessage = "Email precisa ter no maximo 100 caracteres")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Informe a Senha de Acesso")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha precisa ter no minimo 6 caracteres")]
    public string Password { get; set; }
}
