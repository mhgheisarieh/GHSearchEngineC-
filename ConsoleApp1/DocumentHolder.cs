using System;
using System.Collections.Generic;
using System.Text;

namespace GHSearchEngine
{
    class DocumentHolder
    {
        private List<String> documents;

        DocumentHolder(List<String> documents)
        {
            this.documents = documents;
        }

        public List<String> getDocuments()
        {
            return documents;
        }
    }
}
