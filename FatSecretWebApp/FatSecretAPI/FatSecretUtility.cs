using System;

namespace FatSecretWebApp
{
    public static class FatSecretUtility 
    {
        public static int GetFatSecretDate(DateTime dtparamDate)
        {
            int intFatSecretDate = 0;
            DateTime dtStartDate;
            DateTime.TryParse("1/1/1970", out dtStartDate);
            intFatSecretDate = Convert.ToInt32((dtparamDate - dtStartDate).TotalDays);
            return intFatSecretDate;
        }

        public static string GetDateFromFatSecretDay(string strFatSecretDate)
        {
            DateTime dtFatSecretDate = new DateTime();
            DateTime dtFatSecretStartDate;
            DateTime.TryParse("1/1/1970", out dtFatSecretStartDate);

            dtFatSecretDate = dtFatSecretStartDate.AddDays(Convert.ToInt32(strFatSecretDate));

            return dtFatSecretDate.ToShortDateString();
        }

        public static string GetPoundsFromKilos(string paramKilos)
        {
            string pounds = "";
            double dblKilos = 1;
            try
            {
                dblKilos = Convert.ToDouble(paramKilos);
            }
            catch { }
            pounds = (dblKilos * 2.20462262185).ToString();
            return pounds.ToString();
        }
    }
}
