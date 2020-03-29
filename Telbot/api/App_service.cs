using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbot.Dialogs;
using Telbot.model;
using Telbot.storage;
using Telbot.system;

namespace Telbot.api
{
    class App_service
    {
        private EventHandler eventLogin = null;
        private EventHandler eventAuthCheck = null;



        public void login(string user_name, string password, EventHandler handler)
        {
            Request request = new Request();
            eventLogin += handler;

            Dictionary<string, string> data = new Dictionary<string, string> { { "user_name", user_name }, { "client_key", G.client_key}, { "password", password } };
            Dictionary<string, string> headers = new Dictionary<string, string> ();
            request.post(Urls.APP_LOGIN, data, headers, loginCompleteCallBack);
        }

        private void loginCompleteCallBack(object sender, EventArgs e)
        {
            Response res = sender as Response;
            App_model app;
            if (res.status == 1)
            {
                JObject data = res.data;
                app = App_model.parse(res.data);
                app.is_logged_in = 1;
                Log.i("app login was successfull", "App_service", "login");
            }
            else
            {
                FailedDialog _dialog = new FailedDialog(res.message);
                _dialog.ShowDialog();
                app = new App_model();
                //Log.e(res.full_response, "App_service", "login");
            }

            eventLogin(app, new EventArgs());
        }



        public void authCheck(EventHandler handler)
        {
            Request req = new Request();
            eventAuthCheck += handler;
            Dictionary<string, string> headers = new Dictionary<string, string> { { "app-token", G.app.token }, { "client-key", G.client_key } };
            req.get(Urls.APP_AUTH_CHECK, new Dictionary<string, string>(), headers, authCheckCallBack);
        }

        private void authCheckCallBack(object sender, EventArgs e)
        {
            Response res = sender as Response;
            eventAuthCheck(res, new EventArgs());
            //Log.i(res.full_response, "App_service", "authCheck");
        }






       
    }
}
