package com.example.anik.ilostmymouse.fragments;

import android.app.Activity;
import android.content.Context;
import android.graphics.Color;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.GestureDetector;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.RelativeLayout;
import android.widget.TextView;

import com.example.anik.ilostmymouse.R;
import com.example.anik.ilostmymouse.helpers.AppHelper;
import com.example.anik.ilostmymouse.helpers.CustomGestureHandler;
import com.example.anik.ilostmymouse.helpers.TcpSocketClient;

/**
 * Created by Anik on 12-Sep-15, 012.
 */
public class WifiAvailableFragment extends Fragment {

    private final int speed = 10;
    private final int DELAY_BETWEEN_CLICKS_IN_MILLISECONDS = 200;
    private String SSIDName;
    private Activity activity;
    private Context context;
    private RelativeLayout baseLayout;
    private Button buttonLeft;
    private Button buttonRight;
    private int number_of_clicks = 0;
    private boolean thread_started = false;
    private TcpSocketClient client;
    private GestureDetector gestureDetector;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.layout_for_wifi_available, container, false);

        activity = getActivity();
        context = activity.getApplicationContext();

        baseLayout = (RelativeLayout) view.findViewById(R.id.baseLayout);
        baseLayout.setOnTouchListener(new CustomGestureHandler(activity) {
            @Override
            public void onSingleClick() {
                super.onSingleClick();
                client.send(AppHelper.FORMAT_LEFT_CLICK);
            }

            @Override
            public void onDoubleClick() {
                super.onDoubleClick();
                client.send(AppHelper.FORMAT_DOUBLE_CLICK);
            }

            @Override
            public void onSwipeRight(int diffX, int diffY) {
                super.onSwipeRight(diffX, diffY);
                String command = String.format(AppHelper.FORMAT_MOVE, diffX, diffY);
                client.send(command);
            }

            @Override
            public void onSwipeLeft(int diffX, int diffY) {
                super.onSwipeLeft(diffX, diffY);
                String command = String.format(AppHelper.FORMAT_MOVE, diffX, diffY);
                client.send(command);
            }

            @Override
            public void onSwipeUp(int diffX, int diffY) {
                super.onSwipeUp(diffX, diffY);
                String command = String.format(AppHelper.FORMAT_MOVE, diffX, diffY);
                client.send(command);
            }

            @Override
            public void onSwipeDown(int diffX, int diffY) {
                super.onSwipeDown(diffX, diffY);
                String command = String.format(AppHelper.FORMAT_MOVE, diffX, diffY);
                client.send(command);
                Log.v(AppHelper.TAG, "onSwipeDown");
            }
        });

        buttonLeft = (Button) view.findViewById(R.id.buttonLeft);
        buttonRight = (Button) view.findViewById(R.id.buttonRight);

        TextView ssidName = (TextView) view.findViewById(R.id.textViewForWifiName);
        ssidName.setText(String.format("Currently connected to: %s", this.SSIDName));
        ssidName.setTextColor(Color.rgb(70, 147, 153));

        client = new TcpSocketClient();
        client.connect(activity);

        registerRightButtonClickListener();
        registerLeftButtonClickListener();

        return view;
    }

    private void registerLeftButtonClickListener() {
        buttonLeft.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ++number_of_clicks;
                if (!thread_started) {
                    new Thread(new Runnable() {
                        @Override
                        public void run() {
                            thread_started = true;
                            try {
                                Thread.sleep(DELAY_BETWEEN_CLICKS_IN_MILLISECONDS);
                                if (number_of_clicks == 1) {
                                    client.send(AppHelper.FORMAT_LEFT_CLICK);
                                } else if (number_of_clicks == 2) {
                                    client.send(AppHelper.FORMAT_DOUBLE_CLICK);
                                }
                                number_of_clicks = 0;
                                thread_started = false;
                            } catch (InterruptedException e) {
                                e.printStackTrace();
                            }
                        }
                    }).start();
                }
            }
        });
    }

    private void registerRightButtonClickListener() {
        buttonRight.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String command = AppHelper.FORMAT_RIGHT_CLICK;
                client.send(command);
            }
        });
    }

    public void setSSIDName(String SSIDName) {
        this.SSIDName = SSIDName.replaceAll("\"", "");
    }
}
