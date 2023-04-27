using System.Runtime.Serialization;

namespace TaskMaster.Exceptions;

[Serializable]
public class UserDuplicateException : Exception
{
    public UserDuplicateException(string message) : base(message)
    {
        
    }
    
    protected UserDuplicateException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}