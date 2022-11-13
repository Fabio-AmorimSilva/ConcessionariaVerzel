using System;
using System.Collections.Generic;

namespace Concessionaria.Aplicacao.Options
{
    public class FileSettings
    {
        public string CarroImgDirectory { get; set; } = "img\\carros";    
        public string[] CarroFileTypes { get; set; } = new string[0];
        public string DefaultCarroImgPath { get; set; } = "";
    }
}
