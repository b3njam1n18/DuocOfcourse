using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.DTO
{
    public class SubmitAnswersRequest
    {
        public List<SubmitAnswer> Answers { get; set; } = new();
    }
}
