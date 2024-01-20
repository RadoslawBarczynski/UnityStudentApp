using Postgrest.Attributes;
using Supabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    [Table("Test")]
    public class Test : SupabaseModel
    {
    [PrimaryKey("id", false)]
    public Guid id { get; set; }

    [Column("TestName")]
    public string TestName { get; set; }

    [Column("isActive")]
    public bool isActive { get; set; }

    [Column("TeacherId")]
    public Guid TeacherId { get; set; }
}
