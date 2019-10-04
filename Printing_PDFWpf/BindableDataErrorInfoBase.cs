using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printing_PDFWpf
{
    public abstract class BindableDataErrorInfoBase : BindableBase
    {
        #region IDataErrorInfo Members

        public Dictionary<string, string> ErrorDictionary = new Dictionary<string, string>();

        public string Error
        {
            get { return String.Join("; ", ErrorDictionary.Values); }
        }

        public string this[string columnName]
        {
            get
            {
                if (ErrorDictionary.ContainsKey(columnName))
                    return ErrorDictionary[columnName];
                return null;
            }
        }

        #endregion
    }
}
