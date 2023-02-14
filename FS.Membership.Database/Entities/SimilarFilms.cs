using FS.Membership.Database.Services;
using System.ComponentModel.DataAnnotations.Schema;

namespace FS.Membership.Database.Entities;

public class SimilarFilms : IReferenceEntity
{
    public int ParentFilmId { get; set; }
    public virtual Film? ParentFilm { get; set; }
    public int SimilarFilmId { get; set; }

    [ForeignKey("SimilarFilmId")]

    public virtual Film? SimilarFilm { get; set; }
}
