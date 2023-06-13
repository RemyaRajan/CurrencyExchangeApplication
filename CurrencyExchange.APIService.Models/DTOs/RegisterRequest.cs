namespace CurrencyExchange.APIService.Models.DTOs
{
    public class RegisterRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string[] Roles { get; set; }

    }
}
