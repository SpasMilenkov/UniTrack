namespace UniTrackBackend.Services.Commons.Exceptions;

/// <summary>
/// Represents errors that occur during application execution due to a failed operation.
/// </summary>
public class OperationFailedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OperationFailedException"/> class.
    /// </summary>
    public OperationFailedException() : base() {}

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationFailedException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public OperationFailedException(string message) : base(message) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationFailedException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public OperationFailedException(string message, Exception innerException) 
        : base(message, innerException) {}
}
