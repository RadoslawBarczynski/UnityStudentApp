using Postgrest.Attributes;
using Supabase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Table("Grade")]
public class Grade : SupabaseModel
{

        [PrimaryKey("GradeId", false)]
        public Guid GradeId { get; set; }

        [Column("Score")]
        public int Score { get; set; }

        //public virtual Student student { get; set; }

   
}
