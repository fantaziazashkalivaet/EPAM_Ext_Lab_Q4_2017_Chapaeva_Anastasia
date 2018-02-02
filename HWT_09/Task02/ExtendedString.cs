namespace Task02
{
    public static class ExtendedString
    {
        /// <summary>
        /// Определяет, является ли строка целым положительным числом
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPositiveInteger(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            foreach (var e in str)
            {
                if (!char.IsDigit(e))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
