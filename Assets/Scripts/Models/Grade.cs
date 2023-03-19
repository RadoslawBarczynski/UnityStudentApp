using Postgrest.Attributes;
using Supabase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Table("Grade")]
public class Grade : SupabaseModel
{

        [PrimaryKey("id", false)]
        public Guid GradeId { get; set; }

        [Column("G_Score")]
        public int G_Score { get; set; }

        //public virtual Student student { get; set; }

   
}
