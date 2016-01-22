using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemsControlTester {
    public static class Constants {
        // We allow all Unicode letter characters as well as internal spaces and hypens, as long as these do not occur in sequences.
        public const string NAME_REGEX_PATTERN = @"\A\p{L}+([\p{Zs}\-][\p{L}]+)*\z";

        // We allow all Unicode letter and numeric characters as well as internal spaces, as long as these do not occur in sequences.
        public const string ADDRESS_REGEX_PATTERN = @"\A[\p{L}\p{N}]+([\p{Zs}][\p{L}\p{N}]+)*\z";

        // We allow all Unicode umeric characters and hypens, as long as these do not occur in sequences.
        public const string NUMBER_REGEX_PATTERN = @"\A\p{N}+([\p{N}\-][\p{N}]+)*\z";

        // bool
        public const string BOOLEAN_REGEX_PATTERN = @"^([Tt][Rr][Uu][Ee]|[Ff][Aa][Ll][Ss][Ee])$";

        // datetime
        public const string DATE_WITH_SLASHES_REGEX_PATTERN = @"^\d{1,2}\/\d{1,2}\/\d{4}$";
        public const string DATETIME_REGEX_PATTERN = @"^((((([13578])|(1[0-2]))[\-\/\s]?(([1-9])|([1-2][0-9])|(3[01])))|((([469])|(11))[\-\/\s]?(([1-9])|([1-2][0-9])|(30)))|(2[\-\/\s]?(([1-9])|([1-2][0-9]))))[\-\/\s]?\d{4})(\s((([1-9])|(1[02]))\:([0-5][0-9])((\s)|(\:([0-5][0-9])\s))([AM|PM|am|pm]{2,2})))?$";

        // int
        public const string POSITIVE_INTEGER_REGEX_PATTERN = @"^\d+$";
        public const string SIGNED_INTEGER_REGEX_PATTERN = @"^(\+|-)?\d+$";
        public const string SMALL_ZIP_REGEX_PATTERN = @"^\d{5}$";

        // money
        //public const string MONEY_REGEX_PATTERN = @"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$";    // allows 4 decimals
        public const string MONEY_REGEX_PATTERN = @"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,10})?))\)?$";     // allows 10 decimals

        // decimal
        public const string DECIMAL_REGEX_PATTERN = @"[0-9]+(\.[0-9][0-9]?)?";

        public const string MONTHNUMBER_REGEX_PATTERN = @"\A(0?[1-9]|1[0-2])\z";
        public const string YEARNUMBER_REGEX_PATTERN = @"\A([2-9]\d\d\d)\z";
        public const string SSN_REGEX_PATTERN = @"^\d{3}-?\d{2}-?\d{4}$";
    }
}
