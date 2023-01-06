﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public float Rating { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<Author> AuthorList { get; set; }



    }
}