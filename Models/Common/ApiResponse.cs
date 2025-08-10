namespace WebApiTemplate.Models.Common;

/// <summary>
/// Generic API response wrapper
/// </summary>
/// <typeparam name="T">Type of the data being returned</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Whether the operation was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Response message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// The actual data
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// List of errors if any
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// HTTP status code
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Timestamp of the response
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Creates a successful response
    /// </summary>
    /// <param name="data">The data to return</param>
    /// <param name="message">Success message</param>
    /// <param name="statusCode">HTTP status code</param>
    /// <returns>ApiResponse instance</returns>
    public static ApiResponse<T> Success(T data, string message = "Operation completed successfully", int statusCode = 200)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data,
            StatusCode = statusCode
        };
    }

    /// <summary>
    /// Creates an error response
    /// </summary>
    /// <param name="message">Error message</param>
    /// <param name="errors">List of errors</param>
    /// <param name="statusCode">HTTP status code</param>
    /// <returns>ApiResponse instance</returns>
    public static ApiResponse<T> Error(string message, List<string>? errors = null, int statusCode = 400)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors ?? new List<string>(),
            StatusCode = statusCode
        };
    }
}

/// <summary>
/// Paginated response wrapper
/// </summary>
/// <typeparam name="T">Type of the data being returned</typeparam>
public class PaginatedResponse<T>
{
    /// <summary>
    /// List of items
    /// </summary>
    public List<T> Items { get; set; } = new();

    /// <summary>
    /// Total count of items
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Current page number
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Page size
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Whether there is a next page
    /// </summary>
    public bool HasNextPage { get; set; }

    /// <summary>
    /// Whether there is a previous page
    /// </summary>
    public bool HasPreviousPage { get; set; }

    /// <summary>
    /// Creates a paginated response
    /// </summary>
    /// <param name="items">List of items</param>
    /// <param name="totalCount">Total count</param>
    /// <param name="pageNumber">Current page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>PaginatedResponse instance</returns>
    public static PaginatedResponse<T> Create(List<T> items, int totalCount, int pageNumber, int pageSize)
    {
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PaginatedResponse<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages,
            HasNextPage = pageNumber < totalPages,
            HasPreviousPage = pageNumber > 1
        };
    }
}

/// <summary>
/// Pagination parameters
/// </summary>
public class PaginationParameters
{
    private const int MaxPageSize = 50;
    private int _pageSize = 10;

    /// <summary>
    /// Page number (default: 1)
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Page size (default: 10, max: 50)
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    /// <summary>
    /// Search term
    /// </summary>
    public string? SearchTerm { get; set; }

    /// <summary>
    /// Sort by field
    /// </summary>
    public string? SortBy { get; set; }

    /// <summary>
    /// Sort direction (asc/desc)
    /// </summary>
    public string? SortDirection { get; set; } = "asc";
} 