using Postgrest.Attributes;
using Supabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Table("Homework")]
public class Homework : SupabaseModel
{
    [PrimaryKey("id", false)]
    public Guid id { get; set; }

    [Column("ScoreToGet")]
    public int ScoreToGet { get; set; }

    [Column("GameNumber")]
    public int GameNumber { get; set; }

    [Column("isActive")]
    public bool isActive { get; set; }
}
