namespace UniTrackBackend.Services.Commons.Exceptions;

/// <summary>
/// Represents errors that occur when a required data item is not found.
/// </summary>
public class DataNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataNotFoundException"/> class.
    /// </summary>
    public DataNotFoundException() : base() {}

    /// <summary>
    /// Initializes a new instance of the <see cref="DataNotFoundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public DataNotFoundException(string message) : base(message) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="DataNotFoundException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public DataNotFoundException(string message, Exception innerException) 
        : base(message, innerException) {}
}
