using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaLight.Model
{
    public class Chapter
    {
        public string ChapterName { get; set; }
        public Uri ChapterUri { get; set; }
        public bool IsDownloaded { get; set; }
        public bool IsRead { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
