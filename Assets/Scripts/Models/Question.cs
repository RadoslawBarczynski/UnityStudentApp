using Postgrest.Attributes;
using Supabase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[Table("Question")]
public class Question : SupabaseModel
{
    [PrimaryKey("id", false)]
    public Guid id { get; set; }

    [Column("operation")]
    public string operation { get; set; }
    [Column("answer1")]
    public double answer1 { get; set; }
    [Column("answer2")]
    public double answer2 { get; set; }
    [Column("answer3")]
    public double answer3 { get; set; }
}
