using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitWeb.Models
{
    public class IndexViewModel
    {
        public IndexViewModel(IEnumerable<Repository> repositories)
        {
            Repositories = repositories;
        }

        public IEnumerable<Repository> Repositories { get; private set; }
    }
}
