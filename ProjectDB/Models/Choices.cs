using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProjectDB.Models;
public class Choices
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Choice_ID { get; set; }
    public required string Choice1 { get; set; }
    public required string Choice2 { get; set; }
    public required string Choice3 { get; set; }

    [ForeignKey("Question")]
    public int Q_ID { get; set; }
    public required Questions Question { get; set; }
}
