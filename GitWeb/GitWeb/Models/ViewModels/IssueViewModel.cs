using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitWeb.Models
{
    public class IssueViewModel
    {
        public IssueViewModel(IEnumerable<Issue> issues)
        {
            Issues = issues;
        }

        public IEnumerable<Issue> Issues { get; private set; }
    }
}
