using System;

namespace skillNotFoundException.Exceptions
{
    public class SkillNotFoundException : Exception
    {
    
        public override string Message
        {
            get
            {
                return "I have not used this skill in the workforce yet. Check out /resume/projects to see if I applied it there!";
            }
        }
    }
}
