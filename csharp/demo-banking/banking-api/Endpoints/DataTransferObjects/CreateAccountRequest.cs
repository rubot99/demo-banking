using System.ComponentModel.DataAnnotations;

namespace banking_api.Endpoints.Requests
{
    public class CreateAccountRequest
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string AccountHolderName { get; set; }
    }
}
