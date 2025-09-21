

namespace Shared.DTOs;

public record PageResultDTO<T>(IEnumerable<T> Data, int TotalRecords);
