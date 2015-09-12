package com.example.anik.ilostmymouse.fragments;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.example.anik.ilostmymouse.R;

/**
 * Created by Anik on 12-Sep-15, 012.
 */
public class NoWifiFragment extends Fragment {
    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.layout_for_no_wifi, container, false);
        return view;
    }
}
