

namespace Shared.Enums;


public enum ErrorType
{
    Validation,     // 400 Bad Request
    NotFound,       // 404 Not Found
    Failure,        // 500 Internal Server Error
    Conflict,       // 409 Conflict
    Unauthorized,   // 401 Unauthorized
    Forbidden       // 403 Forbidden
}