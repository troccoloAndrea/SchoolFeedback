using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFeedback.Services.Infrastructure
{
    public interface IMySqlServices
    {
        public DataSet Query(FormattableString query);
    }
}
