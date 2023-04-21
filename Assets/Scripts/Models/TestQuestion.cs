using Postgrest.Attributes;
using Supabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    [Table("TestQuestions")]
    public class TestQuestion : SupabaseModel
    {
        [PrimaryKey("id", false)]
        public Guid id { get; set; }

        [Column("TestId")]
        public Guid TestId { get; set; }
        //public Test test { get; set; }
        [Column("QuestionId")]
        public Guid QuestionId { get; set; }
       // public Question question { get; set; }

    }
}
