// ######################################################################################################################
// #  Redistribution and use in source and binary forms, with or without modification, are permitted provided that the  #
// #  following conditions are met:                                                                                     #
// #    1、Redistributions of source code must retain the above copyright notice, this list of conditions and the       #
// #       following disclaimer.                                                                                        #
// #    2、Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the    #
// #       following disclaimer in the documentation and/or other materials provided with the distribution.             #
// #    3、Neither the name of the D.E.M.O.N, K9998(Wei Tao) nor the names of its contributors may be used to endorse   #
// #       or promote products derived from this software without specific prior written permission.                    #
// #                                                                                                                    #
// #       Project Name:                                                                                                #
// #       Module  Name:                                                                                                #
// #       Part of:                                                                                                     #
// #       Date:                                                                                                        #
// #       Version:                                                                                                     #
// #                                                                                                                    #
// #                                           Copyright © 2011 ORG: D.E.M.O.N K9998(Wei Tao) All Rights Reserved      #
// ######################################################################################################################

#region

#endregion

namespace BatchAPKInstaller.Class
{
    #region

    using System.Runtime.InteropServices;
    using System.Text;

    #endregion

    #region

    #endregion

    public class clsINI
    {
        private readonly string path;

        /// <summary>
        ///   INIFile Constructor.
        /// </summary>
        /// <PARAM name = "INIPath"></PARAM>
        public clsINI(string INIPath)
        {
            path = INIPath;
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
                                                             string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                                                          string key, string def, StringBuilder retVal,
                                                          int size, string filePath);

        /// <summary>
        ///   Write Data to the INI File
        /// </summary>
        /// <PARAM name = "Section"></PARAM>
        /// Section name
        /// <PARAM name = "Key"></PARAM>
        /// Key Name
        /// <PARAM name = "Value"></PARAM>
        /// Value Name
        public void IniWriteValue(string sSection, string sKey, string Value)
        {
            WritePrivateProfileString(sSection, sKey, Value, path);
        }

        /// <summary>
        ///   Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name = "Section"></PARAM>
        /// <PARAM name = "Key"></PARAM>
        /// <PARAM name = "Path"></PARAM>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            var temp = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", temp,
                                    255, path);
            return temp.ToString();
        }
    }
}