using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WebApi.DAL
{
    public static partial class Inflector
    {
        private static readonly List<Rule> _plurals = new List<Rule>();
        private static readonly List<Rule> _singulars = new List<Rule>();
        private static readonly List<string> _uncountables = new List<string>();

        private class Rule
        {
            private readonly Regex _regex;
            private readonly string _replacement;

            public Rule(string pattern, string replacement)
            {
                _regex = new Regex(pattern, RegexOptions.IgnoreCase);
                _replacement = replacement;
            }

            public string Apply(string word)
            {
                if (!_regex.IsMatch(word))
                {
                    return null;
                }

                return _regex.Replace(word, _replacement);
            }
        }

        private static void AddIrregular(string singular, string plural)
        {
            AddPlural("(" + singular[0] + ")" + singular.Substring(1) + "$", "$1" + plural.Substring(1));
            AddSingular("(" + plural[0] + ")" + plural.Substring(1) + "$", "$1" + singular.Substring(1));
        }

        private static void AddUncountable(string word)
        {
            _uncountables.Add(word.ToLower());
        }

        private static void AddPlural(string rule, string replacement)
        {
            _plurals.Add(new Rule(rule, replacement));
        }

        private static void AddSingular(string rule, string replacement)
        {
            _singulars.Add(new Rule(rule, replacement));
        }

        public static string Pluralize(this string word)
        {
            return ApplyRules(_plurals, word);
        }

        public static string Singularize(this string word)
        {
            return ApplyRules(_singulars, word);
        }

        private static string ApplyRules(List<Rule> rules, string word)
        {
            string result = word;

            if (!_uncountables.Contains(word.ToLower()))
            {
                for (int i = rules.Count - 1; i >= 0; i--)
                {
                    if ((result = rules[i].Apply(word)) != null)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        public static string Titleize(this string word)
        {
            return Regex.Replace(Humanize(Underscore(word)), @"\b([a-z])",
                                 delegate (Match match)
                                 {
                                     return match.Captures[0].Value.ToUpper();
                                 });
        }

        public static string Humanize(this string lowercaseAndUnderscoredWord) => Capitalize(Regex.Replace(lowercaseAndUnderscoredWord, @"_", " "));

        public static string Underscore(this string pascalCasedWord)
        {
            return Regex.Replace(
                Regex.Replace(
                    Regex.Replace(pascalCasedWord, @"([A-Z]+)([A-Z][a-z])", "$1_$2"), @"([a-z\d])([A-Z])",
                    "$1_$2"), @"[-\s]", "_").ToLower();
        }

        public static string Capitalize(this string word) => word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower();

        public static string Uncapitalize(this string word) => word.Substring(0, 1).ToLower() + word.Substring(1);

        public static string Ordinalize(this string numberString) => Ordanize(int.Parse(numberString), numberString);

        public static string Ordinalize(this int number) => Ordanize(number, number.ToString());

        private static string Ordanize(int number, string numberString) => numberString + ".";

        public static string Dasherize(this string underscoredWord) => underscoredWord.Replace('_', '-');
    }
}

