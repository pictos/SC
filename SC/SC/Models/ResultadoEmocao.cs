﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.Models
{
    public class ResultadoEmocao
    {
        public Facerectangle FaceRectangle { get; set; }
        public Scores Scores { get; set; }
    }

        public class Facerectangle
        {
            public int Height { get; set; }
            public int Left { get; set; }
            public int Top { get; set; }
            public int Width { get; set; }
        }

        public class Scores
        {
            public float Anger { get; set; }
            public float Contempt { get; set; }
            public float Disgust { get; set; }
            public float Fear { get; set; }
            public float Happiness { get; set; }
            public float Neutral { get; set; }
            public float Sadness { get; set; }
            public float Surprise { get; set; }
        }

    }

