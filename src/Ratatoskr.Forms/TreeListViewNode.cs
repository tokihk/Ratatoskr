using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.Forms
{
    public class TreeListViewNode : TreeNode
    {
        public List<object> Values = new List<object>();
    }
}
