using System;
using System.Threading;

namespace AMSExplorer
{
    public class TransferEntryResponse
    {
        public Guid Id;
        public CancellationToken token;
    }
}
