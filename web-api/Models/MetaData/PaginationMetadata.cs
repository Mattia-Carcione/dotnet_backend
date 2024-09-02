//TODO:
//Creo un model Metadata da passare nell'header della risposta
//In riferimento alle paginazioni

namespace Models.Metadata;

/// <summary>
/// Represents metadata information for pagination.
/// </summary>
public class PaginationMetadata
{
    //NB: Imposto i campi come proprietà per JsonSerialazer

    /// <summary>
    /// Gets the number of items included in each page.
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Gets the current page number.
    /// </summary>
    public int CurrentPage { get; }

    /// <summary>
    /// Gets the page number of the previous page.
    /// </summary>
    public int PreviousPage { get; }

    /// <summary>
    /// Gets the page number of the next page.
    /// </summary>
    public int NextPage { get; }

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPageCount { get; }

    /// <summary>
    /// Gets the total number of items across all pages.
    /// </summary>
    public int TotalItemCount { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginationMetadata"/> class.
    /// </summary>
    /// <param name="pageSize">The number of items included in each page.</param>
    /// <param name="currentPage">The current page number.</param>
    /// <param name="totalItemCount">The total number of items across all pages.</param>
    public PaginationMetadata(int pageSize, int currentPage, int totalItemCount)
    {
        PageSize = pageSize;
        TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
        CurrentPage = currentPage > TotalPageCount ? TotalPageCount : currentPage;
        PreviousPage = (currentPage - 1) <= 0 ? CurrentPage : CurrentPage - 1;
        NextPage = (currentPage + 1) > TotalPageCount ? TotalPageCount : currentPage + 1;
        TotalItemCount = totalItemCount;
    }
}
