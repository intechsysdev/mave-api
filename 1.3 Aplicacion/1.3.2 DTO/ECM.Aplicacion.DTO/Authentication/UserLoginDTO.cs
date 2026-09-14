using System;

namespace ECM.Aplicacion.DTO.Authentication
{
    public class UserLoginDTO
    {
        public string CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string UserCust { get; set; }

        public string UserSuccli { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }
        
        public string Phone { get; set; }

        public string ImageLink { get; set; }

        public string FullName { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }

        public DateTime LastConnectionTime { get; set; }

        public string LastConnectionDevice { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool ApprovedTerms { get; set; }

    }
}
