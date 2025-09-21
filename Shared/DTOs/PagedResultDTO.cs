

namespace Shared.DTOs;

public record PagedResultDTO<T>(IEnumerable<T> Data, int TotalRecords);
