//TODO:
//Creo un model Metadata da passare nell'header della risposta
//In riferimento alle paginazioni

namespace Model.Metadatas;

public class PaginationMetadata
{
    //Imposto i campi come proprietà per JsonSerialazer
    public int PageSize {get; set;}
    public int CurrentPage {get; set;}
    public int PreviousPage {get; set;}
    public int NextPage {get; set;}
    public int TotalPageCount {get; set;}
    public int TotalItemCount {get; set;}

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
