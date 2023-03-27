using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmpApp.Models
{
    enum CardType
    {
        [Description("Черновик")]
        Draft,

        [Description("Готовый")]
        Ready,

        [Description("Шаблон")]
        Template,

        [Description("Архив")]
        Archive
    }
}
