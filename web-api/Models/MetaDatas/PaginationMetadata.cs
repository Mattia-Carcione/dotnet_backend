//TODO:
//Creo un model Metadata da passare nell'header della risposta
//In riferimento alle paginazioni

namespace Models.Metadatas;

public class PaginationMetadata
{
    //Imposto i campi come proprietà per JsonSerialazer
    public int PageSize {get;}
    public int CurrentPage {get;}
    public int PreviousPage {get;}
    public int NextPage {get;}
    public int TotalPageCount {get;}
    public int TotalItemCount {get;}

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
