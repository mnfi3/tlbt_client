using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbot.Dialogs;
using Telbot.system;
using TeleSharp.TL;
using TLSharp.Core;
using TLSharp.Core.Exceptions;

namespace Telbot.telegram
{
    class Contact_telegram
    {
        TelegramResponse response;
        private static TelegramClient client;

        public Contact_telegram()
        {
            response = new TelegramResponse();
            client = newClient();

        }


        private TelegramClient newClient()
        {
            if (client != null) return client;
            if (G.telegram.api_id == 0 || G.telegram.api_hash.Length < 3) return null;
            try
            {
                return new TelegramClient(G.telegram.api_id, G.telegram.api_hash);
            }
            catch (MissingApiConfigurationException ex)
            {
                FailedDialog _dialog = new FailedDialog("مقادیر api_id یا  api_hash را با دقت وارد کنید");
                _dialog.ShowDialog();
                return null;
            }
        }


        public async Task getContacts(EventHandler handler)
        {
            await client.ConnectAsync();

            if (client == null)
            {
                response.status = 0;
                response.message = "عملیات شکست خورد.لطفا دوباره امتحان کنید";
                response.data = "";
                handler(response, new EventArgs());
                return;
            }

            var result = await client.GetContactsAsync();
            
           

            if (result.Users.Count > 0)
            {
                List<TLUser> users = result.Users.OfType<TLUser>().ToList<TLUser>();
                response.status = 1;
                response.message = "";
                response.data = users;
            }
            else
            {
                response.status = 0;
                response.message = "عملیات ارسال کد شکست خورد.لطفا دوباره امتحان کنید";
                response.data = new List<TLUser>();
            }
            handler(response, new EventArgs());
        }
    }
}
