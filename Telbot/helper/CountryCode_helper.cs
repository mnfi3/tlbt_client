using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telbot.helper
{
    class CountryCode_helper
    {
        private static Dictionary<string, string> codes = new Dictionary<string, string>();

        public static Dictionary<string, string> getCodes()
        {
            init();
            return codes;
        }


        public static void init(){

           Add("Iran", "98");
           Add("Afghanistan", "93");
           Add("Albania", "355");
           Add("Algeria", "213");
           Add("American Samoa", "1-684");
           Add("Andorra", "376");
           Add("Angola", "244");
           Add("Anguilla", "1-264");
           Add("Antarctica", "672");
           Add("Antigua and Barbuda", "1-268");
           Add("Argentina", "54");
           Add("Armenia", "374");
           Add("Aruba", "297");
           Add("Australia", "61");
           Add("Austria", "43");
           Add("Azerbaijan", "994");
           Add("Bahamas", "1-242");
           Add("Bahrain", "973");
           Add("Bangladesh", "880");
           Add("Barbados", "1-246");
           Add("Belarus", "375");
           Add("Belgium", "32");
           Add("Belize", "501");
           Add("Benin", "229");
           Add("Bermuda", "1-441");
           Add("Bhutan", "975");
           Add("Bolivia", "591");
           Add("Bosnia and Herzegovina", "387");
           Add("Botswana", "267");
           Add("Brazil", "55");
           Add("British Indian Ocean Territory", "246");
           Add("British Virgin Islands", "1-284");
           Add("Brunei", "673");
           Add("Bulgaria", "359");
           Add("Burkina Faso", "226");
           Add("Burundi", "257");
           Add("Cambodia", "855");
           Add("Cameroon", "237");
           Add("Canada", "1");
           Add("Cape Verde", "238");
           Add("Cayman Islands", "1-345");
           Add("Central African Republic", "236");
           Add("Chad", "235");
           Add("Chile", "56");
           Add("China", "86");
           Add("Christmas Island", "61");
           Add("Cocos Islands", "61");
           Add("Colombia", "57");
           Add("Comoros", "269");
           Add("Cook Islands", "682");
           Add("Costa Rica", "506");
           Add("Croatia", "385");
           Add("Cuba", "53");
           Add("Curacao", "599");
           Add("Cyprus", "357");
           Add("Czech Republic", "420");
           Add("Democratic Republic of the Congo", "243");
           Add("Denmark", "45");
           Add("Djibouti", "253");
           Add("Dominica", "1-767");
           
           Add("East Timor", "670");
           Add("Ecuador", "593");
           Add("Egypt", "20");
           Add("El Salvador", "503");
           Add("Equatorial Guinea", "240");
           Add("Eritrea", "291");
           Add("Estonia", "372");
           Add("Ethiopia", "251");
           Add("Falkland Islands", "500");
           Add("Faroe Islands", "298");
           Add("Fiji", "679");
           Add("Finland", "358");
           Add("France", "33");
           Add("French Polynesia", "689");
           Add("Gabon", "241");
           Add("Gambia", "220");
           Add("Georgia", "995");
           Add("Germany", "49");
           Add("Ghana", "233");
           Add("Gibraltar", "350");
           Add("Greece", "30");
           Add("Greenland", "299");
           Add("Grenada", "1-473");
           Add("Guam", "1-671");
           Add("Guatemala", "502");
          
           Add("Guinea", "224");
           
           Add("Guyana", "592");
           Add("Haiti", "509");
           Add("Honduras", "504");
           Add("Hong Kong", "852");
           Add("Hungary", "36");
           Add("Iceland", "354");
           Add("India", "91");
           Add("Indonesia", "62");
           
           Add("Iraq", "964");
           Add("Ireland", "353");
          
           Add("Israel", "972");
           Add("Italy", "39");
           Add("Ivory Coast", "225");
           Add("Jamaica", "1-876");
           Add("Japan", "81");
           
           Add("Jordan", "962");
           Add("Kazakhstan", "7");
           Add("Kenya", "254");
           Add("Kiribati", "686");
           Add("Kosovo", "383");
           Add("Kuwait", "965");
           Add("Kyrgyzstan", "996");
           Add("Laos", "856");
           Add("Latvia", "371");
           Add("Lebanon", "961");
           Add("Lesotho", "266");
           Add("Liberia", "231");
           Add("Libya", "218");
           Add("Liechtenstein", "423");
           Add("Lithuania", "370");
           Add("Luxembourg", "352");
           Add("Macau", "853");
           Add("Macedonia", "389");
           Add("Madagascar", "261");
           Add("Malawi", "265");
           Add("Malaysia", "60");
           Add("Maldives", "960");
           Add("Mali", "223");
           Add("Malta", "356");
           Add("Marshall Islands", "692");
           Add("Mauritania", "222");
           Add("Mauritius", "230");
           Add("Mayotte", "262");
           Add("Mexico", "52");
           Add("Micronesia", "691");
           Add("Moldova", "373");
           Add("Monaco", "377");
           Add("Mongolia", "976");
           Add("Montenegro", "382");
           Add("Montserrat", "1-664");
           Add("Morocco", "212");
           Add("Mozambique", "258");
           Add("Myanmar", "95");
           Add("Namibia", "264");
           Add("Nauru", "674");
           Add("Nepal", "977");
           Add("Netherlands", "31");
           Add("Netherlands Antilles", "599");
           Add("New Caledonia", "687");
           Add("New Zealand", "64");
           Add("Nicaragua", "505");
           Add("Niger", "227");
           Add("Nigeria", "234");
           Add("Niue", "683");
           Add("North Korea", "850");
           Add("Northern Mariana Islands", "1-670");
           Add("Norway", "47");
           Add("Oman", "968");
           Add("Pakistan", "92");
           Add("Palau", "680");
           Add("Palestine", "970");
           Add("Panama", "507");
           Add("Papua New Guinea", "675");
           Add("Paraguay", "595");
           Add("Peru", "51");
           Add("Philippines", "63");
           Add("Pitcairn", "64");
           Add("Poland", "48");
           Add("Portugal", "351");
           
           Add("Qatar", "974");
           Add("Republic of the Congo", "242");
           Add("Reunion", "262");
           Add("Romania", "40");
           Add("Russia", "7");
           Add("Rwanda", "250");
           Add("Saint Barthelemy", "590");
           Add("Saint Helena", "290");
           Add("Saint Kitts and Nevis", "1-869");
           Add("Saint Lucia", "1-758");
           Add("Saint Martin", "590");
           Add("Saint Pierre and Miquelon", "508");
           Add("Saint Vincent and the Grenadines", "1-784");
           Add("Samoa", "685");
           Add("San Marino", "378");
           Add("Sao Tome and Principe", "239");
           Add("Saudi Arabia", "966");
           Add("Senegal", "221");
           Add("Serbia", "381");
           Add("Seychelles", "248");
           Add("Sierra Leone", "232");
           Add("Singapore", "65");
           Add("Sint Maarten", "1-721");
           Add("Slovakia", "421");
          
           Add("Solomon Islands", "677");
           Add("Somalia", "252");
           Add("South Africa", "27");
           Add("South Korea", "82");
           Add("South Sudan", "211");
           Add("Spain", "34");
           Add("Sri Lanka", "94");
           Add("Sudan", "249");
           Add("Suriname", "597");
           Add("Svalbard and Jan Mayen", "47");
           Add("Swaziland", "268");
           Add("Sweden", "46");
           Add("Switzerland", "41");
           Add("Syria", "963");
           Add("Taiwan", "886");
           Add("Tajikistan", "992");
           Add("Tanzania", "255");
           Add("Thailand", "66");
           Add("Togo", "228");
           Add("Tokelau", "690");
           Add("Tonga", "676");
           Add("Trinidad and Tobago", "1-868");
           Add("Tunisia", "216");
           Add("Turkey", "90");
           Add("Turkmenistan", "993");
           Add("Turks and Caicos Islands", "1-649");
           Add("Tuvalu", "688");
           Add("U.S. Virgin Islands", "1-340");
           Add("Uganda", "256");
           Add("Ukraine", "380");
           Add("United Arab Emirates", "971");
           Add("United Kingdom", "44");
           Add("United States", "1");
           Add("Uruguay", "598");
           Add("Uzbekistan", "998");
           Add("Vanuatu", "678");
           Add("Vatican", "379");
           Add("Venezuela", "58");
           Add("Vietnam", "84");
           Add("Wallis and Futuna", "681");
           Add("Western Sahara", "212");
           Add("Yemen", "967");
           Add("Zambia", "260");
           Add("Zimbabwe", "263");
        }

        private static void Add(string key, string value){
            if(!codes.ContainsKey(key)){
                codes.Add(key, value);
            }
        }




      
    }
}
