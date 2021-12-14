﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using API.Model;

namespace API.Models
{
    [Table("tb_m_university")]
    public class University
    {
      

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
    }



}
        


    

