using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbot.Dialogs;
using Telbot.system;
using TLSharp.Core;
using TLSharp.Core.Exceptions;

namespace Telbot.telegram
{
    class Base_telegram
    {

        private static TelegramClient client = null;

        public static TelegramClient getTelegramClient()
        {
            if (client != null)
            {
                if (!client.IsConnected)
                {
                    client.ConnectAsync();
                }
                return client;
            }
            //if (G.telegram.api_id == 0 || G.telegram.api_hash.Length < 3) return null;
            try
            {
                client = new TelegramClient(G.telegram.api_id, G.telegram.api_hash);
                client.ConnectAsync();
            }
            catch (MissingApiConfigurationException ex)
            {
                FailedDialog _dialog = new FailedDialog("مقادیر api_id یا  api_hash را با دقت وارد کنید");
                _dialog.ShowDialog();
                return null;
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                FailedDialog _dialog = new FailedDialog("خطا در ارتباط با سرور تلگرام");
                _dialog.ShowDialog();
                return null;
            }

            return client;
        }
    }
}
