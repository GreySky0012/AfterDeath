using System;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class MyException : ApplicationException
{
    public int _exceptionLevel;
    public int _exceptionId;

    ///<summary>
    /// 默认构造函数
    ///</summary>
    public MyException() { }
    public MyException(string message)
            : base(message) { }
    public MyException(string message, Exception inner)
            : base(message, inner) { }
    public MyException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
}
