using System.ComponentModel.DataAnnotations;

namespace banking_api.Endpoints.Requests
{
    public class UpdateAccountRequest
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string AccountHolderName { get; set; }

        [Required]
        [StringLength(8)]
        public string AccountNumber { get; set; }
    }
}
