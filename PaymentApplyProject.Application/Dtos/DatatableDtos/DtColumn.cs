namespace PaymentApplyProject.Application.Dtos.DatatableDtos
{
    /// <summary>
    /// A jQuery DataTables column.
    /// </summary>
    public class DtColumn
    {
        /// <summary>
        /// Column's data source, as defined by columns.data.
        /// </summary>
        public required string Data { get; set; }

        /// <summary>
        /// Column's name, as defined by columns.name.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Flag to indicate if this column is searchable (true) or not (false). This is controlled by columns.searchable.
        /// </summary>
        public bool Searchable { get; set; }

        /// <summary>
        /// Flag to indicate if this column is orderable (true) or not (false). This is controlled by columns.orderable.
        /// </summary>
        public bool Orderable { get; set; }

        /// <summary>
        /// Search value to apply to this specific column.
        /// </summary>
        public required DtSearch Search { get; set; }
    }
}
