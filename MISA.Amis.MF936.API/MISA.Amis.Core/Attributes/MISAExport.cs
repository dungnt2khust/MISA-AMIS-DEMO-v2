using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MISAExport : Attribute
    {
        public MISAExport(string name)
        {
            FieldName = name;
        }
        public string FieldName { get; set; }
    }
}
