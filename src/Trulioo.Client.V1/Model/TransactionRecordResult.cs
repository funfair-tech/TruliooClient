using System.Collections.Generic;

namespace Trulioo.Client.V1.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class TransactionRecordResult : VerifyResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<DataField> InputFields { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionId"></param>
        public TransactionRecordResult(string transactionId) : base(transactionId)
        {
        }
    }
}
