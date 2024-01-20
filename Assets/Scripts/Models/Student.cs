using Supabase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Postgrest.Attributes;
using System.Threading.Tasks;

[Table("Student")]
public class Student : SupabaseModel
{
    [PrimaryKey("id", false)]
    public Guid id { get; set; }

    [Column("StudentFirstName")]
    public string StudentFirstName { get; set; }

    [Column("StudentLastName")]
    public string StudentLastName { get; set; }

    [Column("Login")]
    public string StudentLogin { get; set; }

    [Column("Password")]
    public string StudentPassword { get; set; }

    [Column("TeacherId")]
    public Guid TeacherId { get; set; } 



    [Column("GradeId")]
    public Guid? GradeId { get; set; }


    //public virtual Grade grade { get; set; }
}
