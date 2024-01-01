using System;

namespace companyNotFoundException.Exceptions
{
    public class CompanyNotFoundException : Exception
    {
    
        public override string Message
        {
            get
            {
                return "I have not been employed at this company. Maybe some day :)\nCheck the spelling and try again.";
            }
        }
    }
}
