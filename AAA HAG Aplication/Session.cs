﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace AAA_HAG_Aplication
{
    internal class Session
    {
        public static MySqlConnection conn; // This should be used to open up connections when they are needed.
                                            // It is defined at the beginning of the main page
    }
    internal class AccountEmail
    {
        public static string address; // The users email address is stored here.
                                      // It is defined during the log in process on the log in page
    }
}
