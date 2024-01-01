using System;

namespace techStackNotFoundException.Exceptions
{
    public class TechStackNotFoundException : Exception
    {
    
        public override string Message
        {
            get
            {
                return "I have not worked with that in a project before. Check out /resume/experience to see if I applied it there!";
            }
        }
    }
}
