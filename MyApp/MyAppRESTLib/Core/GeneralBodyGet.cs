using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTLib.Core
{
    /// <summary>
    /// Indicate possible errors during rest operations. 
    /// Used during operations that converts REST json body into something in server side.
    /// Used to sepparate errors in JsonBody from errors in server side
    /// </summary>
    public class RestExceptionError
    {
        public RestExceptionError()
        {
            ErrorType = _ErrorType.NoError;
            SourceError = _SourceError.Unknown;
        }
        public enum _ErrorType 
        { 
            NoError,
            CastError
        }

        public enum _SourceError
        { 
            /// <summary>
            /// Some exception happened. The server side dev. did not blamed the source.
            /// </summary>
            Unknown,
            
            /// <summary>
            /// Some Server Side error
            /// </summary>
            ServerSide,
            
            /// <summary>
            /// Client side error. Probaly wrong/mal formed parameters
            /// </summary>
            ClientSide
        }

        public _SourceError SourceError { get; set; }

        public _ErrorType ErrorType { get; set; }
        public string InternalMessage{ get; set; }
        public string ExceptionMessage{ get; set; }
        public string StackTrace{ get; set; }
    }

    public class DataFilterExpressionREST
    {
        public string FieldName { get; set; }
        public string Filter { get; set; }
        public enum _FilterType { Equal, Contains, Dynamic}
        public _FilterType FilterType { get; set; }
    }

    /// <summary>
    /// Represents generic body used for filtering GET data
    /// </summary>
    public class GeneralBodyGet
    {
        public GeneralBodyGet()
        {
            NumberOfItens = 1;
            Filters = new List<DataFilterExpressionREST>();
        }

        /// <summary>
        /// Number of itens in list to retrieve.
        /// </summary>
        public int NumberOfItens { get; set; }

        /// <summary>
        /// Indicate the position of the list retrieved. Before the index itens are ignored and not retrieved
        /// </summary>
        public int ItensToSkip { get; set; }

        /// <summary>
        /// Ordering, like fieldA Desc, FiledB Asc
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Filtering list
        /// </summary>
        public List<DataFilterExpressionREST> Filters { get; set; }
    }
}
