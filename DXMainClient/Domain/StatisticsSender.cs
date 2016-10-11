﻿using ClientCore;
using Rampastring.Tools;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace DTAClient.Domain
{
    /// <summary>
    /// A class for sending statistics about updates and CnCNet to Google Analytics.
    /// </summary>
    public class StatisticsSender
    {
        private static StatisticsSender _instance;

        public static StatisticsSender Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StatisticsSender();

                return _instance;
            }
        }

        private StatisticsSender()
        {
            UserAgentHandler.ChangeUserAgent();
            wb = new WebBrowser();

            var gameUrlInfos = new GameURLInfo[]
            {
                new GameURLInfo("DTA", "http://dta.ppmsite.com/ga-dta-update.htm", "http://dta.ppmsite.com/ga-dta-cncnet.htm"),
                new GameURLInfo("TI", "http://dta.ppmsite.com/ga-ti-update.htm", "http://dta.ppmsite.com/ga-ti-cncnet.htm"),
                new GameURLInfo("TS", "http://dta.ppmsite.com/ga-ts-update.htm", "http://dta.ppmsite.com/ga-ts-cncnet.htm"),
                new GameURLInfo("MO", "http://dta.ppmsite.com/ga-mo-update.htm", "http://dta.ppmsite.com/ga-mo-cncnet.htm"),
                new GameURLInfo("YR", "http://dta.ppmsite.com/ga-yr-update.htm", "http://dta.ppmsite.com/ga-yr-cncnet.htm"),
            };

            urlInfos = gameUrlInfos.ToList();

            myGameInfo = urlInfos.Find(g => g.GameID == ClientConfiguration.Instance.LocalGame);
        }

        private List<GameURLInfo> urlInfos;

        private WebBrowser wb;

        private GameURLInfo myGameInfo;

        public void SendUpdate()
        {
            if (myGameInfo == null)
                return;

            wb.Navigate(myGameInfo.UpdateURL);
        }

        public void SendCnCNet()
        {
            if (myGameInfo == null)
                return;

            wb.Navigate(myGameInfo.CnCNetURL);
        }

        class GameURLInfo
        {
            public GameURLInfo(string gameId, string updateUrl, string cncnetUrl)
            {
                GameID = gameId;
                UpdateURL = updateUrl;
                CnCNetURL = cncnetUrl;
            }

            public string GameID;
            public string UpdateURL;
            public string CnCNetURL;
        }
    }
}