using System;

namespace documentIdNotFoundException.Exceptions
{
    public class DocumentIdNotFoundException : Exception
    {
    
        public override string Message
        {
            get
            {
                return "The document ID does not exist in the collection.";
            }
        }
    }
}
