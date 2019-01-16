using System.Collections.Generic;
using Trulioo.Client.V1.Model.Errors;

namespace Trulioo.Client.V1.Model
{
    /// <summary>
    /// A result from a particular datasource
    /// </summary>
    public class DatasourceResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string DatasourceStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DatasourceName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DatasourceField> DatasourceFields { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<AppendedField> AppendedFields { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ServiceError> Errors { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> FieldGroups { get; set; }
    }
}
