namespace FS.Common.DTOs;

public class SimilarFilmsDTO
{
    public int ParentFilmId { get; set; }
    public int SimilarFilmId { get; set; }

    public FilmDTO Film { get; set; }
    public FilmDTO Similarfilm { get; set; }
}

public class SimilarFilmsCreateDTO
{
    public int ParentFilmId { get; set; }
    public int SimilarFilmId { get; set; }
}