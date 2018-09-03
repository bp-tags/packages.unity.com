using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Filters;
using UnityEngine.TestRunner.NUnitExtensions.Filters;

namespace UnityEngine.TestTools.TestRunner.GUI
{
    [Serializable]
    internal class TestRunnerFilter
    {
#pragma warning disable 649
        public string[] assemblyNames;
        public string[] groupNames;
        public string[] categoryNames;
        public static TestRunnerFilter empty = new TestRunnerFilter();
        public string[] testNames;
        public string[] testsToSkip;

        public static string AssemblyNameFromPath(string path)
        {
            string output = Path.GetFileName(path);
            if (output != null && output.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                return output.Substring(0, output.Length - 4);
            return output;
        }

        private bool CategoryMatches(IEnumerable<string> categories)
        {
            if (categoryNames == null || categoryNames.Length == 0)
                return true;

            foreach (string category in categories)
            {
                if (categoryNames.Contains(category))
                    return true;
            }

            return false;
        }

        private bool IDMatchesAssembly(string id)
        {
            if (AreOptionalFiltersEmpty())
                return true;

            if (assemblyNames == null)
                return false;

            int openingBracket = id.IndexOf('[');
            int closingBracket = id.IndexOf(']');
            if (openingBracket >= 0 && openingBracket < id.Length && closingBracket > openingBracket && openingBracket < id.Length)
            {
                //Some assemblies are absolute and explicitly part of the test ID e.g.
                //"[/path/to/assembly-name.dll][rest of ID ...]"
                //While some are minimal assembly names e.g.
                //"[assembly-name][rest of ID ...]"
                //Strip them down to just the assembly name
                string assemblyNameFromID = AssemblyNameFromPath(id.Substring(openingBracket + 1, closingBracket - openingBracket - 1));
                foreach (string assemblyName in assemblyNames)
                {
                    if (assemblyName.Equals(assemblyNameFromID, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }
            return false;
        }

        private bool NameMatches(string name)
        {
            if (AreOptionalFiltersEmpty())
                return true;

            if (groupNames == null)
                return false;

            foreach (var nameFromFilter in groupNames)
            {
                //Strict regex match for test group name on its own
                if (Regex.IsMatch(name, nameFromFilter))
                    return true;
                //Match test names that end with parametrized test values and full nunit generated test names that have . separators
                var regex = nameFromFilter.TrimEnd('$') + @"[\.|\(.*\)]";
                if (Regex.IsMatch(name, regex))
                    return true;
            }
            return false;
        }

        private bool AreOptionalFiltersEmpty()
        {
            if (assemblyNames != null && assemblyNames.Length != 0)
                return false;
            if (groupNames != null && groupNames.Length != 0)
                return false;
            if (testNames != null && testNames.Length != 0)
                return false;
            return true;
        }

        private bool NameMatchesExactly(string name)
        {
            if (AreOptionalFiltersEmpty())
                return true;

            if (testNames == null)
                return false;

            foreach (var exactName in testNames)
            {
                if (name == exactName)
                    return true;
            }
            return false;
        }

        private static void ClearAncestors(IEnumerable<TestRunnerResult> newResultList, string parentID)
        {
            if (string.IsNullOrEmpty(parentID))
                return;

            foreach (var result in newResultList)
            {
                if (result.id == parentID)
                {
                    result.Clear();
                    ClearAncestors(newResultList, result.parentID);
                    break;
                }
            }
        }

        public void ClearResults(List<TestRunnerResult> newResultList)
        {
            foreach (var result in newResultList)
            {
                if (!result.isSuite && CategoryMatches(result.categories))
                {
                    if (IDMatchesAssembly(result.id) || NameMatches(result.fullName) || NameMatchesExactly(result.fullName))
                    {
                        result.Clear();
                        ClearAncestors(newResultList, result.parentID);
                    }
                }
            }
        }

        public ITestFilter BuildNUnitFilter()
        {
            var filter = TestFilter.Empty;
            var nameAndAssemblyFilters = new List<ITestFilter>();

            if (testNames != null && testNames.Length != 0)
            {
                var nameFilter = new OrFilter(testNames.Select(n => new FullNameFilter(n)).ToArray());
                nameAndAssemblyFilters.Add(nameFilter);
            }

            if (groupNames != null && groupNames.Length != 0)
            {
                var exactNamesFilter = new OrFilter(groupNames.Select(n =>
                    {
                        var f = new FullNameFilter(n);
                        f.IsRegex = true;
                        return f;
                    }).ToArray());
                nameAndAssemblyFilters.Add(exactNamesFilter);
            }

            if (assemblyNames != null && assemblyNames.Length != 0)
            {
                var assemblyFilter = new OrFilter(assemblyNames.Select(c => new AssemblyNameFilter(c)).ToArray());
                nameAndAssemblyFilters.Add(assemblyFilter);
            }

            if (nameAndAssemblyFilters.Any())
                filter = new OrFilter(nameAndAssemblyFilters.ToArray());

            if (categoryNames != null && categoryNames.Length != 0)
            {
                var categoryFilter = new OrFilter(categoryNames.Select(c => new CategoryFilterExtended(c) {IsRegex = true}).ToArray());
                filter = new AndFilter(categoryFilter, filter);
            }

            if (testsToSkip != null && testsToSkip.Any())
            {
                var skipTestsFilter = new AndFilter(testsToSkip.Select(t => new NotFilter(new FullNameFilter(t))).ToArray());
                filter = new AndFilter(skipTestsFilter, filter);
            }

            return filter;
        }
    }
}
