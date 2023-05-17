using Supabase;
using Postgrest.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Table("Logs")]
public class Logs : SupabaseModel
{
    [PrimaryKey("id", false)]
    public Guid id { get; set; }

    [Column("comment")]
    public string comment { get; set; }
    [Column("datetime")]
    public DateTime datetime { get; set; }
    [Column("user")]
    public string user { get; set; }

}
