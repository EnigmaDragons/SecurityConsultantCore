namespace SecurityConsultantCore.Common
{
    public static class ArgCleaner
    {
        public static string RemoveParantheses(this string arg)
        {
            return arg.Replace("(", "").Replace(")", "");
        }

        public static string RemoveSpaces(this string arg)
        {
            return arg.Replace(" ", "");
        }

        public static string[] CleanAndSplit(this string arg, char ch)
        {
            var cleanedArg = arg.RemoveParantheses().RemoveSpaces();
            return cleanedArg.Split(ch);
        }
    }
}