using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVPv5.Domain.Entities;

public class DocumentEntity
{
    [Key]
    public int Id { get; set; }

    /*  TODO  */

    [ForeignKey(nameof(UserEntity))]
    public int UserId { get; set; }
}
