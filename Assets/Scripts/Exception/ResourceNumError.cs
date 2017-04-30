using System;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class ResourceReadError : MyException
{
    ///<summary>
    /// 默认构造函数
    ///</summary>
    public ResourceReadError() { _exceptionId = 10000;_exceptionLevel = 1; }
    public ResourceReadError(string message)
            : base(message) { _exceptionId = 10000; _exceptionLevel = 1; }
    public ResourceReadError(string message, Exception inner)
            : base(message, inner) { _exceptionId = 10000; _exceptionLevel = 1; }
    public ResourceReadError(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { _exceptionId = 10000; _exceptionLevel = 1; }
}
