using GCash.Models.ViewModels;

namespace GCash.Models.TableModels
{
    public class TableModel
    {
        public string TableId { get; set; }

        public string GetUrl { get; set; }

        public string ReadUrl { get; set; }

        public string DeleteUrl { get; set; }

        public string UpdateUrl { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string Keyword { get; set; }

        public List<FieldModel> Columns { get; set; }
    }
}
