package com.example.anik.ilostmymouse;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.Bundle;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.widget.FrameLayout;

import com.example.anik.ilostmymouse.fragments.NoWifiFragment;
import com.example.anik.ilostmymouse.fragments.WifiAvailableFragment;
import com.example.anik.ilostmymouse.helpers.AppHelper;

import org.apache.commons.lang3.ArrayUtils;

import java.math.BigInteger;
import java.net.InetAddress;
import java.net.UnknownHostException;


public class MainActivity extends AppCompatActivity {

    static int i = 0;
    FragmentManager fragmentManager;
    FragmentTransaction fragmentTransaction;
    FrameLayout frameLayout;
    WifiManager wifiManager;
    String wifiSSIDName = "";
    private BroadcastReceiver broadcastReceiver = null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        wifiManager = (WifiManager) getSystemService(WIFI_SERVICE);
        fragmentManager = getSupportFragmentManager();
        frameLayout = (FrameLayout) findViewById(R.id.frameLayoutForChangingView);
        createWifiBroadcastReceiver();
        //changeViewBasedOnWifiConnection();
    }

    private void changeViewBasedOnWifiConnection() {
        fragmentTransaction = fragmentManager.beginTransaction();
        if (this.isWiFiAvailable()) {
            WifiAvailableFragment wifiAvailableFragment = new WifiAvailableFragment();
            wifiAvailableFragment.setSSIDName(wifiSSIDName);
            fragmentTransaction.replace(R.id.frameLayoutForChangingView, wifiAvailableFragment);
            fragmentTransaction.commit();
        } else {
            NoWifiFragment noWifiFragment = new NoWifiFragment();
            fragmentTransaction.replace(R.id.frameLayoutForChangingView, noWifiFragment);
            fragmentTransaction.commit();
        }
    }

    private boolean isWiFiAvailable() {
        WifiInfo wifiinfo = wifiManager.getConnectionInfo();
        byte[] myIPAddress = BigInteger.valueOf(wifiinfo.getIpAddress()).toByteArray();
        ArrayUtils.reverse(myIPAddress);
        InetAddress inetAddress = null;
        String remoteHostIPAddress = "0.0.0.0";
        try {
            inetAddress = InetAddress.getByAddress(myIPAddress);
            remoteHostIPAddress = inetAddress.getHostAddress();
            wifiSSIDName = wifiinfo.getSSID();
            String connectionIpAddress = String.format("%s.1", remoteHostIPAddress.substring(0, remoteHostIPAddress.lastIndexOf('.')));
            AppHelper.REMOTE_HOST_IP = connectionIpAddress;

        } catch (UnknownHostException e) {
            Log.v(AppHelper.TAG, "Unknown host exception");
        }
        return !(remoteHostIPAddress.equals("0.0.0.0"));
    }

    @Override
    protected void onResume() {
        super.onResume();
        IntentFilter intentFilter = new IntentFilter();
        intentFilter.addAction(WifiManager.NETWORK_STATE_CHANGED_ACTION);
        registerReceiver(broadcastReceiver, intentFilter);
    }

    @Override
    protected void onPause() {
        super.onPause();
        unregisterReceiver(broadcastReceiver);
    }

    private void createWifiBroadcastReceiver() {
        broadcastReceiver = new BroadcastReceiver() {
            @Override
            public void onReceive(Context context, Intent intent) {

                final String action = intent.getAction();
                if (action.equals(WifiManager.NETWORK_STATE_CHANGED_ACTION)) {
                    if (!intent.getBooleanExtra(WifiManager.EXTRA_SUPPLICANT_CONNECTED, false)) {
                        runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                changeViewBasedOnWifiConnection();
                            }
                        });
                    }
                }
            }
        };
    }
}
