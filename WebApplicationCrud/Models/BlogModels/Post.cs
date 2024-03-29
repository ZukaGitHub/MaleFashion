﻿using WebApplicationCrud.Models.BlogModels;
using System;
using System.Collections.Generic;

namespace WebApplicationCrud.Models.BlogModels
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public string Image { get; set; } = "";
        public string Author { get; set; }
        public string Qoute { get; set; }
        public string QouteAuthor { get; set; }
        public string Description { get; set; } = "";
        public string Tags { get; set; } = "";
        public string Category { get; set; } = "";

        public DateTime Created { get; set; } = DateTime.Now;

        public List<MainComment> MainComments { get; set; }
    }
}
