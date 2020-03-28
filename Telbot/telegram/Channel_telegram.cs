using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbot.Dialogs;
using Telbot.system;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TLSharp.Core;
using TLSharp.Core.Exceptions;

namespace Telbot.telegram
{
    class Channel_telegram
    {
        TelegramResponse response;
        private static TelegramClient client;

        public Channel_telegram()
        {
           response = new TelegramResponse();
           client = Base_telegram.getTelegramClient();

        }


        //private TelegramClient newClient()
        //{
        //    if (client != null) return client;
        //    if (G.telegram.api_id == 0 || G.telegram.api_hash.Length < 3) return null;
        //    try
        //    {
        //        return new TelegramClient(G.telegram.api_id, G.telegram.api_hash);
        //    }
        //    catch (MissingApiConfigurationException ex)
        //    {
        //        FailedDialog _dialog = new FailedDialog("مقادیر api_id یا  api_hash را با دقت وارد کنید");
        //        _dialog.ShowDialog();
        //        return null;
        //    }
        //    catch (System.Net.Sockets.SocketException ex)
        //    {
        //        FailedDialog _dialog = new FailedDialog("خطا در ارتباط با سرور تلگرام");
        //        _dialog.ShowDialog();
        //        return null;
        //    }
        //}


        public virtual async void getChannels(EventHandler handler)
        {
            if (client == null)
            {
                response.status = 0;
                response.message = "عملیات ارسال کد شکست خورد.لطفا دوباره امتحان کنید";
                response.data = "";
                handler(response, new EventArgs());
                return;
            }

           

            //List<TLUser> users = result.Users.OfType<TLUser>().ToList<TLUser>();
            try
            {
                //await client.ConnectAsync();

                var dialogs = (TLDialogs)await client.GetUserDialogsAsync();
                List<TLChannel> channels = dialogs.Chats.OfType<TLChannel>().ToList<TLChannel>();
                if (channels != null)
                {
                    response.status = 1;
                    response.message = "";
                    response.data = channels;
                }
                else
                {
                    response.status = 0;
                    response.message = "خطایی در دریافت اطلاعات رخ داده است";
                    response.data = channels;
                }
            }
            catch (Exception e)
            {
                response.status = 0;
                response.message = "خطایی در دریافت اطلاعات رخ داده است";
                response.data = new List<TLChannel>();
            }
            handler(response, new EventArgs());
        }

    }
}
