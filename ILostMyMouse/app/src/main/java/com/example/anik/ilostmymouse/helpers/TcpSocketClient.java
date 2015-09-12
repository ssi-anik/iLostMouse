package com.example.anik.ilostmymouse.helpers;

import android.content.Context;
import android.os.AsyncTask;
import android.os.Handler;
import android.util.Log;
import android.widget.Toast;

import java.io.IOException;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.UnknownHostException;

/**
 * Created by Anik on 12-Sep-15, 012.
 */

public class TcpSocketClient {
    private Socket socket;
    private PrintWriter out;
    private boolean connected;
    private Context context;

    public TcpSocketClient() {
        socket = null;
        out = null;
        connected = false;
    }

    public void connect(Context context) {
        this.context = context;
        new ConnectTask(context).execute();
    }

    public void disconnect(Context context) {
        if (connected) {
            try {
                out.close();
                socket.close();
                connected = false;
            } catch (IOException e) {
                showToast(context, "Couldn't get I/O for the connection");
                Log.e(AppHelper.TAG, e.getMessage());
            }
        }
    }

    public void send(String command) {
        if (connected)
            out.println(command);
        else
            showToast(context, "You're not connected");
    }

    public boolean isConnected() {
        return connected;
    }

    private void showToast(final Context context, final String message) {
        new Handler(context.getMainLooper()).post(new Runnable() {

            @Override
            public void run() {
                Toast.makeText(context, message, Toast.LENGTH_SHORT).show();
            }
        });
    }

    private class ConnectTask extends AsyncTask<Void, Void, Void> {

        private Context context;

        public ConnectTask(Context context) {
            this.context = context;
        }

        @Override
        protected void onPreExecute() {
            showToast(context, "Connecting to the host..");
            super.onPreExecute();
        }

        @Override
        protected void onPostExecute(Void result) {
            if (connected) {
                showToast(context, "Successfully connected.");
            }
            super.onPostExecute(result);
        }

        @Override
        protected Void doInBackground(Void... params) {
            try {
                String host = AppHelper.REMOTE_HOST_IP;
                int port = Integer.parseInt(AppHelper.SOCKET_PORT);
                socket = new Socket(host, port);
                out = new PrintWriter(socket.getOutputStream(), true);
                connected = true;
            } catch (UnknownHostException e) {
                showToast(context, "Can not figure out the host");
                connected = false;
                Log.e(AppHelper.TAG, e.getMessage());
            } catch (IOException e) {
                showToast(context, "Couldn't get I/O for the connection to: the host ");
                connected = false;
                Log.e(AppHelper.TAG, e.getMessage());
            }
            return null;
        }


    }
}
