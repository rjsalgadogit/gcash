namespace GCash.Models.ViewModels
{
    public class ColumnModel
    {
        public string FieldName { get; set; }

        public string DataType { get; set; }

        public string ColumnName { get { return !string.IsNullOrEmpty(CustomColumnName) ? CustomColumnName : FieldName; } }

        public string CustomRowName { get; set; }

        public string CustomColumnName { get; set; }

        public bool IsKey { get; set; }

        public bool HideFromTable { get; set; }

        public bool HasOption { get; set; }

        public bool HideFromPopup { get; set; }
    }
}
