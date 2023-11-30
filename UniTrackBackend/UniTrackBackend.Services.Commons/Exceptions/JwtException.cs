namespace UniTrackBackend.Services.Commons.Exceptions;

/// <summary>
/// Represents errors that occur during JWT token processing.
/// </summary>
public class JwtException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JwtException"/> class.
    /// </summary>
    public JwtException() : base() {}

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public JwtException(string message) : base(message) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public JwtException(string message, Exception innerException) 
        : base(message, innerException) {}
}
