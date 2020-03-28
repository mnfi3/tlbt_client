using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbot.Dialogs;
using Telbot.system;
using TeleSharp.TL;
using TeleSharp.TL.Channels;
using TeleSharp.TL.Contacts;
using TeleSharp.TL.Messages;
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
        //}


        public virtual async void getContacts(EventHandler handler)
        {
            //if (!client.IsConnected)
            //    await client.ConnectAsync();

            if (client == null)
            {
                response.status = 0;
                response.message = "عملیات شکست خورد.لطفا دوباره امتحان کنید";
                response.data = new List<TLUser>();
                handler(response, new EventArgs());
                return;
            }




            try
            {
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
            }
            catch (Exception e)
            {
                response.status = 0;
                response.message = "عملیات ارسال کد شکست خورد.لطفا دوباره امتحان کنید";
                response.data = new List<TLUser>();
            }
            
            
           

          
            handler(response, new EventArgs());
        }


        public virtual async void addNumberToChannel(EventHandler handler, TLVector<TLInputPhoneContact> contacts, TLChannel channel)
        {
            if (client == null)
            {
                response.status = 0;
                response.message = "عملیات شکست خورد.لطفا دوباره امتحان کنید";
                response.data = new List<TLUser>();
                handler(response, new EventArgs());
                return;
            }

           

            try
            {

                //-----------------------------------add to contacts-----------------------------------------------

                TLImportedContacts importedContacts = await client.SendRequestAsync<TLImportedContacts>(new TLRequestImportContacts
                {
                    Contacts = contacts
                });


                
                //------------------------------------get new users from contacts------------------------------------------------

                //client.Dispose();

                await client.ConnectAsync();

                var result = await client.GetContactsAsync();
                List<TLUser> all_users = result.Users.OfType<TLUser>().ToList<TLUser>();

                List<TLUser> new_users = new List<TLUser>();

                foreach (TLUser u in all_users)
                {
                    foreach (TLInputPhoneContact c in contacts)
                    {
                        if (c.Phone == u.Phone)
                        {
                            new_users.Add(u);
                        }
                    }
                }


                List<TLUser> added_users = new List<TLUser>();


                //------------------------------------add to channel in one step------------------------------------------------

                if (!client.IsConnected) await client.ConnectAsync();
                TLInputUser new_user;
                TLVector<TLAbsInputUser> to_added_users = new TLVector<TLAbsInputUser>();
                TLVector<TLAbsInputChannel> target_channel = new TLVector<TLAbsInputChannel>();
                target_channel.Add(new TLInputChannel() { ChannelId = channel.Id, AccessHash = (long)channel.AccessHash });
                foreach (TLUser user in new_users)
                {
                    new_user = new TLInputUser() { UserId = user.Id, AccessHash = (long)user.AccessHash };
                    to_added_users.Add(new_user);
                }
                var req = new TLRequestInviteToChannel()
                {

                    Channel = target_channel[0],
                    Users = to_added_users
                };

                await client.ConnectAsync();

                try
                {
                    var update = await client.SendRequestAsync<TLUpdates>(req);
                    //added_users.Add(user);
                }
                catch (Exception e)
                {
                    response.status = 0;
                    response.message = "عملیات  شکست خورد.لطفا دوباره امتحان کنید";
                    response.data = new List<TLUser>();
                }




                //------------------------------------add to channel one by one with delay------------------------------------------------

                //await client.ConnectAsync();
                //foreach (TLUser user in new_users)
                //{


                //    if (!client.IsConnected) await client.ConnectAsync();

                //    TLInputUser u = new TLInputUser() { UserId = user.Id, AccessHash = (long)user.AccessHash };
                //    TLVector<TLAbsInputUser> u2 = new TLVector<TLAbsInputUser>();
                //    TLVector<TLAbsInputChannel> c2 = new TLVector<TLAbsInputChannel>();
                //    u2.Add(u);
                //    c2.Add(new TLInputChannel() { ChannelId = channel.Id, AccessHash = (long)channel.AccessHash });
                //    var req2 = new TLRequestInviteToChannel()
                //    {

                //        Channel = c2[0],
                //        Users = u2
                //    };
                //    await client.ConnectAsync();

                //    try
                //    {
                //        var update = await client.SendRequestAsync<TLUpdates>(req2);
                //        added_users.Add(user);
                //    }
                //    catch (Exception e)
                //    {
                //        e.ToString();
                //    }


                //    Random rand = new Random();
                //    int num = (int)rand.Next(3, 15);
                //    await Task.Delay(num * 1000);

                //}








                response.status = 1;
                response.message = "";
                response.data = added_users;

            }
            catch (Exception e)
            {
                response.status = 0;
                response.message = "عملیات  شکست خورد.لطفا دوباره امتحان کنید";
                response.data = new List<TLUser>();
            }

            handler(response, new EventArgs());

           

        }

        
    }
}
